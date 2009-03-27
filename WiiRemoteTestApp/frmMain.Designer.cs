namespace WiiRemoteTestApp
{
    partial class frmMain
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
            this.gbxConnectionSettings = new System.Windows.Forms.GroupBox();
            this.lblReportingMode = new System.Windows.Forms.Label();
            this.cboReportingMode = new System.Windows.Forms.ComboBox();
            this.btnConnectDisconnect = new System.Windows.Forms.Button();
            this.gbButtons = new System.Windows.Forms.GroupBox();
            this.pnlB = new System.Windows.Forms.Panel();
            this.pnlDown = new System.Windows.Forms.Panel();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.pnlPlus = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnlMinus = new System.Windows.Forms.Panel();
            this.pnlA = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlUp = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbxLEDs = new System.Windows.Forms.GroupBox();
            this.nudChaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.btnChaseLEDS = new System.Windows.Forms.Button();
            this.chkLED4 = new System.Windows.Forms.CheckBox();
            this.chkLED3 = new System.Windows.Forms.CheckBox();
            this.chkLED2 = new System.Windows.Forms.CheckBox();
            this.chkLED1 = new System.Windows.Forms.CheckBox();
            this.gbxRumble = new System.Windows.Forms.GroupBox();
            this.rbRumbleOff = new System.Windows.Forms.RadioButton();
            this.rbRumbleOn = new System.Windows.Forms.RadioButton();
            this.gbxBattery = new System.Windows.Forms.GroupBox();
            this.lblBatteryLevel = new System.Windows.Forms.Label();
            this.pgbBatteryLevel = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxAccel = new System.Windows.Forms.GroupBox();
            this.lblAccelZ = new System.Windows.Forms.Label();
            this.lblAccelY = new System.Windows.Forms.Label();
            this.lblAccelX = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrChaseLEDs = new System.Windows.Forms.Timer(this.components);
            this.gbxConnectionSettings.SuspendLayout();
            this.gbButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbxLEDs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChaseSpeed)).BeginInit();
            this.gbxRumble.SuspendLayout();
            this.gbxBattery.SuspendLayout();
            this.gbxAccel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxConnectionSettings
            // 
            this.gbxConnectionSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxConnectionSettings.Controls.Add(this.lblReportingMode);
            this.gbxConnectionSettings.Controls.Add(this.cboReportingMode);
            this.gbxConnectionSettings.Controls.Add(this.btnConnectDisconnect);
            this.gbxConnectionSettings.Location = new System.Drawing.Point(12, 12);
            this.gbxConnectionSettings.Name = "gbxConnectionSettings";
            this.gbxConnectionSettings.Size = new System.Drawing.Size(720, 53);
            this.gbxConnectionSettings.TabIndex = 0;
            this.gbxConnectionSettings.TabStop = false;
            this.gbxConnectionSettings.Text = "Connection Settings";
            // 
            // lblReportingMode
            // 
            this.lblReportingMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReportingMode.AutoSize = true;
            this.lblReportingMode.Location = new System.Drawing.Point(352, 22);
            this.lblReportingMode.Name = "lblReportingMode";
            this.lblReportingMode.Size = new System.Drawing.Size(86, 13);
            this.lblReportingMode.TabIndex = 3;
            this.lblReportingMode.Text = "Reporting Mode:";
            // 
            // cboReportingMode
            // 
            this.cboReportingMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReportingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportingMode.FormattingEnabled = true;
            this.cboReportingMode.Items.AddRange(new object[] {
            "Core Buttons Only",
            "Core Buttons, Accelerometer",
            "Core Buttons, 8 Extension Bytes",
            "Core Buttons, Accelerometer, Extended Infrared",
            "Core Buttons, 19 Extension Bytes",
            "Core Buttons, Accelerometer, 16 Extension Bytes",
            "Core Buttons, Basic Infrared, 9 Extension Bytes",
            "21 Extension Bytes Only",
            "Interleaved Core Buttons and Full IR (1/2 Speed reporting)"});
            this.cboReportingMode.Location = new System.Drawing.Point(444, 19);
            this.cboReportingMode.Name = "cboReportingMode";
            this.cboReportingMode.Size = new System.Drawing.Size(270, 21);
            this.cboReportingMode.TabIndex = 2;
            this.cboReportingMode.SelectedIndexChanged += new System.EventHandler(this.cboReportingMode_SelectedIndexChanged);
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Location = new System.Drawing.Point(6, 19);
            this.btnConnectDisconnect.Name = "btnConnectDisconnect";
            this.btnConnectDisconnect.Size = new System.Drawing.Size(128, 23);
            this.btnConnectDisconnect.TabIndex = 1;
            this.btnConnectDisconnect.Text = "Connect";
            this.btnConnectDisconnect.UseVisualStyleBackColor = true;
            this.btnConnectDisconnect.Click += new System.EventHandler(this.btnConnectDisconnect_Click);
            // 
            // gbButtons
            // 
            this.gbButtons.Controls.Add(this.pnlB);
            this.gbButtons.Controls.Add(this.pnlDown);
            this.gbButtons.Controls.Add(this.pnlHome);
            this.gbButtons.Controls.Add(this.pnlPlus);
            this.gbButtons.Controls.Add(this.pnl2);
            this.gbButtons.Controls.Add(this.pnl1);
            this.gbButtons.Controls.Add(this.pnlMinus);
            this.gbButtons.Controls.Add(this.pnlA);
            this.gbButtons.Controls.Add(this.pnlRight);
            this.gbButtons.Controls.Add(this.pnlUp);
            this.gbButtons.Controls.Add(this.pnlLeft);
            this.gbButtons.Controls.Add(this.pictureBox1);
            this.gbButtons.Location = new System.Drawing.Point(12, 71);
            this.gbButtons.Name = "gbButtons";
            this.gbButtons.Size = new System.Drawing.Size(162, 264);
            this.gbButtons.TabIndex = 1;
            this.gbButtons.TabStop = false;
            this.gbButtons.Text = "Buttons";
            // 
            // pnlB
            // 
            this.pnlB.BackColor = System.Drawing.Color.LightGreen;
            this.pnlB.Location = new System.Drawing.Point(112, 51);
            this.pnlB.Name = "pnlB";
            this.pnlB.Size = new System.Drawing.Size(22, 36);
            this.pnlB.TabIndex = 2;
            this.pnlB.Visible = false;
            // 
            // pnlDown
            // 
            this.pnlDown.BackColor = System.Drawing.Color.LightGreen;
            this.pnlDown.Location = new System.Drawing.Point(31, 64);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(5, 7);
            this.pnlDown.TabIndex = 2;
            this.pnlDown.Visible = false;
            // 
            // pnlHome
            // 
            this.pnlHome.BackColor = System.Drawing.Color.LightGreen;
            this.pnlHome.Location = new System.Drawing.Point(30, 127);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(8, 8);
            this.pnlHome.TabIndex = 2;
            this.pnlHome.Visible = false;
            // 
            // pnlPlus
            // 
            this.pnlPlus.BackColor = System.Drawing.Color.LightGreen;
            this.pnlPlus.Location = new System.Drawing.Point(45, 128);
            this.pnlPlus.Name = "pnlPlus";
            this.pnlPlus.Size = new System.Drawing.Size(8, 8);
            this.pnlPlus.TabIndex = 2;
            this.pnlPlus.Visible = false;
            // 
            // pnl2
            // 
            this.pnl2.BackColor = System.Drawing.Color.LightGreen;
            this.pnl2.Location = new System.Drawing.Point(28, 204);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(13, 13);
            this.pnl2.TabIndex = 2;
            this.pnl2.Visible = false;
            // 
            // pnl1
            // 
            this.pnl1.BackColor = System.Drawing.Color.LightGreen;
            this.pnl1.Location = new System.Drawing.Point(28, 184);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(13, 13);
            this.pnl1.TabIndex = 2;
            this.pnl1.Visible = false;
            // 
            // pnlMinus
            // 
            this.pnlMinus.BackColor = System.Drawing.Color.LightGreen;
            this.pnlMinus.Location = new System.Drawing.Point(16, 128);
            this.pnlMinus.Name = "pnlMinus";
            this.pnlMinus.Size = new System.Drawing.Size(8, 8);
            this.pnlMinus.TabIndex = 2;
            this.pnlMinus.Visible = false;
            // 
            // pnlA
            // 
            this.pnlA.BackColor = System.Drawing.Color.LightGreen;
            this.pnlA.Location = new System.Drawing.Point(25, 85);
            this.pnlA.Name = "pnlA";
            this.pnlA.Size = new System.Drawing.Size(18, 17);
            this.pnlA.TabIndex = 2;
            this.pnlA.Visible = false;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.LightGreen;
            this.pnlRight.Location = new System.Drawing.Point(38, 57);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(7, 5);
            this.pnlRight.TabIndex = 2;
            this.pnlRight.Visible = false;
            // 
            // pnlUp
            // 
            this.pnlUp.BackColor = System.Drawing.Color.LightGreen;
            this.pnlUp.Location = new System.Drawing.Point(31, 47);
            this.pnlUp.Name = "pnlUp";
            this.pnlUp.Size = new System.Drawing.Size(5, 7);
            this.pnlUp.TabIndex = 2;
            this.pnlUp.Visible = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.LightGreen;
            this.pnlLeft.Location = new System.Drawing.Point(22, 57);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(7, 5);
            this.pnlLeft.TabIndex = 2;
            this.pnlLeft.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WiiRemoteTestApp.Properties.Resources.WiiRemote;
            this.pictureBox1.Location = new System.Drawing.Point(4, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 239);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // gbxLEDs
            // 
            this.gbxLEDs.Controls.Add(this.nudChaseSpeed);
            this.gbxLEDs.Controls.Add(this.btnChaseLEDS);
            this.gbxLEDs.Controls.Add(this.chkLED4);
            this.gbxLEDs.Controls.Add(this.chkLED3);
            this.gbxLEDs.Controls.Add(this.chkLED2);
            this.gbxLEDs.Controls.Add(this.chkLED1);
            this.gbxLEDs.Location = new System.Drawing.Point(180, 71);
            this.gbxLEDs.Name = "gbxLEDs";
            this.gbxLEDs.Size = new System.Drawing.Size(166, 76);
            this.gbxLEDs.TabIndex = 2;
            this.gbxLEDs.TabStop = false;
            this.gbxLEDs.Text = "LEDs";
            // 
            // nudChaseSpeed
            // 
            this.nudChaseSpeed.Location = new System.Drawing.Point(86, 51);
            this.nudChaseSpeed.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudChaseSpeed.Name = "nudChaseSpeed";
            this.nudChaseSpeed.Size = new System.Drawing.Size(74, 20);
            this.nudChaseSpeed.TabIndex = 2;
            this.nudChaseSpeed.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudChaseSpeed.ValueChanged += new System.EventHandler(this.nudChaseSpeed_ValueChanged);
            // 
            // btnChaseLEDS
            // 
            this.btnChaseLEDS.Location = new System.Drawing.Point(6, 49);
            this.btnChaseLEDS.Name = "btnChaseLEDS";
            this.btnChaseLEDS.Size = new System.Drawing.Size(73, 23);
            this.btnChaseLEDS.TabIndex = 1;
            this.btnChaseLEDS.Text = "Chase";
            this.btnChaseLEDS.UseVisualStyleBackColor = true;
            this.btnChaseLEDS.Click += new System.EventHandler(this.btnChaseLEDS_Click);
            // 
            // chkLED4
            // 
            this.chkLED4.AutoSize = true;
            this.chkLED4.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLED4.Location = new System.Drawing.Point(129, 12);
            this.chkLED4.Name = "chkLED4";
            this.chkLED4.Size = new System.Drawing.Size(35, 31);
            this.chkLED4.TabIndex = 0;
            this.chkLED4.Text = "....";
            this.chkLED4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED4.UseVisualStyleBackColor = true;
            this.chkLED4.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
            // 
            // chkLED3
            // 
            this.chkLED3.AutoSize = true;
            this.chkLED3.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLED3.Location = new System.Drawing.Point(88, 12);
            this.chkLED3.Name = "chkLED3";
            this.chkLED3.Size = new System.Drawing.Size(23, 31);
            this.chkLED3.TabIndex = 0;
            this.chkLED3.Text = "..";
            this.chkLED3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED3.UseVisualStyleBackColor = true;
            this.chkLED3.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
            // 
            // chkLED2
            // 
            this.chkLED2.AutoSize = true;
            this.chkLED2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLED2.Location = new System.Drawing.Point(47, 12);
            this.chkLED2.Name = "chkLED2";
            this.chkLED2.Size = new System.Drawing.Size(23, 31);
            this.chkLED2.TabIndex = 0;
            this.chkLED2.Text = "..";
            this.chkLED2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED2.UseVisualStyleBackColor = true;
            this.chkLED2.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
            // 
            // chkLED1
            // 
            this.chkLED1.AutoSize = true;
            this.chkLED1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLED1.Location = new System.Drawing.Point(6, 12);
            this.chkLED1.Name = "chkLED1";
            this.chkLED1.Size = new System.Drawing.Size(17, 31);
            this.chkLED1.TabIndex = 0;
            this.chkLED1.Text = ".";
            this.chkLED1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkLED1.UseVisualStyleBackColor = true;
            this.chkLED1.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
            // 
            // gbxRumble
            // 
            this.gbxRumble.Controls.Add(this.rbRumbleOff);
            this.gbxRumble.Controls.Add(this.rbRumbleOn);
            this.gbxRumble.Location = new System.Drawing.Point(180, 149);
            this.gbxRumble.Name = "gbxRumble";
            this.gbxRumble.Size = new System.Drawing.Size(166, 40);
            this.gbxRumble.TabIndex = 2;
            this.gbxRumble.TabStop = false;
            this.gbxRumble.Text = "Rumble";
            // 
            // rbRumbleOff
            // 
            this.rbRumbleOff.AutoSize = true;
            this.rbRumbleOff.Checked = true;
            this.rbRumbleOff.Location = new System.Drawing.Point(96, 14);
            this.rbRumbleOff.Name = "rbRumbleOff";
            this.rbRumbleOff.Size = new System.Drawing.Size(64, 17);
            this.rbRumbleOff.TabIndex = 0;
            this.rbRumbleOff.TabStop = true;
            this.rbRumbleOff.Text = "Turn Off";
            this.rbRumbleOff.UseVisualStyleBackColor = true;
            // 
            // rbRumbleOn
            // 
            this.rbRumbleOn.AutoSize = true;
            this.rbRumbleOn.Location = new System.Drawing.Point(6, 14);
            this.rbRumbleOn.Name = "rbRumbleOn";
            this.rbRumbleOn.Size = new System.Drawing.Size(64, 17);
            this.rbRumbleOn.TabIndex = 0;
            this.rbRumbleOn.TabStop = true;
            this.rbRumbleOn.Text = "Turn On";
            this.rbRumbleOn.UseVisualStyleBackColor = true;
            this.rbRumbleOn.CheckedChanged += new System.EventHandler(this.rbRumbleOn_CheckedChanged);
            // 
            // gbxBattery
            // 
            this.gbxBattery.Controls.Add(this.lblBatteryLevel);
            this.gbxBattery.Controls.Add(this.pgbBatteryLevel);
            this.gbxBattery.Controls.Add(this.panel1);
            this.gbxBattery.Location = new System.Drawing.Point(180, 195);
            this.gbxBattery.Name = "gbxBattery";
            this.gbxBattery.Size = new System.Drawing.Size(166, 50);
            this.gbxBattery.TabIndex = 2;
            this.gbxBattery.TabStop = false;
            this.gbxBattery.Text = "Battery Level";
            // 
            // lblBatteryLevel
            // 
            this.lblBatteryLevel.AutoSize = true;
            this.lblBatteryLevel.Location = new System.Drawing.Point(112, 25);
            this.lblBatteryLevel.Name = "lblBatteryLevel";
            this.lblBatteryLevel.Size = new System.Drawing.Size(27, 13);
            this.lblBatteryLevel.TabIndex = 1;
            this.lblBatteryLevel.Text = "N/A";
            this.lblBatteryLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgbBatteryLevel
            // 
            this.pgbBatteryLevel.Location = new System.Drawing.Point(6, 19);
            this.pgbBatteryLevel.Maximum = 200;
            this.pgbBatteryLevel.Name = "pgbBatteryLevel";
            this.pgbBatteryLevel.Size = new System.Drawing.Size(100, 23);
            this.pgbBatteryLevel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(104, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(4, 12);
            this.panel1.TabIndex = 2;
            // 
            // gbxAccel
            // 
            this.gbxAccel.Controls.Add(this.lblAccelZ);
            this.gbxAccel.Controls.Add(this.lblAccelY);
            this.gbxAccel.Controls.Add(this.lblAccelX);
            this.gbxAccel.Controls.Add(this.label3);
            this.gbxAccel.Controls.Add(this.label2);
            this.gbxAccel.Controls.Add(this.label1);
            this.gbxAccel.Location = new System.Drawing.Point(180, 251);
            this.gbxAccel.Name = "gbxAccel";
            this.gbxAccel.Size = new System.Drawing.Size(166, 84);
            this.gbxAccel.TabIndex = 2;
            this.gbxAccel.TabStop = false;
            this.gbxAccel.Text = "Accelerometer Data";
            // 
            // lblAccelZ
            // 
            this.lblAccelZ.AutoSize = true;
            this.lblAccelZ.Location = new System.Drawing.Point(30, 59);
            this.lblAccelZ.Name = "lblAccelZ";
            this.lblAccelZ.Size = new System.Drawing.Size(122, 13);
            this.lblAccelZ.TabIndex = 1;
            this.lblAccelZ.Text = "Accelerometer Disabled!";
            // 
            // lblAccelY
            // 
            this.lblAccelY.AutoSize = true;
            this.lblAccelY.Location = new System.Drawing.Point(30, 41);
            this.lblAccelY.Name = "lblAccelY";
            this.lblAccelY.Size = new System.Drawing.Size(122, 13);
            this.lblAccelY.TabIndex = 1;
            this.lblAccelY.Text = "Accelerometer Disabled!";
            // 
            // lblAccelX
            // 
            this.lblAccelX.AutoSize = true;
            this.lblAccelX.Location = new System.Drawing.Point(30, 23);
            this.lblAccelX.Name = "lblAccelX";
            this.lblAccelX.Size = new System.Drawing.Size(122, 13);
            this.lblAccelX.TabIndex = 1;
            this.lblAccelX.Text = "Accelerometer Disabled!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // tmrChaseLEDs
            // 
            this.tmrChaseLEDs.Tick += new System.EventHandler(this.tmrChaseLEDs_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 343);
            this.Controls.Add(this.gbxAccel);
            this.Controls.Add(this.gbxBattery);
            this.Controls.Add(this.gbxRumble);
            this.Controls.Add(this.gbxLEDs);
            this.Controls.Add(this.gbButtons);
            this.Controls.Add(this.gbxConnectionSettings);
            this.Name = "frmMain";
            this.Text = "Wii Remote Test Application";
            this.gbxConnectionSettings.ResumeLayout(false);
            this.gbxConnectionSettings.PerformLayout();
            this.gbButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbxLEDs.ResumeLayout(false);
            this.gbxLEDs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChaseSpeed)).EndInit();
            this.gbxRumble.ResumeLayout(false);
            this.gbxRumble.PerformLayout();
            this.gbxBattery.ResumeLayout(false);
            this.gbxBattery.PerformLayout();
            this.gbxAccel.ResumeLayout(false);
            this.gbxAccel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxConnectionSettings;
        private System.Windows.Forms.Button btnConnectDisconnect;
        private System.Windows.Forms.GroupBox gbButtons;
        private System.Windows.Forms.Panel pnlUp;
        private System.Windows.Forms.Panel pnlB;
        private System.Windows.Forms.Panel pnlA;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlDown;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlMinus;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.Panel pnlPlus;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.GroupBox gbxLEDs;
        private System.Windows.Forms.CheckBox chkLED4;
        private System.Windows.Forms.CheckBox chkLED3;
        private System.Windows.Forms.CheckBox chkLED2;
        private System.Windows.Forms.CheckBox chkLED1;
        private System.Windows.Forms.GroupBox gbxRumble;
        private System.Windows.Forms.RadioButton rbRumbleOn;
        private System.Windows.Forms.RadioButton rbRumbleOff;
        private System.Windows.Forms.GroupBox gbxBattery;
        private System.Windows.Forms.ProgressBar pgbBatteryLevel;
        private System.Windows.Forms.Label lblBatteryLevel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbxAccel;
        private System.Windows.Forms.Label lblReportingMode;
        private System.Windows.Forms.ComboBox cboReportingMode;
        private System.Windows.Forms.Label lblAccelZ;
        private System.Windows.Forms.Label lblAccelY;
        private System.Windows.Forms.Label lblAccelX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChaseLEDS;
        private System.Windows.Forms.Timer tmrChaseLEDs;
        private System.Windows.Forms.NumericUpDown nudChaseSpeed;
    }
}

