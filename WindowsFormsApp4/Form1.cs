using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using System.Text.RegularExpressions;
using BioMensurae;
using Microsoft.Win32;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public partial class BioMensurae : Form
    {
        private SerialPort MySerial = new SerialPort();
        DateTime startTime = DateTime.Now;
        List<List<double>> dados = new List<List<double>>();
        List<double> SensorA = new List<double>();
        List<double> SensorB = new List<double>();
        List<double> SensorC = new List<double>();
        List<double> mvDataA = new List<double>();
        List<double> mvDataB = new List<double>();
        List<double> mvDataC = new List<double>();
        List<double> Tempo = new List<double>();
        int nSens = 0;
        int div1 = 0;

        List<double> wSensorA = new List<double>();
        double sensorAmm = 0;
        List<double> wSensorB = new List<double>();
        double sensorBmm = 0;
        List<double> wSensorC = new List<double>();
        double sensorCmm = 0;
        readonly int mmSize = 50;
        readonly double gravidade = 9.80665 ;

        List<byte> byteDivisor = new List<byte>() { 13, 13, 13, 13 }; //uControler /r /r /r /r
        static readonly double VREF = 2.5;
        static readonly int bit = 32;
        static double resolution_mV = (VREF / Math.Pow(2, (bit - 1))) * 1000;
        string pathTxt = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas\txt\";
        string pathSad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas\sad\";
        string pathLog = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas\log\";
        string file_name = "";
        bool stopFlag = false;
        bool first = true;
        //Timer t = new Timer();
        private static System.Timers.Timer aTimer;

        /* Variáveis de calibração*/
        List<List<double>> LinReg = new List<List<double>>();


        public BioMensurae()
        {
            InitializeComponent();
            CreateFolders();

            backgroundWorker2.DoWork += new DoWorkEventHandler(BackgroundWorker2_DoWork);
            
            Timer_config();
            LiveChart.Series[0].Points.AddXY(0, 0);
            LiveChart.Series[0].Points.AddXY(1, 5);
            LiveChart.Series[0].Points.AddXY(2, 10);
            LiveChart.Series[0].Points.AddXY(3, 8);
            LiveChart.Series[0].Points.AddXY(4, 6);
            LiveChart.Series[0].BorderWidth = 3;
            LiveChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            LiveChart.ChartAreas[0].AxisX.RoundAxisValues();
            LiveChart.ChartAreas[0].AlignmentOrientation = AreaAlignmentOrientations.All;
            LiveChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            LiveChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            MapRaw(50, 50);
        }

        List<List<double>> GetCalibConfig()
        {
            List<List<double>> configs = new List<List<double>>();
            TextReader tw = new StreamReader(@"CalibConfig.txt");
            string a = tw.ReadToEnd();
            tw.Close();

            if (TipoBox.Text == "Plataforma 1D")
            {
                List<double> configA = new List<double>();
                configA.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[0].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[1].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[2].Replace('.', ',')));
                //configA.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[3].Replace('.', ',')));
                configs.Add(configA);
            }

            else if (TipoBox.Text == "Bilateral")
            {
                List<double> configA = new List<double>();
                List<double> configB = new List<double>();

                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[0].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[1].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[2].Replace('.', ',')));
                configs.Add(configA);

                configB.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[0].Replace('.', ',')));
                configB.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[1].Replace('.', ',')));
                configB.Add(Convert.ToDouble(a.Split('\r')[1].Substring(5).Split(',')[2].Replace('.', ',')));
                configs.Add(configB);
            }

            else if (TipoBox.Text == "Plataforma 3D")
            {
                List<double> configA = new List<double>();
                List<double> configB = new List<double>();
                List<double> configC = new List<double>();

                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[0].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[1].Replace('.', ',')));
                configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[2].Replace('.', ',')));
                //configA.Add(Convert.ToDouble(a.Split('\r')[3].Substring(5).Split(',')[3].Replace('.', ',')));
                configs.Add(configA);

                configB.Add(Convert.ToDouble(a.Split('\r')[4].Substring(5).Split(',')[0].Replace('.', ',')));
                configB.Add(Convert.ToDouble(a.Split('\r')[4].Substring(5).Split(',')[1].Replace('.', ',')));
                configB.Add(Convert.ToDouble(a.Split('\r')[4].Substring(5).Split(',')[2].Replace('.', ',')));
                //configB.Add(Convert.ToDouble(a.Split('\r')[4].Substring(5).Split(',')[3].Replace('.', ',')));
                configs.Add(configB);

                configC.Add(Convert.ToDouble(a.Split('\r')[5].Substring(5).Split(',')[0].Replace('.', ',')));
                configC.Add(Convert.ToDouble(a.Split('\r')[5].Substring(5).Split(',')[1].Replace('.', ',')));
                configC.Add(Convert.ToDouble(a.Split('\r')[5].Substring(5).Split(',')[2].Replace('.', ',')));
                //configC.Add(Convert.ToDouble(a.Split('\r')[5].Substring(5).Split(',')[3].Replace('.', ',')));
                configs.Add(configC);
            }
            return configs;
        }

        void Timer_config()
        {
            aTimer = new System.Timers.Timer
            { Interval = 500 };
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Start();
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                SetText(clock_display, DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {
                var cronometro = DateTime.Now.Subtract(startTime);
                SetText(clock_display, cronometro.Hours.ToString().PadLeft(2, '0') + ":" +
                            cronometro.Minutes.ToString().PadLeft(2, '0') + ":" +
                            cronometro.Seconds.ToString().PadLeft(2, '0'));
                if (SensorA.Count() > 0)
                {
                    SetText(MVText, Math.Round(mvDataA.Last(), 3).ToString());
                    if (nSens == 2)
                    {
                        SetText(PesoText, Math.Round(sensorAmm + sensorBmm, 3).ToString());
                    }
                    else
                        SetText(PesoText, Math.Round(sensorAmm, 3).ToString());
                        //SetText(PesoText, Math.Round(SensorA.Last(), 3).ToString());
                }
            }
        }

        private delegate void SetTextDelegate(Label tb, string Text); //protected delegate void

        private void SetText(Label tb, string Text) //protected void
        {
            if (tb.InvokeRequired)
            {
                try
                {
                    tb.Invoke(new SetTextDelegate(SetText), tb, Text);
                    return;
                }
                catch { return; }
            }
            tb.Text = Text;
        }

        private delegate void SetProgressBarDelegate(ProgressBar tb, int value);

        private void SetProgressBar(ProgressBar tb, int value) //protected void
        {
            if (tb.InvokeRequired)
            {
                tb.Invoke(new SetProgressBarDelegate(SetProgressBar), tb, value);
                return;
            }
            tb.Value = value;
        }

        private delegate void LiveChartUpdate(Chart ch, double[] y);

        private void UpdateLiveChart(Chart ch, double[] y)
        {
            if (ch.InvokeRequired)
            {
                ch.Invoke(new LiveChartUpdate(UpdateLiveChart), ch, y);
                return;
            }
            try
            {
                for (int i = 0; i < y.Length; i++)
                {
                    if (LiveChart.Series[i].Points.Count >= 500)
                        LiveChart.Series[i].Points.RemoveAt(0);
                    LiveChart.Series[i].Points.Add(y[i]);
                }
                //LiveChart.ChartAreas[0].RecalculateAxesScale();

                //if (y.Length == 1)
                //    LiveChart.Legends.Add(new Legend("Fy (Kg)"));
                //else if (y.Length == 2)
                //{
                //    LiveChart.Legends.Add(new Legend("Fy Direita (Kg)"));
                //    LiveChart.Legends.Add(new Legend("Fy Esquerda (Kg)"));
                //}
                //else if (y.Length == 2)
                //{
                //    LiveChart.Legends.Add(new Legend({ "Expenses", "Expenses", "Expenses" }));
                //}

            }
            catch { }
        }


        private delegate void LiveCOPUpdate(Chart ch, double z, double x);

        private void UpdateCOPChart(Chart ch, double z, double x)
        {
            if (ch.InvokeRequired)
            {
                ch.Invoke(new LiveCOPUpdate(UpdateCOPChart), ch, x, z);
                return;
            }
            try
            {
                if (COPChart.Series.Count == 8)
                {
                    COPChart.Series[6].Points.Clear();
                    COPChart.Series[6].Points.AddXY(x, z);
                    COPChart.ChartAreas[0].RecalculateAxesScale();
                }
                else
                {
                    COPChart.Series.Last().Points.Clear();
                    COPChart.Series.Last().Points.AddXY(x, z);
                    COPChart.ChartAreas[0].RecalculateAxesScale();
                }
            }
            catch { }
        }


        void CreateFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas")) //"\\BioMensurae 2ADS Coletas"
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas");
                installDriver();
                Update_EventLog_TextBox("As coletas serão salvas em: " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BioMensurae DAQ Coletas");
            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + pathTxt))
                Directory.CreateDirectory(pathTxt);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + pathSad))
                Directory.CreateDirectory(pathSad);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + pathLog))
                Directory.CreateDirectory(pathLog);
        }

        private void installDriver()
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo("CH34x_Install_Windows_v3_4.exe")
            {
                //UseShellExecute = false,
                //Arguments = @"C:\Users\10522\Desktop\spssJob1.spj -production",
                //WorkingDirectory = @"C:\Program Files\IBM\SPSS\Statistics\22",
            };
            p.Start();
            p.WaitForExit();
            Thread.Sleep(1000);
        }

        private void SerialPortsBox_Click(object sender, EventArgs e)
        {
            SerialPortsBox.Items.Clear();
            SerialPortsBox.Items.AddRange(SerialPort.GetPortNames());
        }

        private void SerialPortsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SerialPortsBox.Text != "") { Record_Button.Enabled = true; }
            else { Record_Button.Enabled = false; }
        }

        void Connect_Click()
        {
            try
            {
                if (SerialPortsBox.Text == "")
                { Update_EventLog_TextBox("Por favor selecionar a porta COM conectada na plataforma."); }
                else
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
                    MySerial.PortName = SerialPortsBox.Text;
                    MySerial.BaudRate = 2000000;
                    MySerial.ReadTimeout = 100;
                    MySerial.WriteTimeout = 100;
                    MySerial.Parity = Parity.None;
                    MySerial.StopBits = StopBits.One;
                    MySerial.DataBits = 8;
                    MySerial.Handshake = Handshake.None;
                    MySerial.ReadBufferSize = 20480;
                    MySerial.DtrEnable = true;

                    MySerial.Open();
                    Task.Delay(1500).Wait();

                    if (!MySerial.IsOpen)
                        Update_EventLog_TextBox("A conexão a porta " + SerialPortsBox.Text + " falhou.");

                    SerialPortsBox.Enabled = false;
                }
            }
            catch (Exception e1) { Update_EventLog_TextBox("Erro: " + e1.ToString()); }
        }

        void Upload_Schetch()
        {
            string tipo = "";
            if (TipoBox.Text == "Plataforma 1D") { tipo = @"singleCell1262.ino.eightanaloginputs.hex"; }
            //else if (TipoBox.Text == "Plataforma 2D") { tipo = @"doubleCell1262.ino.eightanaloginputs.hex"; }
            else if (TipoBox.Text == "Bilateral" || TipoBox.Text == "Plataforma 3D") { tipo = @"3D.ino.eightanaloginputs.hex"; }
            try
            {
                var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = tipo,
                    PortName = SerialPortsBox.Text,
                    ArduinoModel = ArduinoModel.NanoR3
                });
                uploader.UploadSketch();
                Update_EventLog_TextBox("Configurado para: " + TipoBox.Text + ".");
            }
            catch { Update_EventLog_TextBox("Não foi possível configurar a placa, tente novamente."); }
        }

        private void Record_Button_Click(object sender, EventArgs e)
        {
            Plot_Button.Enabled = false;
            Plot_Button.BackColor = Color.DarkGray;
            Record_Button.Enabled = false;
            stopFlag = false;
            while (LiveChart.Series[0].Points.Count > 0)
            { 
                LiveChart.Series[0].Points.RemoveAt(0);
                LiveChart.Series[0].BorderWidth = 3;
                LiveChart.ChartAreas[0].AxisY.Minimum = -10;
                LiveChart.ChartAreas[0].AxisY.Maximum = 300;
            }

            while (LiveChart.Series[1].Points.Count > 0)
            {
                LiveChart.Series[1].Points.RemoveAt(0);
                LiveChart.Series[1].BorderWidth = 3;
                LiveChart.ChartAreas[1].AxisY.Minimum = -10;
                LiveChart.ChartAreas[1].AxisY.Maximum = 300;
            }
            while (LiveChart.Series[2].Points.Count > 0)
            {
                LiveChart.Series[2].Points.RemoveAt(0);
                LiveChart.Series[2].BorderWidth = 3;
                LiveChart.ChartAreas[2].AxisY.Minimum = -10;
                LiveChart.ChartAreas[2].AxisY.Maximum = 300;
            }

            Connect_Click();
            //Update_EventLog_TextBox("Calibração de " + calibTime + " segundos.");
            //Update_EventLog_TextBox("Aguarde o aviso sonoro para iniciar.");

            if (TipoBox.Text == "Plataforma 1D") { div1 = 12; nSens = 1; }
            else if (TipoBox.Text == "Plataforma 3D" || TipoBox.Text == "Bilateral")
            { div1 = 20;
                if (TipoBox.Text == "Plataforma 3D")
                    nSens = 3;
                else
                    nSens = 2;
            }

            if (!backgroundWorker2.IsBusy)
            {
                //ProgressBar.Maximum = 100;// Convert.ToInt32(TimeBox.Text);
                //ProgressBar.Step = 1;
                //ProgressBar.Value = 0;
                Plot_Button.Enabled = false;
                backgroundWorker2.RunWorkerAsync(); // Start the asynchronous operation.
                StatusText.Text = "ON";
                StatusText.ForeColor = System.Drawing.Color.Green;
            }
            Stop_Button.Enabled = true;
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            dados.Clear();
            stopFlag = true;
            StatusText.Text = "OFF";
            StatusText.ForeColor = Color.Red;
            Update_EventLog_TextBox("Coleta finalizada, processando dados...");

            Stop_Button.Enabled = false;
            Task.Delay(1000).Wait(); //Time to be sure both ADS has stopped asynchronous operation

            if (nSens == 1)
                dados.Add(SensorA);
            else if (nSens == 2)
            {
                dados.Add(SensorA);
                dados.Add(SensorB);
            }
            else if (nSens == 3)
            {
                dados.Add(SensorA);
                dados.Add(SensorB);
                dados.Add(SensorC);
            }
            dados.Add(Tempo);
            Save_txt(dados, GetFileName());
            Save_sad(dados, GetFileName());

            Plot_Button.Enabled = true;
            Plot_Button.BackColor = Color.Blue;
            Record_Button.Enabled = true;
            SerialPortsBox.Enabled = true;
        }

        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            List<byte> ReadSlidingWindow = new List<byte>();
            dados.Clear();
            mvDataA.Clear();
            SensorA.Clear();
            mvDataB.Clear();
            SensorB.Clear();
            mvDataC.Clear();
            SensorC.Clear();
            Tempo.Clear();
            first = true;
            MySerial.Write("0");  //TURN OFF RELAY

            BackgroundWorker worker = sender as BackgroundWorker;

            if (MySerial.IsOpen) //Clear buffer
            {
                //BackgroundBeep.Beep(); //Console.Beep(1000, 100); 
                int dt = MySerial.BytesToRead;
                byte[] dR = new byte[dt];
                MySerial.Read(dR, 0, dt);
            }
            else //if not opened (not supposed to), open port and clear buffer
            {
                MessageBox.Show("Erro: Porta " + MySerial.PortName + " estava fechada.");
                MySerial.Open();
                Task.Delay(1500).Wait();
                //Console.Beep(1000, 100); //Console.Beep(5000, 1000);
                int dt = MySerial.BytesToRead;
                byte[] dR = new byte[dt];
                MySerial.Read(dR, 0, dt);
            }

            var stopwatch = Stopwatch.StartNew();
            bool sincronizador = false;
            //aTimer.Stop();
            //aTimer.Start();

            while (true)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (MySerial.IsOpen)
                    {
                        int dataLength = MySerial.BytesToRead;
                        byte[] dataRecevied = new byte[dataLength];
                        MySerial.Read(dataRecevied, 0, dataLength);

                        if (dataRecevied.Length > 0)
                        {
                            for (int i = 0; i < dataRecevied.Length; i++)
                                ReadSlidingWindow.Add(dataRecevied[i]);

                            //TextWriter asdasd = new StreamWriter(pathLog + @"SavedListA.txt");

                            //Remove 1ra amostra se quebrada
                            //Verifica se a placa esta configurada corretamente
                            if (first)
                            {
                                first = false;
                                while (true)
                                {
                                    try
                                    {
                                        if (!byteDivisor.SequenceEqual(ReadSlidingWindow.GetRange(0, 4)))
                                            ReadSlidingWindow.RemoveAt(0);
                                        else if (!byteDivisor.SequenceEqual(ReadSlidingWindow.GetRange(div1, 4)))
                                        {
                                            errorToColect();
                                            break;
                                        }
                                        else { break; }
                                    }
                                    catch (Exception)
                                    {
                                        errorToColect();
                                        break;
                                    }
                                }
                                startTime = DateTime.Now;
                                stopwatch = Stopwatch.StartNew();
                                Task.Run(() => BackgroundBeep.Beep());
                            }

                            while (ReadSlidingWindow.Count() > 60)
                            {
                                if (byteDivisor.SequenceEqual(ReadSlidingWindow.GetRange(0, 4)) && byteDivisor.SequenceEqual(ReadSlidingWindow.GetRange(div1, 4)))
                                { //Verifica se a amostra inicia com \r\r\r\r e esta completa ->(\r\r\r\rb0b1b2b3\t0t1t2t3\r\r\r\r....)
                                    List<byte[]> UnsignedByteArray = new List<byte[]>();
                                    for (int sn = 1; sn <= nSens; sn++)
                                    {
                                        UnsignedByteArray.Add(ReadSlidingWindow.GetRange(4*sn, 4).ToArray());
                                    }
                                    if (div1 == 20 && nSens == 2) //Para o caso de 2 sensores
                                        UnsignedByteArray.Add(ReadSlidingWindow.GetRange(16, 4).ToArray());
                                    else
                                        UnsignedByteArray.Add(ReadSlidingWindow.GetRange(4 * (nSens + 1), 4).ToArray());

                                    //byte[] timeTemp = ReadSlidingWindow.GetRange(4 * (nSens + 1), 4).ToArray();

                                    if (BitConverter.IsLittleEndian)
                                    {
                                        for (int sn = 0; sn < UnsignedByteArray.Count(); sn++)
                                            Array.Reverse(UnsignedByteArray[sn]);
                                        //Array.Reverse(timeTemp);
                                    }

                                    SetDataValues(UnsignedByteArray);

                                    ReadSlidingWindow.RemoveRange(0, div1);
                                }
                                else
                                { //Se amostra não ta completa, descartar ela -> igual a amostra anterior
                                    if (SensorA.Count > 0)
                                    {
                                        if (nSens == 1) { SensorA.Add(SensorA.Last()); }
                                        if (nSens == 2)
                                        {
                                            SensorA.Add(SensorA.Last());
                                            SensorB.Add(SensorB.Last());
                                        }
                                        if (nSens == 3)
                                        {
                                            SensorA.Add(SensorA.Last());
                                            SensorB.Add(SensorB.Last());
                                            SensorC.Add(SensorC.Last());
                                        }
                                        Tempo.Add(Tempo.Last());
                                    }
                                    bool b = true; int i = 1;
                                    while (b)
                                    {
                                        //i++;
                                        try
                                        {
                                            if (byteDivisor.SequenceEqual(ReadSlidingWindow.GetRange(i, 4)))
                                            {
                                                ReadSlidingWindow.RemoveRange(0, i);
                                                b = false;
                                            }
                                            i++;
                                        }
                                        catch { b = false; }
                                    }
                                }
                            }
                            if (SensorA.Count() >  0)
                            {
                                if (nSens == 1)
                                {
                                    double[] charPlot = { sensorAmm };
                                    UpdateLiveChart(LiveChart, charPlot);// wSensorA.Last() + mmSensorB.Last());
                                }
                                else if (nSens == 2)
                                {
                                    double[] charPlot = { sensorAmm, sensorBmm };
                                    UpdateLiveChart(LiveChart, charPlot);// wSensorA.Last() + mmSensorB.Last());
                                    UpdateCOPChart(COPChart, (50 * (sensorAmm / (sensorAmm + sensorBmm))) - 25, 0);
                                }
                                else if (nSens == 3)
                                {
                                    double[] charPlot = { sensorAmm, sensorBmm, sensorCmm };
                                    //double[] charPlot = { sensorAmm * gravidade, sensorBmm * gravidade, sensorCmm * gravidade };
                                    UpdateLiveChart(LiveChart, charPlot);//wSensorA.Last());
                                    UpdateCOPChart(COPChart, (sensorCmm / sensorAmm)*100, (sensorBmm / sensorAmm)*100);
                                }
                            }

                            //asdasd.Close();
                        }

                        if (stopFlag)
                        {
                            var stopTime = startTime + stopwatch.Elapsed;
                            stopwatch.Stop();
                            //SetProgressBar(ProgressBar, 100);
                            break; //STOP DATA ACQUISITION
                        }

                        if (!sincronizador && stopwatch.Elapsed.TotalMilliseconds > 3000)
                        {
                            MySerial.Write("1");  //TURN ON RELAY
                            //if (SensorA.Count > 0)
                            //{
                            //    if (nSens == 1) { SensorA[SensorA.Count - 1] = Math.Abs(SensorA.Max()) * 10; }
                            //    if (nSens == 2)
                            //    {
                            //        SensorA[SensorA.Count - 1] = Math.Abs(SensorA.Max()) * 10;
                            //        SensorB[SensorB.Count - 1] = Math.Abs(SensorB.Max()) * 10;
                            //    }
                            //    if (nSens == 3)
                            //    {
                            //        SensorA[SensorA.Count - 1] = Math.Abs(SensorA.Max()) * 10;
                            //        SensorB[SensorB.Count - 1] = Math.Abs(SensorB.Max()) * 10;
                            //        SensorC[SensorC.Count - 1] = Math.Abs(SensorC.Max()) * 10;
                            //    }
                            //}
                            sincronizador = true;
                        }

                        if (sincronizador && stopwatch.Elapsed.TotalMilliseconds > 5000)
                        {
                            MySerial.Write("0");  //TURN OFF RELAY
                        }

                        var cronometro = DateTime.Now.Subtract(startTime);
                        SetText(clock_display, cronometro.Hours.ToString().PadLeft(2, '0') + ":" +
                            cronometro.Minutes.ToString().PadLeft(2, '0') + ":" +
                            cronometro.Seconds.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        /* DECODIFICAR O RESTO DOS DADOS NA JANELA */
                        break;
                    }
                }
            }
            MySerial.Close();
            //dados.Add(ReadValues);
            ////dados.Add(ReadTimes);
            //TextWriter tw = new StreamWriter(pathLog + @"SavedList.txt");
            //foreach (double s in ReadValues)
            //    tw.WriteLine(s);
            //tw.Close();
        }

        private void errorToColect()
        {
            if (MySerial.IsOpen)
                MySerial.Close();
            //Stop_Button_Click(sender, e);
            Update_EventLog_TextBox("Placa não configurada corretamente.");
            stopFlag = true;
            this.BeginInvoke((Action)(() =>
            {
                Stop_Button.Enabled = false;
                Record_Button.Enabled = true;
                Plot_Button.Enabled = false;
                OpenFile_Button.Enabled = true;
                Calib_Button.Enabled = true;
                SerialPortsBox.Enabled = true;
                StatusText.Text = "OFF";
                StatusText.ForeColor = System.Drawing.Color.Red;
            }));
        }

        void SetDataValues(List<byte[]> mvRead)
        {
            if(nSens == 1)
            {
                mvDataA.Add(BitConverter.ToInt32(mvRead[0], 0) * resolution_mV);
                SensorA.Add((mvDataA.Last()) * LinReg[0][0] + LinReg[0][1]);

                wSensorA.Add(SensorA.Last());
                if (wSensorA.Count() > mmSize)
                    wSensorA.RemoveAt(0);
                sensorAmm = wSensorA.Sum() / wSensorA.Count();
            }
            else if (nSens == 2)
            {
                mvDataA.Add(BitConverter.ToInt32(mvRead[0], 0) * resolution_mV);
                SensorA.Add(mvDataA.Last() * LinReg[0][0] + LinReg[0][1]);
                mvDataB.Add(BitConverter.ToInt32(mvRead[1], 0) * resolution_mV);
                SensorB.Add(mvDataB.Last() * LinReg[1][0] + LinReg[1][1]);

                wSensorA.Add(SensorA.Last());
                wSensorB.Add(SensorB.Last());
                if (wSensorA.Count() > mmSize)
                {
                    wSensorA.RemoveAt(0);
                    wSensorB.RemoveAt(0);
                }
                sensorAmm = wSensorA.Sum() / wSensorA.Count();
                sensorBmm = wSensorB.Sum() / wSensorB.Count();
            }
            else if (nSens == 3)
            {
                mvDataA.Add(BitConverter.ToInt32(mvRead[0], 0) * resolution_mV);
                SensorA.Add(mvDataA.Last() * LinReg[0][0] + LinReg[0][1]);
                mvDataB.Add(BitConverter.ToInt32(mvRead[1], 0) * resolution_mV);
                SensorB.Add(mvDataB.Last() * LinReg[1][0] + LinReg[1][1]);
                mvDataC.Add(BitConverter.ToInt32(mvRead[2], 0) * resolution_mV);
                SensorC.Add(mvDataC.Last() * LinReg[2][0] + LinReg[2][1]);

                wSensorA.Add(SensorA.Last());
                wSensorB.Add(SensorB.Last());
                wSensorC.Add(SensorC.Last());
                if (wSensorA.Count() > mmSize)
                {
                    wSensorA.RemoveAt(0);
                    wSensorB.RemoveAt(0);
                    wSensorC.RemoveAt(0);
                }
                sensorAmm = wSensorA.Sum() / wSensorA.Count();
                sensorBmm = wSensorB.Sum() / wSensorB.Count();
                sensorCmm = wSensorC.Sum() / wSensorC.Count();
            }

            
            Tempo.Add(Convert.ToDouble(BitConverter.ToUInt32(mvRead.Last(), 0)) / 1000);

            //mvData.Add((BitConverter.ToUInt32(mvRead, 0) * resolution_mV) - LinReg[0][3]);// = SensorA.Last() - LinReg[0][3];
            //SensorA.Add(mvData.Last() * LinReg[0][0] + LinReg[0][1]);
            //Tempo.Add(Convert.ToDouble(BitConverter.ToUInt32(tRead, 0)) / 1000);
        }

        private void Plot_Button_Click(object sender, EventArgs e) { Plot_graph(); }

        void Plot_graph()
        {
            int nDados = dados.Count();
            if (nDados > 0)
            {
                for (int k = 0; k < nDados - 1; k++)
                {
                    List<List<double>> temp = new List<List<double>> { dados[k], dados[nDados - 1] };
                    if (nDados == 2)
                    {
                        Form2 Graph_Window = new Form2(temp, k, "1D");
                        Graph_Window.Show();
                    }
                    else if (nDados == 3)
                    {
                        Form2 Graph_Window = new Form2(temp, k, "Bi");
                        Graph_Window.Show();
                    }
                    else if (nDados == 4)
                    {
                        Form2 Graph_Window = new Form2(temp, k, "3D");
                        Graph_Window.Show();
                    }
                }
            }
        }

        private void EventLogTextBox_TextChanged(object sender, EventArgs e)
        {
            EventLogTextBox.SelectionStart = EventLogTextBox.Text.Length;
            EventLogTextBox.ScrollToCaret();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { if (MySerial.IsOpen) MySerial.Close(); }

        private void Upload_Button_Click(object sender, EventArgs e)
        {
            if (SerialPortsBox.Text != "")
            {
                //Update_EventLog_TextBox("Configurando para " + TipoBox.Text + ".");
                Upload_Schetch();
            }
            else
            {
                Update_EventLog_TextBox("Selecione a porta conectada ao ADS.");
            }
        }

        private void OpenFile_Button_Click(object sender, EventArgs e) => Open_file();

        string GetFileName()
        {
            List<string> file_pathName = new List<string>();
            List<string> file_pathFull = Directory.GetFiles(pathTxt).ToList();
            //Directory.GetFiles(pathTxt).ToList();

            foreach (string v in file_pathFull) //Pega do nome a parte com a data + número
                file_pathName.Add(v.Remove(0, 62).Remove(v.Remove(0, 64).Length - 2));

            List<double> mesma_data = new List<double>();
            foreach (string x in file_pathName) //Pega o maior valor dos números de arquivos com mesmo nome
            {
                if (x.Contains(DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", "")))
                { mesma_data.Add(Convert.ToDouble(x.Remove(0, 8))); }
            }
            if (mesma_data.Count > 0)
                return DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", "") + (mesma_data.Max() + 1).ToString().PadLeft(3, '0');
            else
                return DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", "") + 0.ToString().PadLeft(3, '0');
        }

        void Save_txt(List<List<double>> saveData, string fileName)
        {
            try
            {
                TextWriter twTXT = new StreamWriter(pathTxt + fileName + ".txt");
                twTXT.WriteLine("Data da coleta: " + DateTime.Now.ToString("dd/MM/yyyy - H:mm:ss")); //DateTime.Now.Date.ToString("dd/MM/yyyy - H:mm:ss"));
                twTXT.WriteLine("Sensores: " + (saveData.Count - 1));
                twTXT.WriteLine("Tempo de coleta: " + saveData[saveData.Count() - 1].Last() + " segundos.");
                twTXT.WriteLine("Amostras: " + saveData[0].Count());
                twTXT.WriteLine(("Taxa de aquisição: " + saveData[0].Count() / saveData[saveData.Count() - 1].Last() + "Hz").Replace(",", "."));
                if (saveData.Count() == 2) { twTXT.WriteLine("Tempo\t\tSensor 1"); }
                if (saveData.Count() == 3) { twTXT.WriteLine("Tempo\t\tSensor 1\tSensor 2"); }
                if (saveData.Count() == 4) { twTXT.WriteLine("Tempo\t\tSensor 1\tSensor 2\tSensor 3"); }

                for (int j = 0; j < saveData[0].Count(); j++)
                {
                    string f = "";
                    f += saveData[saveData.Count - 1][j].ToString("#0.0000000000").Replace(",", ".") + "\t";
                    for (int i = 0; i < saveData.Count() - 1; i++)
                    {
                        f += saveData[i][j].ToString("#0.0000000000").Replace(",", ".") + "\t";
                    }
                    twTXT.WriteLine(f);
                }
                twTXT.Close();
            }
            catch (Exception e)
            {
                Update_EventLog_TextBox("Error sTXT01: " + e.Message);
                //EventLogTextBox.AppendText("Erro: " + e.ToString());
            }
        }

        void Save_sad(List<List<double>> saveData, string fileName)
        {
            try
            {
                //EventLogTextBox.AppendText("Arquivo: " + fileName + ".SAD" + Environment.NewLine);
                //EventLogTextBox.AppendText("Arquivo: " + file_name + ".SAD" + Environment.NewLine);
                var SAD = new StringBuilder();
                SAD.AppendLine("SADv2"); SAD.AppendLine("Arquivo tipo AQDADOS importado"); SAD.AppendLine(""); SAD.AppendLine(""); SAD.AppendLine("");
                if (saveData.Count() == 2) { SAD.AppendLine("1"); }
                if (saveData.Count() == 3) { SAD.AppendLine("2"); }
                if (saveData.Count() == 4) { SAD.AppendLine("3"); }
                SAD.AppendLine("1"); SAD.AppendLine("0 0 1 0 0 0 0"); SAD.AppendLine("0 0 0 0 0 0 0");

                for (int i = 0; i < saveData.Count() - 1; i++)
                {
                    SAD.AppendLine("Sensor " + (i + 1).ToString()); SAD.AppendLine((i + 1).ToString()); SAD.AppendLine("0"); SAD.AppendLine(saveData[0].Count().ToString());
                    for (int j = 0; j < saveData[0].Count(); j++)
                    {
                        string[] f = { saveData[saveData.Count() - 1][j].ToString("#0.0000000000").Replace(",", "."), saveData[i][j].ToString("#0.0000000000").Replace(",", ".") };
                        SAD.AppendLine(string.Join(" ", f));
                    }
                }
                File.WriteAllText(pathSad + fileName + ".SAD", SAD.ToString());
                //File.WriteAllText(pathSad + file_name + ".SAD", SAD.ToString());
            }
            catch (Exception e)
            {
                Update_EventLog_TextBox("Error sSAD01: " + e.Message);
                //EventLogTextBox.AppendText("Erro: " + e.ToString());
            }
        }

        void Open_file()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = pathTxt,
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            dados.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_name = openFileDialog1.SafeFileName.Remove(openFileDialog1.SafeFileName.Length - 4);
                Update_EventLog_TextBox("Processando coleta " + file_name + "...");
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        sr.ReadLine();
                        int nSensor = Convert.ToInt32(Regex.Split(sr.ReadLine(), ": ").Last());
                        sr.ReadLine();
                        int nSamples = Convert.ToInt32(Regex.Split(sr.ReadLine(), ": ").Last());
                        sr.ReadLine(); sr.ReadLine();

                        for (int i = 0; i <= nSensor; i++) { dados.Add(new List<double>(new double[nSamples])); }
                        int k = 0;
                        for (int i = 0; i < nSamples; i++)
                        {
                            string[] result = Regex.Split(sr.ReadLine(), "\t");
                            for (int j = 0; j < nSensor; j++)
                            {
                                dados[j][k] = Convert.ToDouble(result[j + 1].Replace(".", ","));
                            }
                            dados[dados.Count() - 1][k] = Convert.ToDouble(result[0].Replace(".", ",")); //tempo (ultimo)
                            k++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
            Update_EventLog_TextBox("Coleta processada.");
            Plot_Button.Enabled = true;
            Plot_Button.BackColor = Color.Blue;
        }

        private void TipoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Upload_Button.Enabled = true;
            LinReg.Clear();
            LinReg = GetCalibConfig();
            if (TipoBox.Text == "Bilateral")
            {
                Update_EventLog_TextBox("ESQUEMA DE LIGAÇÃO: " + Environment.NewLine +
                                        "C\tB\tA" + Environment.NewLine +
                                        "-\t1D\t3D");
                BilateralBox.Enabled = true;

                if (COPChart.Series.Count < 8)
                {
                    Series s = new Series("Meio");
                    for (int i = 0; i < 50 + 1; i++)
                    {
                        s.Points.AddXY(0, i - (50 / 2));
                    }
                    COPChart.Series.Add(s);
                    COPChart.Series.Last().ChartType = SeriesChartType.Line;
                    COPChart.Series.Last().Color = Color.Blue;
                    COPChart.Series.Last().BorderWidth = 1;
                }
            }
            else
            {
                BilateralBox.Enabled = false;
                if (TipoBox.Text == "Plataforma 3D")
                    Update_EventLog_TextBox("ESQUEMA DE LIGAÇÃO: " + Environment.NewLine +
                                            "C\tB\tA" + Environment.NewLine +
                                            "Mz\tMx\tFy");

                if (TipoBox.Text == "Plataforma 1D")
                    Update_EventLog_TextBox("ESQUEMA DE LIGAÇÃO: " + Environment.NewLine +
                                            "C\tB\tA" + Environment.NewLine +
                                            "-\t-\tFy");
                if(COPChart.Series.Count == 8)
                {
                    COPChart.Series.RemoveAt(COPChart.Series.Count-1);
                }

            }
        }

        private void Calib_Button_Click(object sender, EventArgs e)
        {
            if (MySerial.IsOpen)
            {
                MySerial.Close();
                Update_EventLog_TextBox("Abrindo Janela de Calibração" + Environment.NewLine);
            }
            Form3 Calib_Window = new Form3();
            Calib_Window.Show();
        }

        private void CheckBox3D_Click(object sender, EventArgs e)
        {
            if (!CheckBox3D.Checked)
                CheckBox1D.Checked = true;
            else
                CheckBox1D.Checked = false;
        }

        private void CheckBox1D_Click(object sender, EventArgs e)
        {
            if (!CheckBox1D.Checked)
                CheckBox3D.Checked = true;
            else
                CheckBox3D.Checked = false;
        }

        private void Update_EventLog_TextBox(string v)
        {
            try
            {
                this.BeginInvoke((Action)(() =>
                {
                    EventLogTextBox.AppendText(v + Environment.NewLine);
                }));
            }
            catch { }
        }

        void MapRaw(int X, int Z)
        {
            try
            {
                COPChart.Series.Clear();
                COPChart.ChartAreas[0].CursorX.IsUserEnabled = false;
                COPChart.ChartAreas[0].CursorY.IsUserEnabled = false;
                COPChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                COPChart.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                COPChart.ChartAreas[0].CursorX.AutoScroll = false;
                COPChart.ChartAreas[0].CursorY.AutoScroll = false;
                COPChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                COPChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                COPChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                COPChart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                COPChart.ChartAreas[0].AxisX2.LabelStyle.Enabled = false;
                COPChart.ChartAreas[0].AxisY2.LabelStyle.Enabled = false;
                COPChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                COPChart.ChartAreas[0].AxisX2.MajorGrid.LineWidth = 0;
                COPChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                COPChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;
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
                    COPChart.Series.Add(s);
                    if (j == 4 || j == 5)
                    {
                        COPChart.Series[j].ChartType = SeriesChartType.Line;
                        COPChart.Series[j].Color = Color.Black;
                        COPChart.Series[j].BorderDashStyle = ChartDashStyle.Dot;
                        COPChart.Series[j].BorderWidth = 1;
                    }
                    else
                    {
                        COPChart.Series[j].ChartType = SeriesChartType.Line;
                        COPChart.Series[j].Color = Color.Blue;
                        COPChart.Series[j].BorderWidth = 3;
                    }
                }
                Series s2 = new Series();
                s2.Points.AddXY(0, 0);
                COPChart.Series.Add(s2);
                COPChart.Series.Last().ChartType = SeriesChartType.Point;
                COPChart.Series.Last().MarkerStyle = MarkerStyle.Circle;
                COPChart.Series.Last().MarkerSize = 10;
                COPChart.Series.Last().Color = Color.Red;
                COPChart.Series.Last().BorderWidth = 3;

                COPChart.ChartAreas[0].AxisX.Maximum = 25;
                COPChart.ChartAreas[0].AxisY.Maximum = 25;
                COPChart.ChartAreas[0].AxisX.Minimum = -25;
                COPChart.ChartAreas[0].AxisY.Minimum = -25;
                COPChart.ChartAreas[0].AxisX.Interval = 5;
                COPChart.ChartAreas[0].AxisY.Interval = 5;
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
    }

}
