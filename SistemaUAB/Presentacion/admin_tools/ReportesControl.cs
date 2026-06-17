using iTextSharp.text;
using iTextSharp.text.pdf;
using SistemaUAB.DataLayers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// Alias para evitar confusiones
using PdfFont = iTextSharp.text.Font;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class ReportesControl : UserControl
    {
        private DataTable reporteDataTable;
        private string tipoReporteActual = "";
        private bool esReporteConTabla = false;

        public ReportesControl()
        {
            InitializeComponent();
            ConfigurarFechasPorDefecto();
            ConfigurarComboReportes();
            ConfigurarChart();
            dgvResultados.AutoGenerateColumns = true;
        }

        #region Configuración Inicial

        private void ConfigurarFechasPorDefecto()
        {
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now;
        }

        private void ConfigurarComboReportes()
        {
            cmbTipoReporte.Items.Clear();
            cmbTipoReporte.Items.Add("📊 Ambientes más usados");
            cmbTipoReporte.Items.Add("🕐 Horarios de mayor ocupación");
            cmbTipoReporte.Items.Add("👥 Reservas por tipo de usuario");
            cmbTipoReporte.Items.Add("❌ Reservas canceladas con motivos");
            cmbTipoReporte.Items.Add("📈 Tendencia diaria de reservas");
            cmbTipoReporte.SelectedIndex = 0;
        }

        private void ConfigurarChart()
        {
            chartResultados.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "ChartAreaPrincipal";
            chartArea.BackColor = Color.WhiteSmoke;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            chartResultados.ChartAreas.Add(chartArea);

            chartResultados.Legends.Clear();
            Legend legend = new Legend();
            legend.Name = "Legend1";
            legend.Docking = Docking.Bottom;
            legend.Font = new System.Drawing.Font("Segoe UI", 10);
            chartResultados.Legends.Add(legend);

            chartResultados.Series.Clear();
            Series series = new Series();
            series.ChartArea = "ChartAreaPrincipal";
            series.Legend = "Legend1";
            series.Name = "Series1";
            chartResultados.Series.Add(series);

            chartResultados.Titles.Clear();
            Title title = new Title();
            title.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(0, 122, 204);
            title.Name = "Title1";
            title.Text = "Reporte de Reservas";
            chartResultados.Titles.Add(title);
        }

        #endregion

        #region Métodos de Generación de Reportes

        private void GenerarReporte()
        {
            if (!ValidarFiltros())
                return;

            MostrarCarga(true);
            try
            {
                switch (cmbTipoReporte.SelectedIndex)
                {
                    case 0: GenerarReporteAmbientesMasUsados(); break;
                    case 1: GenerarReporteHorariosOcupacion(); break;
                    case 2: GenerarReportePorTipoUsuario(); break;
                    case 3: GenerarReporteCancelaciones(); break;
                    case 4: GenerarReporteTendenciaDiaria(); break;
                    default: MessageBox.Show("Seleccione un tipo de reporte válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;
                }
                ActualizarTituloReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCarga(false);
            }
        }

        private void GenerarReporteAmbientesMasUsados()
        {
            string query = @"
                SELECT TOP 5 
                    a.Codigo AS Ambiente,
                    COUNT(r.IdReserva) AS TotalReservas,
                    a.CapacidadMaxima,
                    CAST(COUNT(r.IdReserva) * 100.0 / SUM(COUNT(r.IdReserva)) OVER() AS DECIMAL(5,2)) AS Porcentaje
                FROM Ambiente a
                INNER JOIN Reserva r ON a.IdAmbiente = r.IdAmbiente
                WHERE r.Fecha BETWEEN @fechaInicio AND @fechaFin
                  AND r.Estado = 'Activa'
                GROUP BY a.Codigo, a.CapacidadMaxima
                ORDER BY TotalReservas DESC";

            var parametros = new Dictionary<string, object>
            {
                { "@fechaInicio", dtpDesde.Value },
                { "@fechaFin", dtpHasta.Value }
            };

            reporteDataTable = Helpers.EjecutarQuery(query, parametros);
            tipoReporteActual = "Ambientes más usados";
            esReporteConTabla = true;

            if (reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el rango de fechas seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEnChart(reporteDataTable, "Ambientes más usados", "Ambiente", "TotalReservas", SeriesChartType.Column);
                return;
            }

            MostrarEnChart(reporteDataTable, "Ambientes más usados", "Ambiente", "TotalReservas", SeriesChartType.Column);
            MostrarEnDataGridView(reporteDataTable);
        }

        private void GenerarReporteHorariosOcupacion()
        {
            string query = @"
                SELECT 
                    DATEPART(HOUR, r.HoraInicio) AS Hora,
                    COUNT(*) AS TotalReservas,
                    CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS Porcentaje
                FROM Reserva r
                WHERE r.Fecha BETWEEN @fechaInicio AND @fechaFin
                  AND r.Estado = 'Activa'
                GROUP BY DATEPART(HOUR, r.HoraInicio)
                ORDER BY Hora ASC";

            var parametros = new Dictionary<string, object>
            {
                { "@fechaInicio", dtpDesde.Value },
                { "@fechaFin", dtpHasta.Value }
            };

            reporteDataTable = Helpers.EjecutarQuery(query, parametros);
            tipoReporteActual = "Horarios de mayor ocupación";
            esReporteConTabla = true;

            if (reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el rango de fechas seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEnChart(reporteDataTable, "Horarios de mayor ocupación", "Hora", "TotalReservas", SeriesChartType.Line);
                return;
            }

            MostrarEnChart(reporteDataTable, "Horarios de mayor ocupación", "Hora", "TotalReservas", SeriesChartType.Line);
            MostrarEnDataGridView(reporteDataTable);
        }

        private void GenerarReportePorTipoUsuario()
        {
            string query = @"
                SELECT 
                    u.TipoUsuario AS Tipo,
                    COUNT(r.IdReserva) AS TotalReservas,
                    COUNT(DISTINCT u.IdUsuario) AS UsuariosUnicos,
                    CAST(COUNT(r.IdReserva) * 100.0 / SUM(COUNT(r.IdReserva)) OVER() AS DECIMAL(5,2)) AS Porcentaje
                FROM Usuario u
                INNER JOIN Reserva r ON u.IdUsuario = r.IdUsuario
                WHERE r.Fecha BETWEEN @fechaInicio AND @fechaFin
                  AND r.Estado = 'Activa'
                GROUP BY u.TipoUsuario
                ORDER BY TotalReservas DESC";

            var parametros = new Dictionary<string, object>
            {
                { "@fechaInicio", dtpDesde.Value },
                { "@fechaFin", dtpHasta.Value }
            };

            reporteDataTable = Helpers.EjecutarQuery(query, parametros);
            tipoReporteActual = "Reservas por tipo de usuario";
            esReporteConTabla = true;

            if (reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el rango de fechas seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEnChart(reporteDataTable, "Reservas por tipo de usuario", "Tipo", "TotalReservas", SeriesChartType.Pie);
                return;
            }

            MostrarEnChart(reporteDataTable, "Reservas por tipo de usuario", "Tipo", "TotalReservas", SeriesChartType.Pie);
            MostrarEnDataGridView(reporteDataTable);
        }

        private void GenerarReporteCancelaciones()
        {
            string query = @"
                SELECT 
                    a.Codigo AS Ambiente,
                    u.NombreCompleto AS Usuario,
                    r.Fecha,
                    r.HoraInicio,
                    r.HoraFin,
                    r.Motivo AS MotivoCancelacion,
                    DATEDIFF(DAY, r.Fecha, GETDATE()) AS DiasDesdeCancelacion
                FROM Reserva r
                INNER JOIN Ambiente a ON r.IdAmbiente = a.IdAmbiente
                INNER JOIN Usuario u ON r.IdUsuario = u.IdUsuario
                WHERE r.Fecha BETWEEN @fechaInicio AND @fechaFin
                  AND r.Estado = 'Cancelada'
                ORDER BY r.Fecha DESC";

            var parametros = new Dictionary<string, object>
            {
                { "@fechaInicio", dtpDesde.Value },
                { "@fechaFin", dtpHasta.Value }
            };

            reporteDataTable = Helpers.EjecutarQuery(query, parametros);
            tipoReporteActual = "Reservas canceladas con motivos";
            esReporteConTabla = true;

            if (reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron cancelaciones para el rango de fechas seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEnDataGridView(reporteDataTable);
                chartResultados.Visible = false;
                return;
            }

            // Para cancelaciones mostramos solo tabla, no gráfico
            chartResultados.Visible = false;
            MostrarEnDataGridView(reporteDataTable);
        }

        private void GenerarReporteTendenciaDiaria()
        {
            string query = @"
                SELECT 
                    r.Fecha,
                    COUNT(*) AS TotalReservas,
                    SUM(CASE WHEN r.Estado = 'Activa' THEN 1 ELSE 0 END) AS Activas,
                    SUM(CASE WHEN r.Estado = 'Cancelada' THEN 1 ELSE 0 END) AS Canceladas,
                    SUM(CASE WHEN r.Estado = 'Finalizada' THEN 1 ELSE 0 END) AS Finalizadas
                FROM Reserva r
                WHERE r.Fecha BETWEEN @fechaInicio AND @fechaFin
                GROUP BY r.Fecha
                ORDER BY r.Fecha ASC";

            var parametros = new Dictionary<string, object>
            {
                { "@fechaInicio", dtpDesde.Value },
                { "@fechaFin", dtpHasta.Value }
            };

            reporteDataTable = Helpers.EjecutarQuery(query, parametros);
            tipoReporteActual = "Tendencia diaria de reservas";
            esReporteConTabla = true;

            if (reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el rango de fechas seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEnChart(reporteDataTable, "Tendencia diaria de reservas", "Fecha", "TotalReservas", SeriesChartType.Line);
                return;
            }

            MostrarEnChartTendencia(reporteDataTable);
            MostrarEnDataGridView(reporteDataTable);
        }

        #endregion

        #region Visualización de Resultados

        private void MostrarEnChart(DataTable data, string titulo, string ejeX, string ejeY, SeriesChartType tipo = SeriesChartType.Column)
        {
            chartResultados.Visible = true;
            dgvResultados.Visible = false;

            chartResultados.Series.Clear();
            chartResultados.Titles[0].Text = titulo;

            Series series = new Series();
            series.ChartArea = "ChartAreaPrincipal";
            series.Legend = "Legend1";
            series.Name = titulo;
            series.ChartType = tipo;

            if (tipo == SeriesChartType.Pie)
            {
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Black";
                series["PieStartAngle"] = "90";
            }

            chartResultados.ChartAreas[0].AxisX.Title = ejeX;
            chartResultados.ChartAreas[0].AxisY.Title = ejeY;

            foreach (DataRow row in data.Rows)
            {
                string xValue = row[ejeX]?.ToString() ?? "";
                double yValue = Convert.ToDouble(row[ejeY]);
                DataPoint point = series.Points.Add(yValue);
                point.AxisLabel = xValue;
                point.Label = yValue.ToString("N0");

                if (tipo == SeriesChartType.Pie)
                {
                    point.Label = $"{xValue}\n{yValue:N0} ({row["Porcentaje"]?.ToString() ?? "0"}%)";
                }
            }

            chartResultados.Series.Add(series);
        }

        private void MostrarEnChartTendencia(DataTable data)
        {
            chartResultados.Visible = true;
            dgvResultados.Visible = false;
            chartResultados.Series.Clear();
            chartResultados.Titles[0].Text = "Tendencia diaria de reservas";

            chartResultados.ChartAreas[0].AxisX.Title = "Fecha";
            chartResultados.ChartAreas[0].AxisY.Title = "Total";

            string[] columnas = { "TotalReservas", "Activas", "Canceladas", "Finalizadas" };
            Color[] colores = { Color.FromArgb(0, 122, 204), Color.FromArgb(40, 167, 69), Color.FromArgb(220, 53, 69), Color.FromArgb(255, 193, 7) };
            string[] nombresSeries = { "Total", "Activas", "Canceladas", "Finalizadas" };

            for (int i = 0; i < columnas.Length; i++)
            {
                Series series = new Series();
                series.ChartArea = "ChartAreaPrincipal";
                series.Legend = "Legend1";
                series.Name = nombresSeries[i];
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.Color = colores[i];
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 6;

                foreach (DataRow row in data.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                    double valor = Convert.ToDouble(row[columnas[i]]);
                    DataPoint point = series.Points.Add(valor);
                    point.AxisLabel = fecha.ToString("dd/MM");
                    point.Label = valor.ToString("N0");
                }

                chartResultados.Series.Add(series);
            }
        }

        private void MostrarEnDataGridView(DataTable data)
        {
            if (data.Rows.Count > 20)
            {
                chartResultados.Visible = false;
                dgvResultados.Visible = true;
                dgvResultados.DataSource = data;
                AjustarColumnasDataGridView();
            }
            else
            {
                dgvResultados.Visible = true;
                dgvResultados.DataSource = data;
                AjustarColumnasDataGridView();
            }
        }

        private void AjustarColumnasDataGridView()
        {
            if (dgvResultados.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvResultados.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void MostrarCarga(bool mostrar)
        {
            panelCarga.Visible = mostrar;
            panelCarga.BringToFront();
            this.Refresh();
            Application.DoEvents();
        }

        #endregion

        #region Validaciones

        private bool ValidarFiltros()
        {
            if (dtpDesde.Value > dtpHasta.Value)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbTipoReporte.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDesde.Value > dtpHasta.Value)
                dtpHasta.Value = dtpDesde.Value;
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHasta.Value < dtpDesde.Value)
                dtpDesde.Value = dtpHasta.Value;
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTituloReporte();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportarPDF();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        #endregion

        #region Métodos de Exportación

        private void ExportarPDF()
        {
            if (reporteDataTable == null || reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Primero debe generar un reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.FileName = $"Reporte_{tipoReporteActual}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MostrarCarga(true);
                try
                {
                    CrearPDF(saveFileDialog.FileName);
                    MessageBox.Show($"PDF generado exitosamente:\n{saveFileDialog.FileName}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el PDF:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    MostrarCarga(false);
                }
            }
        }

        private void CrearPDF(string rutaArchivo)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            // Título - MÁS LIMPIO
            Paragraph titulo = new Paragraph($"Reporte: {tipoReporteActual}",
                new PdfFont(PdfFont.FontFamily.HELVETICA, 18, PdfFont.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            doc.Add(new Paragraph(" "));

            // Subtítulo con fechas - MÁS LIMPIO
            Paragraph fechas = new Paragraph($"Período: {dtpDesde.Value:dd/MM/yyyy} - {dtpHasta.Value:dd/MM/yyyy}",
                new PdfFont(PdfFont.FontFamily.HELVETICA, 12, PdfFont.NORMAL));
            fechas.Alignment = Element.ALIGN_CENTER;
            doc.Add(fechas);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            // Tabla
            PdfPTable tabla = new PdfPTable(reporteDataTable.Columns.Count);
            tabla.WidthPercentage = 100;

            // Encabezados - MÁS LIMPIO
            foreach (DataColumn col in reporteDataTable.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName,
                    new PdfFont(PdfFont.FontFamily.HELVETICA, 10, PdfFont.BOLD)));
                cell.BackgroundColor = new BaseColor(0, 122, 204);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 5;
                tabla.AddCell(cell);
            }

            // Datos - MÁS LIMPIO
            foreach (DataRow row in reporteDataTable.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item?.ToString() ?? "",
                        new PdfFont(PdfFont.FontFamily.HELVETICA, 9, PdfFont.NORMAL)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 5;
                    tabla.AddCell(cell);
                }
            }

            doc.Add(tabla);

            // Pie de página - MÁS LIMPIO
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.ITALIC)));

            doc.Close();
        }

        private void Imprimir()
        {
            if (reporteDataTable == null || reporteDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Primero debe generar un reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Funcionalidad de impresión en desarrollo.\nPor favor use la exportación a PDF para imprimir.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Métodos Auxiliares

        private void ActualizarTituloReporte()
        {
            if (cmbTipoReporte.SelectedIndex >= 0)
            {
                lblTituloReporte.Text = cmbTipoReporte.SelectedItem.ToString();
            }
        }

        #endregion
    }
}