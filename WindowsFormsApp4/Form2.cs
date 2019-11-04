using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        public Form2(List<List<double>> dados, int k, string t) //(List<double> sA, List<double> sB, List<double> sC, int nS)
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Chart.Series.Clear();
            Chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            Chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            Chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            Chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            Chart.ChartAreas[0].CursorX.AutoScroll = true;
            Chart.ChartAreas[0].CursorY.AutoScroll = true;
            Chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            Chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            Chart.ChartAreas[0].AxisY.IsStartedFromZero = false;

            Chart.MouseWheel += Chart_MouseWheel;
            Chart.MouseMove += Chart_MouseMove;

            Series s1 = new Series("Sensor A");
            Series s2 = new Series("Sensor B");
            Series s3 = new Series("Sensor C");
            Series s = new Series();

            try
            {
                for (int j = 0; j < dados.Count() - 1; j++) //para cada canal
                {
                    for (int i = 0; i < dados[0].Count(); i++) //para cada ponto
                    {
                        s.Points.AddXY(dados[dados.Count() - 1][i], dados[j][i]);
                    }
                    Chart.Series.Add(s);
                }

                Chart.ChartAreas[0].AxisX.Title = "Segundos";
                Chart.Legends.Clear();
                Chart.Series[0].Name = "Sensor A";
                Chart.Series[0].ChartType = SeriesChartType.Line;

                if (t == "1D")
                {
                    Chart.Titles.Add("Força Vertical"); Chart.ChartAreas[0].AxisY.Title = "Peso [Kg] ";
                    Chart.Series[0].Color = Color.Blue;
                }
                else if (t == "Bi")
                {
                    if(k == 0)
                    {
                        Chart.Titles.Add("Força Vertical - Plataforma 3D"); Chart.ChartAreas[0].AxisY.Title = "Peso [Kg] ";
                        Chart.Series[0].Color = Color.Blue;
                    }
                    else
                    {
                        Chart.Titles.Add("Força Vertical - Plataforma 1D"); Chart.ChartAreas[0].AxisY.Title = "Peso [Kg] ";
                        Chart.Series[0].Color = Color.Green;
                    }
                }
                else if (t == "3D")
                {
                    if (k == 0)
                    {
                        Chart.Titles.Add("Força Vertical"); Chart.ChartAreas[0].AxisY.Title = "Peso [Kg] ";
                        Chart.Series[0].Color = Color.Blue;
                    }
                    else if (k == 1)
                    {
                        Chart.Titles.Add("Momento em X"); Chart.ChartAreas[0].AxisY.Title = "Kg*m ";
                        Chart.Series[0].Color = Color.Red;
                    }
                    else
                    {
                        Chart.Titles.Add("Momento em Z"); Chart.ChartAreas[0].AxisY.Title = "Kg*m ";
                        Chart.Series[0].Color = Color.Green;
                    }
                }

                //if (dados.Count() == 3) //Plataforma 2D
                //{
                //    Chart.Series[0].Name = "Sensor A";
                //    Chart.Series[1].Name = "Sensor B";
                //    Chart.Series[0].ChartType = SeriesChartType.Line;
                //    Chart.Series[1].ChartType = SeriesChartType.Line;
                //    Chart.Titles.Add("Plataforma 2D");
                //}
                //if (dados.Count() == 4) //Plataforma 3D ou Bilateral
                //{
                //    Chart.Series[0].Name = "Sensor A";
                //    Chart.Series[1].Name = "Sensor B";
                //    Chart.Series[2].Name = "Sensor C";
                //    Chart.Series[0].ChartType = SeriesChartType.Line;
                //    Chart.Series[1].ChartType = SeriesChartType.Line;
                //    Chart.Series[2].ChartType = SeriesChartType.Line;
                //    Chart.Titles.Add("Plataforma 3D");
                //}
                //Chart.ChartAreas[0].AxisX.Interval = 1;
                Chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}"; //or "0.00"
                Chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}"; //or "0.00"
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
        public SeriesCollection Series { get; set; }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = Chart.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        if (Math.Abs(pos.X - pointXPixel) < 2 &&
                            Math.Abs(pos.Y - pointYPixel) < 2)
                        {
                            tooltip.Show("X=" + prop.XValue + ", Y=" + prop.YValues[0], this.Chart,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }
    }
}
