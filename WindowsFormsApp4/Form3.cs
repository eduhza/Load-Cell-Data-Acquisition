using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using MathNet.Numerics;


namespace BioMensurae
{
    public partial class Form3 : Form
    {
        private SerialPort MySerial = new SerialPort();
        string pathCalib = @"CalibConfig.txt";
        List<double> Dados = new List<double>();
        List<double> AmostraKG = new List<double>();
        List<double> AmostraMV = new List<double>();
        List<double> AmostraLida = new List<double>();
        List<string> Leitura = new List<string>();

        List<double> LinRegEq = new List<double>();

        string tipoSelected = "";
        bool IsSampling = false;
        bool Calibrando = true;
        bool SampleMode = false;
        bool PrimeiraLeitura = false;
        DateTime tSample = DateTime.Now;
        int nSample = 0;

        bool tarado = false;

        double calibPeso = 0;
        double coordX = 0;
        double coordZ = 0;

        //double SLOPE = 0;
        //double INTERCEPT = 0;
        //double RSQR = 0;

        public Form3()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);

            Application.EnableVisualStyles();
            MapRaw(50, 50);
            LinRegRaw();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Leitura.Clear();
                AmostraLida.Clear();
                DateTime tUpdate = DateTime.Now.AddSeconds(1);

                BackgroundWorker worker = sender as BackgroundWorker;
                UpdateCalibLog("Fazendo leitura dos dados...");

                while (true)
                {
                    if (worker.CancellationPending == true) //Assure worker will stop when CancelAsync is called
                    {
                        e.Cancel = true;
                        break;
                    }

                    /* Após configurar a placa, o sistema começa a fazer a leitura da porta COM
                     * e a cada (1) segundo todo o dado é decodificado e o valor médio do segundo
                     * apresentado no ComboBox LeituraBox. Após a primeira coleta de calibração a 
                     * variável SampleMode se responsabiliza por não atualizar mais o LeituraBox */
                    if (!IsSampling && !SampleMode)
                    {
                        if (MySerial.IsOpen)
                        {
                            try
                            {
                                string a = MySerial.ReadExisting();
                                Leitura.Add(a);
                            }
                            catch (Exception) { continue; }

                            if (DateTime.Now >= tUpdate)
                            {
                                string Tudo = String.Join("", Leitura);
                                Tudo = Tudo.Substring(Tudo.IndexOf(',') + 1); //Discarta primeira amostra pois pode vir quebrada
                                Tudo = Tudo.Substring(0, Tudo.LastIndexOf(',')-1); //Discarta última amostra pois pode vir quebrada

                                try { 
                                foreach (string str1 in Tudo.Split(','))
                                {
                                    if (FyCheckBox.Checked && double.TryParse(str1.Split(';')[0].Replace('.', ','), out double a))
                                    {
                                        AmostraLida.Add(a);
                                    }
                                    else if (MxCheckBox.Checked && double.TryParse(str1.Split(';')[1].Replace('.', ','), out double b))
                                    {
                                        AmostraLida.Add(b);
                                    }
                                    else if (MzCheckBox.Checked && double.TryParse(str1.Split(';')[2].Replace('.', ','), out double c))
                                    {
                                        AmostraLida.Add(c);
                                    }
                                }
                                }
                                catch(Exception ex) { MessageBox.Show("Erro: " + ex.Message + "AAAAA"); }
                                if (AmostraLida.Count > 0)
                                {
                                    UpdateLeituraBox((AmostraLida.Sum() / AmostraLida.Count()).ToString("N4").Replace(",", "."));
                                    AmostraLida.Clear();
                                    tUpdate = DateTime.Now.AddSeconds(1);
                                }
                                Leitura.Clear();
                                AmostraLida.Clear();
                            }
                        }
                    }

                    /* Ao clicar em "Adicionar Leitura" o sistema, em primeiro momento, cria as variáveis e indica que esta em coleta.
                     * A coleta de dados será realizada por tSample segundos e então os dados processados. Em seguida,
                     * a média da leitura é disponibilizada para armazenamento e pós processamento (atualização dos gráficos, 
                     * equações, et al.). */

                    if (IsSampling)
                    {
                        if (Calibrando) //CONFIGURA VARIÁVEIS DA CALIBRAÇÃO
                        {
                            //tarado = false;
                            AmostraLida.Clear();
                            Leitura.Clear();
                            SampleMode = true;
                            AmostraKG.Clear();
                            AmostraMV.Clear();
                            Calibrando = false;
                            LinRegEq.Clear();
                        }
                        if (PrimeiraLeitura) //LIMPA BUFFER ANTES DE CADA COLETA
                        {
                            MySerial.DiscardOutBuffer();
                            MySerial.DiscardInBuffer();
                            PrimeiraLeitura = false;
                        }
                        if (MySerial.IsOpen)
                        {
                            try
                            {
                                string a = MySerial.ReadExisting();
                                Leitura.Add(a);
                            }
                            catch (Exception) { continue; }

                            if (DateTime.Now >= tSample)
                            {
                                string Tudo = String.Join("", Leitura);
                                Tudo = Tudo.Substring(Tudo.IndexOf(',') + 1); //Discarta primeira amostra pois pode vir quebrada
                                Tudo = Tudo.Substring(0, Tudo.LastIndexOf(',') - 1); //Discarta última amostra pois pode vir quebrada
                                foreach (string str1 in Tudo.Split(','))
                                { //double.TryParse remove linhas vazias, só aceita doubles.
                                    if (FyCheckBox.Checked && double.TryParse(str1.Split(';')[0].Replace('.', ','), out double a))
                                        AmostraLida.Add(a);
                                    else if (MxCheckBox.Checked && double.TryParse(str1.Split(';')[1].Replace('.', ','), out double b))
                                        AmostraLida.Add(b);
                                    else if (MzCheckBox.Checked && double.TryParse(str1.Split(';')[2].Replace('.', ','), out double c))
                                        AmostraLida.Add(c);
                                }

                                if (AmostraLida.Count > 0)
                                {
                                    try
                                    {
                                        double KgTemp = calibPeso;
                                        AmostraMV.Add(AmostraLida.Sum() / AmostraLida.Count());
                                        //AmostraKG.Add(calibPeso);

                                        if (FyCheckBox.Checked)
                                        {
                                            AmostraKG.Add(KgTemp);
                                            LinRegEq = UpdateLinRegEquation(AmostraKG.ToArray(), AmostraMV.ToArray());
                                        }
                                        else if (MxCheckBox.Checked)
                                        {
                                            AmostraKG.Add(KgTemp * coordZ);
                                            LinRegEq = UpdateLinRegEquation(AmostraKG.ToArray(), AmostraMV.ToArray());
                                        }
                                        else if (MzCheckBox.Checked)
                                        {
                                            AmostraKG.Add(KgTemp * coordX);
                                            LinRegEq = UpdateLinRegEquation(AmostraKG.ToArray(), AmostraMV.ToArray());
                                        }

                                        UpdateLeituraBox(AmostraMV.Last().ToString("N4").Replace(",", "."));

                                        UpdateLinRegChart(AmostraKG, AmostraMV);
                                        UpdateMapXZChart(coordX, coordZ);
                                        UpdateCalibLog("Amostra Nº:" 
                                            + AmostraMV.Count().ToString() 
                                            + ": mV: " + Math.Round(AmostraMV.Last(), 3).ToString() 
                                            + "\tPeso: " + KgTemp.ToString());

                                        if (AmostraMV.Count() > 2)
                                        {
                                            this.BeginInvoke(new Action(() =>
                                            {
                                                Save_Button.Enabled = true;
                                            }));
                                        }
                                        //}
                                        IsSampling = false;
                                    }
                                    catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message + " - GetValue1"); }
                                }
                                AmostraLida.Clear();
                                Leitura.Clear();
                            }

                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message + " - SampleIN1"); }
        }

        void UpdateLeituraBox(string texto)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (tipoSelected == "1D - 500x500")
                    LeituraBox.Text = "Fy: " + texto + " mV";
                else
                {
                    if (FyCheckBox.Checked) { LeituraBox.Text = "Fy: " + texto + Environment.NewLine + " mV"; }
                    if (MxCheckBox.Checked) { LeituraBox.Text = "Mx: " + texto + Environment.NewLine + " mV"; }
                    if (MzCheckBox.Checked) { LeituraBox.Text = "Mz: " + texto + Environment.NewLine + " mV"; }
                }

                if (Dados.Count() > 0)
                {
                    LeituraBox.ForeColor = Color.Black;
                    LeituraBox.BackColor = Color.Red;
                }
            }));
        }

        void UpdateCalibLog(string texto)
        {
            this.BeginInvoke(new Action(() =>
            {
                CalibLogTextBox.AppendText(texto + Environment.NewLine);
            }));
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) // This event handler updates the progress.
        {

        }
        void MapRaw(int X, int Z)
        {
            try
            {
                MapXZ.Series.Clear();
                MapXZ.ChartAreas[0].CursorX.IsUserEnabled = false;
                MapXZ.ChartAreas[0].CursorY.IsUserEnabled = false;
                MapXZ.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                MapXZ.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                MapXZ.ChartAreas[0].CursorX.AutoScroll = false;
                MapXZ.ChartAreas[0].CursorY.AutoScroll = false;
                MapXZ.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                MapXZ.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                Series s1 = new Series("Borda");
                for (int j = 0; j < 6; j++)
                {
                    Series s = new Series();
                    for (int i = 0; i < 50 + 1; i++)
                    {
                        if (j == 0) { s.Points.AddXY((X / 2), i - (Z / 2)); }
                        if (j == 1) { s.Points.AddXY(-(X / 2), i - (Z / 2)); }
                        if (j == 2) { s.Points.AddXY(i - (X / 2), (Z / 2)); }
                        if (j == 3) { s.Points.AddXY(i - (X / 2), -(Z / 2)); }
                        if (j == 4) { s.Points.AddXY(-(X / 2) + i, -(Z / 2) + i); }
                        if (j == 5) { s.Points.AddXY(-(X / 2) + i, (Z / 2) - i); }
                    }
                    MapXZ.Series.Add(s);
                    if (j == 4 || j == 5)
                    {
                        MapXZ.Series[j].ChartType = SeriesChartType.Line;
                        MapXZ.Series[j].Color = Color.Black;
                        MapXZ.Series[j].BorderDashStyle = ChartDashStyle.Dash;
                        MapXZ.Series[j].BorderWidth = 1;
                    }
                    else
                    {
                        MapXZ.Series[j].ChartType = SeriesChartType.Line;
                        MapXZ.Series[j].Color = Color.Blue;
                        MapXZ.Series[j].BorderWidth = 3;
                    }
                }
                MapXZ.ChartAreas[0].AxisX.Title = "X (cm)";
                MapXZ.ChartAreas[0].AxisY.Title = "Z (cm)";
                MapXZ.ChartAreas[0].AxisX.Maximum = 30;
                MapXZ.ChartAreas[0].AxisY.Maximum = 30;
                MapXZ.ChartAreas[0].AxisX.Minimum = -30;
                MapXZ.ChartAreas[0].AxisY.Minimum = -30;
                MapXZ.ChartAreas[0].AxisX.Interval = 10;
                MapXZ.ChartAreas[0].AxisY.Interval = 10;
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        void UpdateMapXZChart(double X, double Z)
        {
            this.BeginInvoke(new Action(() =>
            {
                //LinRegChart.Series.Clear();
                Series s = new Series();
                s.Points.AddXY(X * 100, Z * 100);
                MapXZ.Series.Add(s);
                MapXZ.Series[6 + nSample].ChartType = SeriesChartType.Point;
                MapXZ.Series[6 + nSample].MarkerStyle = MarkerStyle.Star5;
                MapXZ.Series[6 + nSample].MarkerSize = 15;
                MapXZ.Series[6 + nSample].Color = Color.Red;
                MapXZ.Series[6 + nSample].BorderWidth = 3;
                nSample = nSample + 1;
            }));

        }

        void LinRegRaw()
        {
            try
            {
                LinRegChart.Series.Clear();
                LinRegChart.ChartAreas[0].CursorX.IsUserEnabled = false;
                LinRegChart.ChartAreas[0].CursorY.IsUserEnabled = false;
                LinRegChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                LinRegChart.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                LinRegChart.ChartAreas[0].CursorX.AutoScroll = false;
                LinRegChart.ChartAreas[0].CursorY.AutoScroll = false;
                LinRegChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                LinRegChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

                double[] KGinit = { 0, 10, 20, 30, 40, 50, 60 };
                double[] MVinit = { 0, 5, 10, 15, 20, 25, 30 };
                double[] p = Fit.Polynomial(MVinit, KGinit, 2);
                double rSqr = GoodnessOfFit.RSquared(MVinit.Select(x => p[1] * x + p[2]), KGinit);
                EquationBox.Text = "Fy: " + Math.Round(p[1], 5) + "x + (" + Math.Round(p[2], 5) + ") [Kg] --> " + "R2: " + Math.Round(rSqr, 5);

                Series s = new Series();
                for (int i = 0; i < KGinit.Count(); i++)
                {
                    s.Points.AddXY(KGinit[i], MVinit[i]);
                }
                LinRegChart.Series.Add(s);
                LinRegChart.Series[0].ChartType = SeriesChartType.Point;
                LinRegChart.Series[0].Color = Color.Blue;
                LinRegChart.Series[0].BorderWidth = 3;
                LinRegChart.ChartAreas[0].AxisX.Title = "Carga Vertical (KG)";
                LinRegChart.ChartAreas[0].AxisY.Title = "Tensão (mV)";

                LinRegChart.ChartAreas[0].AxisX.Maximum = 60;
                LinRegChart.ChartAreas[0].AxisX.Minimum = 0;
                LinRegChart.ChartAreas[0].AxisX.Interval = 10;
                LinRegChart.ChartAreas[0].AxisY.Maximum = 30;
                LinRegChart.ChartAreas[0].AxisY.Minimum = 0;
                LinRegChart.ChartAreas[0].AxisY.Interval = 5;
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        void UpdateLinRegChart(List<double> KG, List<double> MV)
        {
            this.BeginInvoke(new Action(() =>
            {
                LinRegChart.Series.Clear();
                Series s = new Series();

                for (int i = 0; i < KG.Count(); i++)
                {
                    s.Points.AddXY(KG[i], MV[i]);
                }

                LinRegChart.Series.Add(s);
                LinRegChart.Series[0].ChartType = SeriesChartType.Point;
                LinRegChart.Series[0].Color = Color.Red;
                LinRegChart.Series[0].BorderWidth = 3;

                if (KG.Max() == 0)
                    LinRegChart.ChartAreas[0].AxisX.Maximum = 60;
                else
                    LinRegChart.ChartAreas[0].AxisX.Maximum = Math.Round(KG.Max() * 1.025);

                if (KG.Min() > 0)
                    LinRegChart.ChartAreas[0].AxisX.Minimum = Math.Round(KG.Min() * 0.975);
                else
                    LinRegChart.ChartAreas[0].AxisX.Minimum = Math.Round(KG.Min() * 1.025);

                LinRegChart.ChartAreas[0].AxisY.Maximum = Math.Round(MV.Max() * 1.005);
                if (MV.Min() > 0)
                    LinRegChart.ChartAreas[0].AxisY.Minimum = Math.Round(MV.Min() * 0.995);
                else
                    LinRegChart.ChartAreas[0].AxisY.Minimum = Math.Round(MV.Min() * 1.005);
                if (MV.Max() - MV.Min() > 0)
                    LinRegChart.ChartAreas[0].AxisY.Interval = Math.Abs((MV.Max() - MV.Min()) / 5);
                LinRegChart.ChartAreas[0].AxisX.Interval = 10;

                /* Curva de Tendência */
                Series s2 = new Series();

                LinRegChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}"; //or "0.00"
                LinRegChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}"; //or "0.00"
            }));

        }

        List<double> UpdateLinRegEquation(double[] KG, double[] MV)
        {
            List<double> linregTemp = new List<double>();
            if (KG.Count() > 2)
            {
                double[] p = Fit.Polynomial(MV, KG, 1);
                double rSqr = GoodnessOfFit.RSquared(MV.Select(x => p[1] * x + p[0]), KG);
                linregTemp.Add(p[1]);
                linregTemp.Add(p[0]);
                linregTemp.Add(rSqr);
                this.BeginInvoke(new Action(() =>
                {
                    if (tipoSelected == "1D - 500x500")
                        EquationBox.Text = "Fy: " + Math.Round(linregTemp[0], 5) + "x + (" + Math.Round(linregTemp[1], 5) + ") [Kg] --> " + "R2: " + Math.Round(linregTemp[2], 5);
                    //EquationBox.Text = "Fy: " + Math.Round(SLOPE, 5) + "x + (" + Math.Round(INTERCEPT, 5) + ") [Kg] --> " + "R2: " + Math.Round(RSQR, 5);
                    else if (tipoSelected == "3D - 500x500")
                    {
                        if (FyCheckBox.Checked)
                            EquationBox.Text = "Fy: " + Math.Round(linregTemp[0], 5) + "x + (" + Math.Round(linregTemp[1], 5) + ") [Kg] --> " + "R2: " + Math.Round(linregTemp[2], 5);
                        if (MxCheckBox.Checked)
                            EquationBox.Text = "Mx: " + Math.Round(linregTemp[0], 5) + "x + (" + Math.Round(linregTemp[1], 5) + ") [Kg] --> " + "R2: " + Math.Round(linregTemp[2], 5);
                        if (MzCheckBox.Checked)
                            EquationBox.Text = "Mz: " + Math.Round(linregTemp[0], 5) + "x + (" + Math.Round(linregTemp[1], 5) + ") [Kg] --> " + "R2: " + Math.Round(linregTemp[2], 5);
                    }
                }));
            }
            else
            {
                this.BeginInvoke(new Action(() =>
                {
                    EquationBox.Text = "São necessários pelo menos 3 medidas.";
                }));
            }
            return linregTemp;
        }

        private void SerialPortsBox_Click(object sender, EventArgs e)
        {
            SerialPortsBox.Items.Clear();
            SerialPortsBox.Items.AddRange(SerialPort.GetPortNames());
        }

        private void Config_Button_Click(object sender, EventArgs e)
        {
            Upload_Schetch();
            ConnectarPlataforma();
            tipoSelected = TipoBox.Text;
            try
            {
                Application.DoEvents();
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync(); // Start the asynchronous operation. 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                MySerial.Close();
            }
            LeituraBox.Enabled = true;
            PesoBox.Enabled = true;
            GetValueButton.Enabled = true;
            PesoBox.Text = "0";
            PesoBox.Select();
            TipoBox.Enabled = false;
            SerialPortsBox.Enabled = false;
            Config_Button.Enabled = false;
            if (tipoSelected == "3D - 500x500")
            {
                FyCheckBox.Enabled = true;
                MxCheckBox.Enabled = true;
                MzCheckBox.Enabled = true;
                if (!FyCheckBox.Checked)
                {
                    ZBox.Enabled = true;
                    XBox.Enabled = true;
                }
                ZBox.Text = "0";
                XBox.Text = "0";
            }
        }

        void Upload_Schetch()
        {
            if (MySerial != null) //Make sure MySerial port is closed
            {
                if (MySerial.IsOpen) //if open, close
                {
                    MySerial.Close();
                    Task.Delay(2000).Wait();
                    MySerial = null;
                }
            }
            //if (MySerial.IsOpen) { MySerial.Close(); }
            string tipo = "";
            if (TipoBox.Text == "1D - 500x500") { tipo = @"calib1D.ino.eightanaloginputs.hex"; }
            else if (TipoBox.Text == "3D - 500x500") { tipo = @"calib3D.ino.eightanaloginputs.hex"; }
            else if (TipoBox.Text == "6D - 400x600") { tipo = @"calib3D.ino.eightanaloginputs.hex"; }
            else if (TipoBox.Text == "") { tipo = @"calib1D.ino.eightanaloginputs.hex"; TipoBox.SelectedIndex = 0; }
            try
            {
                if (SerialPortsBox.Text == "")
                {
                    SerialPortsBox.Items.Clear();
                    SerialPortsBox.Items.AddRange(SerialPort.GetPortNames());
                    SerialPortsBox.SelectedIndex = 0;
                }

                var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = tipo,
                    PortName = SerialPortsBox.Text,
                    ArduinoModel = ArduinoModel.NanoR3
                });
                uploader.UploadSketch();

                UpdateCalibLog("Placa configurada para plataforma " + TipoBox.Text);

            }
            catch (Exception ex)
            {
                MySerial.Close();
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        void ConnectarPlataforma()
        {
            MapRaw(50, 50);
            LinRegRaw();
            if (MySerial != null) //Make sure MySerial port is closed
            {
                if (MySerial.IsOpen) //if open, close
                {
                    MySerial.Close();
                    Task.Delay(2000).Wait();
                    MySerial = null;
                }
            }
            MySerial.PortName = SerialPortsBox.Text;
            MySerial.BaudRate = 2000000;
            MySerial.ReadTimeout = 500;
            MySerial.WriteTimeout = 100;
            MySerial.Parity = Parity.None;
            MySerial.StopBits = StopBits.One;
            MySerial.DataBits = 8;
            MySerial.Handshake = Handshake.None;
            MySerial.ReadBufferSize = 20480;
            MySerial.DtrEnable = true;
            //MySerial.NewLine = ",";

            MySerial.Open();
            Task.Delay(1500).Wait();

            if (!MySerial.IsOpen)
                UpdateCalibLog("A conexão a porta " + SerialPortsBox.Text + " falhou.");
            //CalibLogTextBox.AppendText("A conexão a porta " + SerialPortsBox.Text + " falhou." + Environment.NewLine);
            else
                UpdateCalibLog("Conectado a porta " + SerialPortsBox.Text + ".");
            //CalibLogTextBox.AppendText("Conectado a porta " + SerialPortsBox.Text + "." + Environment.NewLine);
        }

        private void CalibLogTextBox_TextChanged(object sender, EventArgs e)
        {
            CalibLogTextBox.SelectionStart = CalibLogTextBox.Text.Length;
            CalibLogTextBox.ScrollToCaret();
        }

        private void GetValueButton_Click(object sender, EventArgs e)
        {
            MzCheckBox.Enabled = false;
            MxCheckBox.Enabled = false;
            FyCheckBox.Enabled = false;
            StopCalib_Button.Enabled = true;
            if (FyCheckBox.Checked)
            {
                calibPeso = Convert.ToDouble(PesoBox.Text.Replace('.', ','));
                coordX = 0;
                coordZ = 0;
                XBox.Enabled = false;
                ZBox.Enabled = false;
                if (PesoBox.Text != "" && Double.TryParse(PesoBox.Text.Replace('.', ','), out double nun1))
                {
                    tSample = DateTime.Now.AddSeconds(2);
                    IsSampling = true;
                    PrimeiraLeitura = true;
                    Dados.Clear();
                    Leitura.Clear();
                }
            }
            else if (MxCheckBox.Checked || MzCheckBox.Checked)
            {
                calibPeso = Convert.ToDouble(PesoBox.Text.Replace('.', ','));
                coordX = Convert.ToDouble(XBox.Text) / 100;
                coordZ = Convert.ToDouble(ZBox.Text) / 100;
                if (PesoBox.Text != "" && Double.TryParse(PesoBox.Text.Replace('.', ','), out double nun1) && XBox.Text.Replace('.', ',') != "" && Double.TryParse(XBox.Text.Replace('.', ','), out double nun2) && ZBox.Text.Replace('.', ',') != "" && Double.TryParse(ZBox.Text.Replace('.', ','), out double nun3))
                {
                    tSample = DateTime.Now.AddSeconds(2);
                    IsSampling = true;
                    PrimeiraLeitura = true;
                    Dados.Clear();
                    Leitura.Clear();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            if (MySerial.IsOpen)
                MySerial.Close();
            TipoBox.Enabled = true;
            SerialPortsBox.Enabled = true;
            Config_Button.Enabled = true;
            LeituraBox.Enabled = false;
            PesoBox.Enabled = false;
            FyCheckBox.Enabled = false;
            MxCheckBox.Enabled = false;
            MzCheckBox.Enabled = false;
            XBox.Enabled = false;
            ZBox.Enabled = false;
            GetValueButton.Enabled = false;
            StopCalib_Button.Enabled = false;
            Save_Button.Enabled = false;

            IsSampling = false;
            Calibrando = true;
            SampleMode = false;

            LeituraBox.ForeColor = SystemColors.WindowText;
            LeituraBox.BackColor = SystemColors.Control;
            LeituraBox.Text = "";

            UpdateCalibLog("Plataforma Calibrada: Fy = " 
                + Math.Round(LinRegEq[0],5).ToString() 
                + "*mV + " 
                + Math.Round(LinRegEq[1], 5).ToString() 
                + " --> R2 = " 
                + Math.Round(LinRegEq[2], 5).ToString() + ".");
            

            nSample = 0;

            string[] stringSeparators = new string[] { "\r\n" };
            StreamReader reader = new StreamReader(pathCalib);
            string input = reader.ReadToEnd();
            string[] bla = input.Split(stringSeparators, StringSplitOptions.None);
            reader.Close();

            TextWriter tw = new StreamWriter(pathCalib);

            if (tipoSelected == "1D - 500x500")
                bla[1] = "Fy: " + LinRegEq[0].ToString().Replace(",", ".") + "," + LinRegEq[1].ToString().Replace(",", ".") + "," + LinRegEq[2].ToString().Replace(",", ".");
            else if (tipoSelected == "3D - 500x500")
            {
                if (FyCheckBox.Checked)
                    bla[3] = "Fy: " + LinRegEq[0].ToString().Replace(",", ".") + "," + LinRegEq[1].ToString().Replace(",", ".") + "," + LinRegEq[2].ToString().Replace(",", ".");
                else if (MxCheckBox.Checked)
                    bla[4] = "Mx: " + LinRegEq[0].ToString().Replace(",", ".") + "," + LinRegEq[1].ToString().Replace(",", ".") + "," + LinRegEq[2].ToString().Replace(",", ".");
                else if (MzCheckBox.Checked)
                    bla[5] = "Mz: " + LinRegEq[0].ToString().Replace(",", ".") + "," + LinRegEq[1].ToString().Replace(",", ".") + "," + LinRegEq[2].ToString().Replace(",", ".");
            }
            tw.WriteLine(string.Join("\r\n", bla));
            tw.Close();
        }

        private void PesoBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow two decimal point
            if (Regex.IsMatch(PesoBox.Text, @"\.\d\d\d\d"))
            {
                e.Handled = true;
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            if (MySerial.IsOpen) { MySerial.Close(); }
        }

        private void XBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && !((e.KeyChar != '-') || XBox.Text == ""))
            {
                e.Handled = true;
            }
            // only allow two decimal point
            if (Regex.IsMatch(XBox.Text, @"\.\d\d") || Regex.IsMatch(XBox.Text, @"-\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void ZBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && !((e.KeyChar != '-') || ZBox.Text == ""))
            {
                e.Handled = true;
            }
            // only allow two decimal point
            if (Regex.IsMatch(ZBox.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void FyCheckBox_Click(object sender, EventArgs e)
        {
            FyCheckBox.Checked = true;
            MxCheckBox.Checked = false;
            MzCheckBox.Checked = false;
            XBox.Enabled = false;
            ZBox.Enabled = false;
        }

        private void MxCheckBox_Click(object sender, EventArgs e)
        {
            FyCheckBox.Checked = false;
            MxCheckBox.Checked = true;
            MzCheckBox.Checked = false;
            XBox.Enabled = true;
            ZBox.Enabled = true;
        }

        private void MzCheckBox_Click(object sender, EventArgs e)
        {
            FyCheckBox.Checked = false;
            MxCheckBox.Checked = false;
            MzCheckBox.Checked = true;
            XBox.Enabled = true;
            ZBox.Enabled = true;
        }

        private void StopCalib_Button_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tem certeza que deseja finalizar a calibração sem salvar os dados?",
                                                "Finalizar sem salvar!",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                backgroundWorker1.CancelAsync();
                if (MySerial.IsOpen)
                    MySerial.Close();

                TipoBox.Enabled = true;
                SerialPortsBox.Enabled = true;
                Config_Button.Enabled = true;
                LeituraBox.Enabled = false;
                PesoBox.Enabled = false;
                FyCheckBox.Enabled = false;
                MxCheckBox.Enabled = false;
                MzCheckBox.Enabled = false;
                XBox.Enabled = false;
                ZBox.Enabled = false;
                GetValueButton.Enabled = false;
                StopCalib_Button.Enabled = false;
                Save_Button.Enabled = false;

                IsSampling = false;
                Calibrando = true;
                SampleMode = false;

                MapRaw(50, 50);
                LinRegRaw();
                nSample = 0;

                LeituraBox.ForeColor = SystemColors.WindowText;
                LeituraBox.BackColor = SystemColors.Control;
                LeituraBox.Text = "";

                UpdateCalibLog("Calibração encerrada. Por favor, iniciar nova calibração.");
            }
            else
            {
                UpdateCalibLog("Para salvar a calibração, clique em \"Salvar Calibração\".");
            }
        }

        private void PesoBox_Click(object sender, EventArgs e)
        {
            PesoBox.Text = "";
        }

        private void XBox_Click(object sender, EventArgs e)
        {
            XBox.Text = "";
        }

        private void ZBox_Click(object sender, EventArgs e)
        {
            ZBox.Text = "";
        }
    }
}
