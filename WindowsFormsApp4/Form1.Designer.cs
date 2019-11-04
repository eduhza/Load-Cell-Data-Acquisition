namespace WindowsFormsApp4
{
    partial class BioMensurae
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BioMensurae));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.OpenFile_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EventLogTextBox = new System.Windows.Forms.RichTextBox();
            this.BilateralBox = new System.Windows.Forms.GroupBox();
            this.CheckBox1D = new System.Windows.Forms.CheckBox();
            this.CheckBox3D = new System.Windows.Forms.CheckBox();
            this.Calib_Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TipoBox = new System.Windows.Forms.ComboBox();
            this.SerialPortsBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Upload_Button = new System.Windows.Forms.Button();
            this.Plot_Button = new System.Windows.Forms.Button();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.Record_Button = new System.Windows.Forms.Button();
            this.clock_display = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LiveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.COPChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.StatusText = new System.Windows.Forms.Label();
            this.MVText = new System.Windows.Forms.Label();
            this.PesoText = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MVLabel = new System.Windows.Forms.Label();
            this.PesoLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.BilateralBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiveChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.COPChart)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // OpenFile_Button
            // 
            this.OpenFile_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpenFile_Button.BackgroundImage")));
            this.OpenFile_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.OpenFile_Button.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.OpenFile_Button.FlatAppearance.BorderSize = 0;
            this.OpenFile_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFile_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenFile_Button.ForeColor = System.Drawing.Color.Black;
            this.OpenFile_Button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OpenFile_Button.Location = new System.Drawing.Point(120, 156);
            this.OpenFile_Button.Name = "OpenFile_Button";
            this.OpenFile_Button.Size = new System.Drawing.Size(43, 31);
            this.OpenFile_Button.TabIndex = 18;
            this.OpenFile_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OpenFile_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip5.SetToolTip(this.OpenFile_Button, "Abrir coleta");
            this.OpenFile_Button.UseVisualStyleBackColor = true;
            this.OpenFile_Button.Click += new System.EventHandler(this.OpenFile_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EventLogTextBox);
            this.groupBox1.Controls.Add(this.BilateralBox);
            this.groupBox1.Controls.Add(this.Calib_Button);
            this.groupBox1.Controls.Add(this.OpenFile_Button);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TipoBox);
            this.groupBox1.Controls.Add(this.SerialPortsBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Upload_Button);
            this.groupBox1.Controls.Add(this.Plot_Button);
            this.groupBox1.Controls.Add(this.Stop_Button);
            this.groupBox1.Controls.Add(this.Record_Button);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 388);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações";
            // 
            // EventLogTextBox
            // 
            this.EventLogTextBox.BackColor = System.Drawing.Color.White;
            this.EventLogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventLogTextBox.Location = new System.Drawing.Point(6, 193);
            this.EventLogTextBox.Name = "EventLogTextBox";
            this.EventLogTextBox.ReadOnly = true;
            this.EventLogTextBox.Size = new System.Drawing.Size(187, 189);
            this.EventLogTextBox.TabIndex = 45;
            this.EventLogTextBox.Text = "";
            this.EventLogTextBox.TextChanged += new System.EventHandler(this.EventLogTextBox_TextChanged);
            // 
            // BilateralBox
            // 
            this.BilateralBox.Controls.Add(this.CheckBox1D);
            this.BilateralBox.Controls.Add(this.CheckBox3D);
            this.BilateralBox.Enabled = false;
            this.BilateralBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BilateralBox.Location = new System.Drawing.Point(4, 102);
            this.BilateralBox.Name = "BilateralBox";
            this.BilateralBox.Size = new System.Drawing.Size(200, 48);
            this.BilateralBox.TabIndex = 44;
            this.BilateralBox.TabStop = false;
            this.BilateralBox.Text = "Bilateral: Plataforma A";
            // 
            // CheckBox1D
            // 
            this.CheckBox1D.AutoSize = true;
            this.CheckBox1D.Location = new System.Drawing.Point(45, 20);
            this.CheckBox1D.Name = "CheckBox1D";
            this.CheckBox1D.Size = new System.Drawing.Size(40, 17);
            this.CheckBox1D.TabIndex = 42;
            this.CheckBox1D.Text = "1D";
            this.CheckBox1D.UseVisualStyleBackColor = true;
            this.CheckBox1D.Click += new System.EventHandler(this.CheckBox1D_Click);
            // 
            // CheckBox3D
            // 
            this.CheckBox3D.AutoSize = true;
            this.CheckBox3D.Checked = true;
            this.CheckBox3D.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox3D.Location = new System.Drawing.Point(97, 20);
            this.CheckBox3D.Name = "CheckBox3D";
            this.CheckBox3D.Size = new System.Drawing.Size(40, 17);
            this.CheckBox3D.TabIndex = 42;
            this.CheckBox3D.Text = "3D";
            this.CheckBox3D.UseVisualStyleBackColor = true;
            this.CheckBox3D.Click += new System.EventHandler(this.CheckBox3D_Click);
            // 
            // Calib_Button
            // 
            this.Calib_Button.BackColor = System.Drawing.Color.Transparent;
            this.Calib_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Calib_Button.BackgroundImage")));
            this.Calib_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Calib_Button.FlatAppearance.BorderSize = 0;
            this.Calib_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Calib_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Calib_Button.Location = new System.Drawing.Point(160, 156);
            this.Calib_Button.Name = "Calib_Button";
            this.Calib_Button.Size = new System.Drawing.Size(43, 31);
            this.Calib_Button.TabIndex = 25;
            this.Calib_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Calib_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip6.SetToolTip(this.Calib_Button, "Calibrar Plataformas");
            this.Calib_Button.UseVisualStyleBackColor = true;
            this.Calib_Button.Click += new System.EventHandler(this.Calib_Button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 43;
            // 
            // TipoBox
            // 
            this.TipoBox.AllowDrop = true;
            this.TipoBox.DisplayMember = "0";
            this.TipoBox.DropDownHeight = 103;
            this.TipoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoBox.FormattingEnabled = true;
            this.TipoBox.IntegralHeight = false;
            this.TipoBox.Items.AddRange(new object[] {
            "Plataforma 1D",
            "Plataforma 3D",
            "Bilateral"});
            this.TipoBox.Location = new System.Drawing.Point(72, 29);
            this.TipoBox.Name = "TipoBox";
            this.TipoBox.Size = new System.Drawing.Size(132, 24);
            this.TipoBox.TabIndex = 35;
            this.TipoBox.ValueMember = "0";
            this.TipoBox.SelectedIndexChanged += new System.EventHandler(this.TipoBox_SelectedIndexChanged);
            // 
            // SerialPortsBox
            // 
            this.SerialPortsBox.AllowDrop = true;
            this.SerialPortsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialPortsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerialPortsBox.FormattingEnabled = true;
            this.SerialPortsBox.Location = new System.Drawing.Point(72, 65);
            this.SerialPortsBox.Name = "SerialPortsBox";
            this.SerialPortsBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SerialPortsBox.Size = new System.Drawing.Size(132, 24);
            this.SerialPortsBox.TabIndex = 36;
            this.SerialPortsBox.SelectedIndexChanged += new System.EventHandler(this.SerialPortsBox_SelectedIndexChanged);
            this.SerialPortsBox.Click += new System.EventHandler(this.SerialPortsBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Porta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Tipo:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Upload_Button
            // 
            this.Upload_Button.BackColor = System.Drawing.Color.ForestGreen;
            this.Upload_Button.Enabled = false;
            this.Upload_Button.FlatAppearance.BorderSize = 0;
            this.Upload_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Upload_Button.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Upload_Button.Location = new System.Drawing.Point(174, 52);
            this.Upload_Button.Name = "Upload_Button";
            this.Upload_Button.Size = new System.Drawing.Size(30, 10);
            this.Upload_Button.TabIndex = 41;
            this.toolTip1.SetToolTip(this.Upload_Button, "Configurar Placa");
            this.Upload_Button.UseVisualStyleBackColor = false;
            this.Upload_Button.Click += new System.EventHandler(this.Upload_Button_Click);
            // 
            // Plot_Button
            // 
            this.Plot_Button.BackColor = System.Drawing.Color.Transparent;
            this.Plot_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Plot_Button.BackgroundImage")));
            this.Plot_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Plot_Button.Cursor = System.Windows.Forms.Cursors.Default;
            this.Plot_Button.Enabled = false;
            this.Plot_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Plot_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.Plot_Button.ForeColor = System.Drawing.Color.Transparent;
            this.Plot_Button.Location = new System.Drawing.Point(80, 156);
            this.Plot_Button.Name = "Plot_Button";
            this.Plot_Button.Size = new System.Drawing.Size(43, 31);
            this.Plot_Button.TabIndex = 34;
            this.toolTip2.SetToolTip(this.Plot_Button, "Plotar gráfico");
            this.Plot_Button.UseVisualStyleBackColor = false;
            this.Plot_Button.Click += new System.EventHandler(this.Plot_Button_Click);
            // 
            // Stop_Button
            // 
            this.Stop_Button.BackColor = System.Drawing.Color.Transparent;
            this.Stop_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Stop_Button.BackgroundImage")));
            this.Stop_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Stop_Button.Enabled = false;
            this.Stop_Button.FlatAppearance.BorderSize = 0;
            this.Stop_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop_Button.Location = new System.Drawing.Point(40, 156);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(43, 31);
            this.Stop_Button.TabIndex = 33;
            this.toolTip4.SetToolTip(this.Stop_Button, "Finalizar coleta");
            this.Stop_Button.UseVisualStyleBackColor = false;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // Record_Button
            // 
            this.Record_Button.BackColor = System.Drawing.Color.Transparent;
            this.Record_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Record_Button.BackgroundImage")));
            this.Record_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Record_Button.Enabled = false;
            this.Record_Button.FlatAppearance.BorderSize = 0;
            this.Record_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Record_Button.Location = new System.Drawing.Point(0, 156);
            this.Record_Button.Name = "Record_Button";
            this.Record_Button.Size = new System.Drawing.Size(43, 31);
            this.Record_Button.TabIndex = 32;
            this.toolTip3.SetToolTip(this.Record_Button, "Iniciar coleta");
            this.Record_Button.UseVisualStyleBackColor = false;
            this.Record_Button.Click += new System.EventHandler(this.Record_Button_Click);
            // 
            // clock_display
            // 
            this.clock_display.AutoSize = true;
            this.clock_display.Location = new System.Drawing.Point(507, 5);
            this.clock_display.Name = "clock_display";
            this.clock_display.Size = new System.Drawing.Size(71, 20);
            this.clock_display.TabIndex = 2;
            this.clock_display.Text = "00:00:00";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.LiveChart);
            this.panel1.Controls.Add(this.clock_display);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(226, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 273);
            this.panel1.TabIndex = 21;
            // 
            // LiveChart
            // 
            this.LiveChart.BackColor = System.Drawing.Color.Transparent;
            this.LiveChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LiveChart.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.LiveChart.BorderlineColor = System.Drawing.Color.Black;
            this.LiveChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            this.LiveChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.LiveChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.LiveChart.Legends.Add(legend1);
            this.LiveChart.Location = new System.Drawing.Point(7, 26);
            this.LiveChart.Name = "LiveChart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.MarkerSize = 1;
            series1.Name = "Series1";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            this.LiveChart.Series.Add(series1);
            this.LiveChart.Series.Add(series2);
            this.LiveChart.Series.Add(series3);
            this.LiveChart.Size = new System.Drawing.Size(598, 239);
            this.LiveChart.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mistral", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "Live Graph";
            // 
            // COPChart
            // 
            this.COPChart.BackColor = System.Drawing.Color.Transparent;
            this.COPChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.COPChart.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.COPChart.BorderlineColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            this.COPChart.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.COPChart.Legends.Add(legend2);
            this.COPChart.Location = new System.Drawing.Point(350, 277);
            this.COPChart.Name = "COPChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.MarkerSize = 1;
            series4.Name = "Series1";
            series4.YValuesPerPoint = 2;
            this.COPChart.Series.Add(series4);
            this.COPChart.Size = new System.Drawing.Size(130, 130);
            this.COPChart.TabIndex = 23;
            this.COPChart.Text = "COP";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.StatusText);
            this.groupBox2.Controls.Add(this.MVText);
            this.groupBox2.Controls.Add(this.PesoText);
            this.groupBox2.Controls.Add(this.StatusLabel);
            this.groupBox2.Controls.Add(this.MVLabel);
            this.groupBox2.Controls.Add(this.PesoLabel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBox2.Location = new System.Drawing.Point(226, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 109);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Live Data";
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.StatusText.ForeColor = System.Drawing.Color.Red;
            this.StatusText.Location = new System.Drawing.Point(57, 84);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(30, 13);
            this.StatusText.TabIndex = 1;
            this.StatusText.Text = "OFF";
            // 
            // MVText
            // 
            this.MVText.AutoSize = true;
            this.MVText.Location = new System.Drawing.Point(57, 57);
            this.MVText.Name = "MVText";
            this.MVText.Size = new System.Drawing.Size(40, 13);
            this.MVText.TabIndex = 1;
            this.MVText.Text = "00.000";
            // 
            // PesoText
            // 
            this.PesoText.AutoSize = true;
            this.PesoText.Location = new System.Drawing.Point(57, 25);
            this.PesoText.Name = "PesoText";
            this.PesoText.Size = new System.Drawing.Size(40, 13);
            this.PesoText.TabIndex = 1;
            this.PesoText.Text = "00.000";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(6, 84);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(47, 13);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "Status:";
            // 
            // MVLabel
            // 
            this.MVLabel.AutoSize = true;
            this.MVLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MVLabel.Location = new System.Drawing.Point(6, 57);
            this.MVLabel.Name = "MVLabel";
            this.MVLabel.Size = new System.Drawing.Size(32, 13);
            this.MVLabel.TabIndex = 0;
            this.MVLabel.Text = "mV: ";
            // 
            // PesoLabel
            // 
            this.PesoLabel.AutoSize = true;
            this.PesoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PesoLabel.Location = new System.Drawing.Point(6, 25);
            this.PesoLabel.Name = "PesoLabel";
            this.PesoLabel.Size = new System.Drawing.Size(43, 13);
            this.PesoLabel.TabIndex = 0;
            this.PesoLabel.Text = "Peso: ";
            // 
            // BioMensurae
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(846, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.COPChart);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BioMensurae";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BioMensurae Data Acquisition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BilateralBox.ResumeLayout(false);
            this.BilateralBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiveChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.COPChart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button OpenFile_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.ToolTip toolTip6;
        private System.Windows.Forms.Label clock_display;
        private System.Windows.Forms.Button Calib_Button;
        private System.Windows.Forms.Button Plot_Button;
        private System.Windows.Forms.Button Stop_Button;
        private System.Windows.Forms.Button Record_Button;
        private System.Windows.Forms.ComboBox TipoBox;
        private System.Windows.Forms.ComboBox SerialPortsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Upload_Button;
        private System.Windows.Forms.GroupBox BilateralBox;
        private System.Windows.Forms.CheckBox CheckBox1D;
        private System.Windows.Forms.CheckBox CheckBox3D;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart LiveChart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox EventLogTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart COPChart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.Label MVText;
        private System.Windows.Forms.Label PesoText;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label MVLabel;
        private System.Windows.Forms.Label PesoLabel;
    }
}

