namespace BioMensurae
{
    partial class Form3
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.TipoBox = new System.Windows.Forms.ComboBox();
            this.SerialPortsBox = new System.Windows.Forms.ComboBox();
            this.Config_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MzCheckBox = new System.Windows.Forms.CheckBox();
            this.MxCheckBox = new System.Windows.Forms.CheckBox();
            this.FyCheckBox = new System.Windows.Forms.CheckBox();
            this.ZBox = new System.Windows.Forms.TextBox();
            this.XBox = new System.Windows.Forms.TextBox();
            this.GetValueButton = new System.Windows.Forms.Button();
            this.PesoBox = new System.Windows.Forms.TextBox();
            this.LeituraBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MapXZ = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CalibLogTextBox = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LinRegChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.EquationBox = new System.Windows.Forms.TextBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.StopCalib_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapXZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinRegChart)).BeginInit();
            this.SuspendLayout();
            // 
            // TipoBox
            // 
            this.TipoBox.FormattingEnabled = true;
            this.TipoBox.Items.AddRange(new object[] {
            "1D - 500x500",
            "3D - 500x500",
            "6D - 400x600"});
            this.TipoBox.Location = new System.Drawing.Point(73, 17);
            this.TipoBox.Name = "TipoBox";
            this.TipoBox.Size = new System.Drawing.Size(121, 21);
            this.TipoBox.TabIndex = 0;
            // 
            // SerialPortsBox
            // 
            this.SerialPortsBox.FormattingEnabled = true;
            this.SerialPortsBox.Location = new System.Drawing.Point(73, 44);
            this.SerialPortsBox.Name = "SerialPortsBox";
            this.SerialPortsBox.Size = new System.Drawing.Size(121, 21);
            this.SerialPortsBox.TabIndex = 1;
            this.SerialPortsBox.Click += new System.EventHandler(this.SerialPortsBox_Click);
            // 
            // Config_Button
            // 
            this.Config_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Config_Button.Location = new System.Drawing.Point(6, 71);
            this.Config_Button.Name = "Config_Button";
            this.Config_Button.Size = new System.Drawing.Size(189, 23);
            this.Config_Button.TabIndex = 2;
            this.Config_Button.Text = "Configurar ";
            this.Config_Button.UseVisualStyleBackColor = true;
            this.Config_Button.Click += new System.EventHandler(this.Config_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Config_Button);
            this.groupBox1.Controls.Add(this.TipoBox);
            this.groupBox1.Controls.Add(this.SerialPortsBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações da Plataforma";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Porta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MzCheckBox);
            this.groupBox2.Controls.Add(this.MxCheckBox);
            this.groupBox2.Controls.Add(this.FyCheckBox);
            this.groupBox2.Controls.Add(this.ZBox);
            this.groupBox2.Controls.Add(this.XBox);
            this.groupBox2.Controls.Add(this.GetValueButton);
            this.groupBox2.Controls.Add(this.PesoBox);
            this.groupBox2.Controls.Add(this.LeituraBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 194);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calibração";
            // 
            // MzCheckBox
            // 
            this.MzCheckBox.AutoSize = true;
            this.MzCheckBox.Enabled = false;
            this.MzCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MzCheckBox.Location = new System.Drawing.Point(150, 82);
            this.MzCheckBox.Name = "MzCheckBox";
            this.MzCheckBox.Size = new System.Drawing.Size(45, 21);
            this.MzCheckBox.TabIndex = 9;
            this.MzCheckBox.Text = "Mz";
            this.MzCheckBox.UseVisualStyleBackColor = true;
            this.MzCheckBox.Click += new System.EventHandler(this.MzCheckBox_Click);
            // 
            // MxCheckBox
            // 
            this.MxCheckBox.AutoSize = true;
            this.MxCheckBox.Enabled = false;
            this.MxCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MxCheckBox.Location = new System.Drawing.Point(78, 82);
            this.MxCheckBox.Name = "MxCheckBox";
            this.MxCheckBox.Size = new System.Drawing.Size(44, 21);
            this.MxCheckBox.TabIndex = 9;
            this.MxCheckBox.Text = "Mx";
            this.MxCheckBox.UseVisualStyleBackColor = true;
            this.MxCheckBox.Click += new System.EventHandler(this.MxCheckBox_Click);
            // 
            // FyCheckBox
            // 
            this.FyCheckBox.AutoSize = true;
            this.FyCheckBox.Checked = true;
            this.FyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FyCheckBox.Enabled = false;
            this.FyCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FyCheckBox.Location = new System.Drawing.Point(6, 82);
            this.FyCheckBox.Name = "FyCheckBox";
            this.FyCheckBox.Size = new System.Drawing.Size(42, 21);
            this.FyCheckBox.TabIndex = 9;
            this.FyCheckBox.Text = "Fy";
            this.FyCheckBox.UseVisualStyleBackColor = true;
            this.FyCheckBox.Click += new System.EventHandler(this.FyCheckBox_Click);
            // 
            // ZBox
            // 
            this.ZBox.Enabled = false;
            this.ZBox.Location = new System.Drawing.Point(125, 132);
            this.ZBox.Name = "ZBox";
            this.ZBox.Size = new System.Drawing.Size(67, 23);
            this.ZBox.TabIndex = 6;
            this.ZBox.Click += new System.EventHandler(this.ZBox_Click);
            this.ZBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZBox_KeyPress);
            // 
            // XBox
            // 
            this.XBox.Enabled = false;
            this.XBox.Location = new System.Drawing.Point(27, 132);
            this.XBox.Name = "XBox";
            this.XBox.Size = new System.Drawing.Size(67, 23);
            this.XBox.TabIndex = 5;
            this.XBox.Click += new System.EventHandler(this.XBox_Click);
            this.XBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XBox_KeyPress);
            // 
            // GetValueButton
            // 
            this.GetValueButton.Enabled = false;
            this.GetValueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetValueButton.Location = new System.Drawing.Point(6, 165);
            this.GetValueButton.Name = "GetValueButton";
            this.GetValueButton.Size = new System.Drawing.Size(189, 23);
            this.GetValueButton.TabIndex = 8;
            this.GetValueButton.Text = "Adicionar Leitura";
            this.GetValueButton.UseVisualStyleBackColor = true;
            this.GetValueButton.Click += new System.EventHandler(this.GetValueButton_Click);
            // 
            // PesoBox
            // 
            this.PesoBox.Enabled = false;
            this.PesoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PesoBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PesoBox.Location = new System.Drawing.Point(89, 51);
            this.PesoBox.Name = "PesoBox";
            this.PesoBox.Size = new System.Drawing.Size(105, 23);
            this.PesoBox.TabIndex = 4;
            this.PesoBox.Click += new System.EventHandler(this.PesoBox_Click);
            this.PesoBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PesoBox_KeyPress);
            // 
            // LeituraBox
            // 
            this.LeituraBox.Enabled = false;
            this.LeituraBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeituraBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LeituraBox.Location = new System.Drawing.Point(72, 22);
            this.LeituraBox.Name = "LeituraBox";
            this.LeituraBox.ReadOnly = true;
            this.LeituraBox.Size = new System.Drawing.Size(122, 23);
            this.LeituraBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Peso (Kg)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(107, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Coordenadas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Leitura:";
            // 
            // MapXZ
            // 
            this.MapXZ.BackColor = System.Drawing.Color.Transparent;
            this.MapXZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            chartArea1.Name = "ChartArea1";
            this.MapXZ.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.MapXZ.Legends.Add(legend1);
            this.MapXZ.Location = new System.Drawing.Point(218, 12);
            this.MapXZ.Name = "MapXZ";
            this.MapXZ.Size = new System.Drawing.Size(300, 300);
            this.MapXZ.TabIndex = 5;
            this.MapXZ.TabStop = false;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Mapa de carga XZ";
            this.MapXZ.Titles.Add(title1);
            // 
            // CalibLogTextBox
            // 
            this.CalibLogTextBox.Location = new System.Drawing.Point(12, 329);
            this.CalibLogTextBox.Name = "CalibLogTextBox";
            this.CalibLogTextBox.ReadOnly = true;
            this.CalibLogTextBox.Size = new System.Drawing.Size(461, 133);
            this.CalibLogTextBox.TabIndex = 6;
            this.CalibLogTextBox.TabStop = false;
            this.CalibLogTextBox.Text = "";
            this.CalibLogTextBox.TextChanged += new System.EventHandler(this.CalibLogTextBox_TextChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // LinRegChart
            // 
            this.LinRegChart.BackColor = System.Drawing.Color.Transparent;
            this.LinRegChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            chartArea2.Name = "ChartArea1";
            this.LinRegChart.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.LinRegChart.Legends.Add(legend2);
            this.LinRegChart.Location = new System.Drawing.Point(511, 12);
            this.LinRegChart.Name = "LinRegChart";
            this.LinRegChart.Size = new System.Drawing.Size(300, 300);
            this.LinRegChart.TabIndex = 7;
            this.LinRegChart.TabStop = false;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            title2.Name = "Title1";
            title2.Text = "Relação Kg x mV";
            this.LinRegChart.Titles.Add(title2);
            // 
            // EquationBox
            // 
            this.EquationBox.BackColor = System.Drawing.SystemColors.Control;
            this.EquationBox.Location = new System.Drawing.Point(511, 329);
            this.EquationBox.Multiline = true;
            this.EquationBox.Name = "EquationBox";
            this.EquationBox.Size = new System.Drawing.Size(288, 104);
            this.EquationBox.TabIndex = 8;
            this.EquationBox.TabStop = false;
            // 
            // Save_Button
            // 
            this.Save_Button.Enabled = false;
            this.Save_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_Button.Location = new System.Drawing.Point(610, 439);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(189, 23);
            this.Save_Button.TabIndex = 7;
            this.Save_Button.Text = "Salvar Calibração";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // StopCalib_Button
            // 
            this.StopCalib_Button.Enabled = false;
            this.StopCalib_Button.Location = new System.Drawing.Point(511, 439);
            this.StopCalib_Button.Name = "StopCalib_Button";
            this.StopCalib_Button.Size = new System.Drawing.Size(75, 23);
            this.StopCalib_Button.TabIndex = 9;
            this.StopCalib_Button.Text = "Parar";
            this.StopCalib_Button.UseVisualStyleBackColor = true;
            this.StopCalib_Button.Click += new System.EventHandler(this.StopCalib_Button_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 474);
            this.Controls.Add(this.StopCalib_Button);
            this.Controls.Add(this.EquationBox);
            this.Controls.Add(this.LinRegChart);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.CalibLogTextBox);
            this.Controls.Add(this.MapXZ);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "Calibrate Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapXZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinRegChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TipoBox;
        private System.Windows.Forms.ComboBox SerialPortsBox;
        private System.Windows.Forms.Button Config_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox PesoBox;
        private System.Windows.Forms.TextBox LeituraBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ZBox;
        private System.Windows.Forms.TextBox XBox;
        private System.Windows.Forms.Button GetValueButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart MapXZ;
        private System.Windows.Forms.RichTextBox CalibLogTextBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart LinRegChart;
        private System.Windows.Forms.TextBox EquationBox;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.CheckBox FyCheckBox;
        private System.Windows.Forms.CheckBox MzCheckBox;
        private System.Windows.Forms.CheckBox MxCheckBox;
        private System.Windows.Forms.Button StopCalib_Button;
    }
}