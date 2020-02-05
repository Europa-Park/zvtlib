namespace ZvtEcrInterfaceTester {
	partial class Main {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
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
		private void InitializeComponent() {
			this.bRegister = new System.Windows.Forms.Button();
			this.tLog = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.bPayment = new System.Windows.Forms.Button();
			this.numPayAmount = new System.Windows.Forms.NumericUpDown();
			this.numReceiptNumber = new System.Windows.Forms.NumericUpDown();
			this.bReversal = new System.Windows.Forms.Button();
			this.bEndOfDay = new System.Windows.Forms.Button();
			this.bPayAsync = new System.Windows.Forms.Button();
			this.bRefundAsync = new System.Windows.Forms.Button();
			this.bReverseAsync = new System.Windows.Forms.Button();
			this.numRefundAmount = new System.Windows.Forms.NumericUpDown();
			this.bRefund = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cComPort = new System.Windows.Forms.ComboBox();
			this.cLogToTextBox = new System.Windows.Forms.CheckBox();
			this.bCreateInterface = new System.Windows.Forms.Button();
			this.cBaud = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bRegisterAsync = new System.Windows.Forms.Button();
			this.cPermBlockPrint = new System.Windows.Forms.CheckBox();
			this.cPermStatus = new System.Windows.Forms.CheckBox();
			this.cPermLinePrint = new System.Windows.Forms.CheckBox();
			this.cPermInterStatus = new System.Windows.Forms.CheckBox();
			this.cPermAbort = new System.Windows.Forms.CheckBox();
			this.cPermCompletion = new System.Windows.Forms.CheckBox();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.bClearLog = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPayAmount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numReceiptNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRefundAmount)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// bRegister
			// 
			this.bRegister.Location = new System.Drawing.Point(6, 19);
			this.bRegister.Name = "bRegister";
			this.bRegister.Size = new System.Drawing.Size(99, 23);
			this.bRegister.TabIndex = 0;
			this.bRegister.Text = "Register";
			this.bRegister.UseVisualStyleBackColor = true;
			this.bRegister.Click += new System.EventHandler(this.bRegister_Click);
			// 
			// tLog
			// 
			this.tLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tLog.Location = new System.Drawing.Point(0, 0);
			this.tLog.Multiline = true;
			this.tLog.Name = "tLog";
			this.tLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tLog.Size = new System.Drawing.Size(660, 177);
			this.tLog.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.propertyGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(660, 399);
			this.panel1.TabIndex = 2;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.bPayment);
			this.groupBox3.Controls.Add(this.numPayAmount);
			this.groupBox3.Controls.Add(this.numReceiptNumber);
			this.groupBox3.Controls.Add(this.bReversal);
			this.groupBox3.Controls.Add(this.bEndOfDay);
			this.groupBox3.Controls.Add(this.bPayAsync);
			this.groupBox3.Controls.Add(this.bRefundAsync);
			this.groupBox3.Controls.Add(this.bReverseAsync);
			this.groupBox3.Controls.Add(this.numRefundAmount);
			this.groupBox3.Controls.Add(this.bRefund);
			this.groupBox3.Location = new System.Drawing.Point(12, 250);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(310, 143);
			this.groupBox3.TabIndex = 28;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Payment";
			// 
			// bPayment
			// 
			this.bPayment.Location = new System.Drawing.Point(6, 19);
			this.bPayment.Name = "bPayment";
			this.bPayment.Size = new System.Drawing.Size(99, 23);
			this.bPayment.TabIndex = 3;
			this.bPayment.Text = "Pay";
			this.bPayment.UseVisualStyleBackColor = true;
			this.bPayment.Click += new System.EventHandler(this.bPayment_Click);
			// 
			// numPayAmount
			// 
			this.numPayAmount.Location = new System.Drawing.Point(217, 21);
			this.numPayAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numPayAmount.Name = "numPayAmount";
			this.numPayAmount.Size = new System.Drawing.Size(53, 20);
			this.numPayAmount.TabIndex = 4;
			this.numPayAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numReceiptNumber
			// 
			this.numReceiptNumber.Location = new System.Drawing.Point(217, 79);
			this.numReceiptNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numReceiptNumber.Name = "numReceiptNumber";
			this.numReceiptNumber.Size = new System.Drawing.Size(53, 20);
			this.numReceiptNumber.TabIndex = 7;
			// 
			// bReversal
			// 
			this.bReversal.Location = new System.Drawing.Point(6, 77);
			this.bReversal.Name = "bReversal";
			this.bReversal.Size = new System.Drawing.Size(99, 23);
			this.bReversal.TabIndex = 8;
			this.bReversal.Text = "Reverse";
			this.bReversal.UseVisualStyleBackColor = true;
			this.bReversal.Click += new System.EventHandler(this.bReversal_Click);
			// 
			// bEndOfDay
			// 
			this.bEndOfDay.Location = new System.Drawing.Point(6, 106);
			this.bEndOfDay.Name = "bEndOfDay";
			this.bEndOfDay.Size = new System.Drawing.Size(99, 23);
			this.bEndOfDay.TabIndex = 16;
			this.bEndOfDay.Text = "End Of Day";
			this.bEndOfDay.UseVisualStyleBackColor = true;
			this.bEndOfDay.Click += new System.EventHandler(this.bEndOfDay_Click);
			// 
			// bPayAsync
			// 
			this.bPayAsync.Location = new System.Drawing.Point(111, 19);
			this.bPayAsync.Name = "bPayAsync";
			this.bPayAsync.Size = new System.Drawing.Size(100, 23);
			this.bPayAsync.TabIndex = 10;
			this.bPayAsync.Text = "PayAsync";
			this.bPayAsync.UseVisualStyleBackColor = true;
			this.bPayAsync.Click += new System.EventHandler(this.bPayAsync_Click);
			// 
			// bRefundAsync
			// 
			this.bRefundAsync.Enabled = false;
			this.bRefundAsync.Location = new System.Drawing.Point(111, 48);
			this.bRefundAsync.Name = "bRefundAsync";
			this.bRefundAsync.Size = new System.Drawing.Size(100, 23);
			this.bRefundAsync.TabIndex = 15;
			this.bRefundAsync.Text = "RefundAsync";
			this.bRefundAsync.UseVisualStyleBackColor = true;
			// 
			// bReverseAsync
			// 
			this.bReverseAsync.Location = new System.Drawing.Point(111, 77);
			this.bReverseAsync.Name = "bReverseAsync";
			this.bReverseAsync.Size = new System.Drawing.Size(100, 23);
			this.bReverseAsync.TabIndex = 11;
			this.bReverseAsync.Text = "ReverseAsync";
			this.bReverseAsync.UseVisualStyleBackColor = true;
			this.bReverseAsync.Click += new System.EventHandler(this.bReverseAsync_Click);
			// 
			// numRefundAmount
			// 
			this.numRefundAmount.Location = new System.Drawing.Point(217, 50);
			this.numRefundAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numRefundAmount.Name = "numRefundAmount";
			this.numRefundAmount.Size = new System.Drawing.Size(53, 20);
			this.numRefundAmount.TabIndex = 14;
			this.numRefundAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// bRefund
			// 
			this.bRefund.Location = new System.Drawing.Point(6, 48);
			this.bRefund.Name = "bRefund";
			this.bRefund.Size = new System.Drawing.Size(99, 23);
			this.bRefund.TabIndex = 13;
			this.bRefund.Text = "Refund";
			this.bRefund.UseVisualStyleBackColor = true;
			this.bRefund.Click += new System.EventHandler(this.bRefund_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cComPort);
			this.groupBox2.Controls.Add(this.cLogToTextBox);
			this.groupBox2.Controls.Add(this.bCreateInterface);
			this.groupBox2.Controls.Add(this.cBaud);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(310, 74);
			this.groupBox2.TabIndex = 27;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Connection";
			// 
			// cComPort
			// 
			this.cComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cComPort.FormattingEnabled = true;
			this.cComPort.Location = new System.Drawing.Point(111, 20);
			this.cComPort.Name = "cComPort";
			this.cComPort.Size = new System.Drawing.Size(100, 21);
			this.cComPort.TabIndex = 19;
			// 
			// cLogToTextBox
			// 
			this.cLogToTextBox.AutoSize = true;
			this.cLogToTextBox.Location = new System.Drawing.Point(7, 47);
			this.cLogToTextBox.Name = "cLogToTextBox";
			this.cLogToTextBox.Size = new System.Drawing.Size(194, 17);
			this.cLogToTextBox.TabIndex = 18;
			this.cLogToTextBox.Text = "Log debug output to TextBox (slow)";
			this.cLogToTextBox.UseVisualStyleBackColor = true;
			// 
			// bCreateInterface
			// 
			this.bCreateInterface.Location = new System.Drawing.Point(6, 19);
			this.bCreateInterface.Name = "bCreateInterface";
			this.bCreateInterface.Size = new System.Drawing.Size(99, 23);
			this.bCreateInterface.TabIndex = 1;
			this.bCreateInterface.Text = "Create Interface";
			this.bCreateInterface.UseVisualStyleBackColor = true;
			this.bCreateInterface.Click += new System.EventHandler(this.bCreateInterface_Click);
			// 
			// cBaud
			// 
			this.cBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cBaud.FormattingEnabled = true;
			this.cBaud.Items.AddRange(new object[] {
            "9600",
            "115200"});
			this.cBaud.Location = new System.Drawing.Point(217, 20);
			this.cBaud.Name = "cBaud";
			this.cBaud.Size = new System.Drawing.Size(78, 21);
			this.cBaud.TabIndex = 17;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.bRegister);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.bRegisterAsync);
			this.groupBox1.Controls.Add(this.cPermBlockPrint);
			this.groupBox1.Controls.Add(this.cPermStatus);
			this.groupBox1.Controls.Add(this.cPermLinePrint);
			this.groupBox1.Controls.Add(this.cPermInterStatus);
			this.groupBox1.Controls.Add(this.cPermAbort);
			this.groupBox1.Controls.Add(this.cPermCompletion);
			this.groupBox1.Location = new System.Drawing.Point(12, 92);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(310, 152);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Register";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(169, 13);
			this.label1.TabIndex = 25;
			this.label1.Text = "Specify permitted ZVT commands:";
			// 
			// bRegisterAsync
			// 
			this.bRegisterAsync.Location = new System.Drawing.Point(111, 19);
			this.bRegisterAsync.Name = "bRegisterAsync";
			this.bRegisterAsync.Size = new System.Drawing.Size(100, 23);
			this.bRegisterAsync.TabIndex = 9;
			this.bRegisterAsync.Text = "RegisterAsync";
			this.bRegisterAsync.UseVisualStyleBackColor = true;
			this.bRegisterAsync.Click += new System.EventHandler(this.bRegisterAsync_Click);
			// 
			// cPermBlockPrint
			// 
			this.cPermBlockPrint.AutoSize = true;
			this.cPermBlockPrint.Location = new System.Drawing.Point(135, 117);
			this.cPermBlockPrint.Name = "cPermBlockPrint";
			this.cPermBlockPrint.Size = new System.Drawing.Size(123, 17);
			this.cPermBlockPrint.TabIndex = 24;
			this.cPermBlockPrint.Text = "Block Print (0x06D3)";
			this.cPermBlockPrint.UseVisualStyleBackColor = true;
			// 
			// cPermStatus
			// 
			this.cPermStatus.AutoSize = true;
			this.cPermStatus.Checked = true;
			this.cPermStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cPermStatus.Location = new System.Drawing.Point(7, 71);
			this.cPermStatus.Name = "cPermStatus";
			this.cPermStatus.Size = new System.Drawing.Size(100, 17);
			this.cPermStatus.TabIndex = 19;
			this.cPermStatus.Text = "Status (0x040F)";
			this.cPermStatus.UseVisualStyleBackColor = true;
			// 
			// cPermLinePrint
			// 
			this.cPermLinePrint.AutoSize = true;
			this.cPermLinePrint.Checked = true;
			this.cPermLinePrint.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cPermLinePrint.Location = new System.Drawing.Point(7, 117);
			this.cPermLinePrint.Name = "cPermLinePrint";
			this.cPermLinePrint.Size = new System.Drawing.Size(116, 17);
			this.cPermLinePrint.TabIndex = 23;
			this.cPermLinePrint.Text = "Line Print (0x06D1)";
			this.cPermLinePrint.UseVisualStyleBackColor = true;
			// 
			// cPermInterStatus
			// 
			this.cPermInterStatus.AutoSize = true;
			this.cPermInterStatus.Checked = true;
			this.cPermInterStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cPermInterStatus.Location = new System.Drawing.Point(135, 71);
			this.cPermInterStatus.Name = "cPermInterStatus";
			this.cPermInterStatus.Size = new System.Drawing.Size(161, 17);
			this.cPermInterStatus.TabIndex = 20;
			this.cPermInterStatus.Text = "Intermediate Status (0x04FF)";
			this.cPermInterStatus.UseVisualStyleBackColor = true;
			// 
			// cPermAbort
			// 
			this.cPermAbort.AutoSize = true;
			this.cPermAbort.Checked = true;
			this.cPermAbort.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cPermAbort.Location = new System.Drawing.Point(135, 94);
			this.cPermAbort.Name = "cPermAbort";
			this.cPermAbort.Size = new System.Drawing.Size(96, 17);
			this.cPermAbort.TabIndex = 22;
			this.cPermAbort.Text = "Abort (0x061E)";
			this.cPermAbort.UseVisualStyleBackColor = true;
			// 
			// cPermCompletion
			// 
			this.cPermCompletion.AutoSize = true;
			this.cPermCompletion.Checked = true;
			this.cPermCompletion.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cPermCompletion.Location = new System.Drawing.Point(7, 94);
			this.cPermCompletion.Name = "cPermCompletion";
			this.cPermCompletion.Size = new System.Drawing.Size(122, 17);
			this.cPermCompletion.TabIndex = 21;
			this.cPermCompletion.Text = "Completion (0x060F)";
			this.cPermCompletion.UseVisualStyleBackColor = true;
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.Location = new System.Drawing.Point(328, 18);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid1.Size = new System.Drawing.Size(320, 375);
			this.propertyGrid1.TabIndex = 12;
			this.propertyGrid1.ToolbarVisible = false;
			// 
			// bClearLog
			// 
			this.bClearLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bClearLog.Location = new System.Drawing.Point(0, 177);
			this.bClearLog.Name = "bClearLog";
			this.bClearLog.Size = new System.Drawing.Size(660, 23);
			this.bClearLog.TabIndex = 12;
			this.bClearLog.Text = "Clear Log";
			this.bClearLog.UseVisualStyleBackColor = true;
			this.bClearLog.Click += new System.EventHandler(this.bClearLog_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tLog);
			this.panel2.Controls.Add(this.bClearLog);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 399);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(660, 200);
			this.panel2.TabIndex = 3;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(660, 599);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "Main";
			this.Text = "ZvtEcrInterfaceTester";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numPayAmount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numReceiptNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRefundAmount)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bRegister;
		private System.Windows.Forms.TextBox tLog;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button bCreateInterface;
		private System.Windows.Forms.Button bPayment;
		private System.Windows.Forms.NumericUpDown numPayAmount;
		private System.Windows.Forms.Button bReversal;
		private System.Windows.Forms.NumericUpDown numReceiptNumber;
		private System.Windows.Forms.Button bRegisterAsync;
		private System.Windows.Forms.Button bReverseAsync;
		private System.Windows.Forms.Button bPayAsync;
		private System.Windows.Forms.Button bClearLog;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button bRefundAsync;
		private System.Windows.Forms.NumericUpDown numRefundAmount;
		private System.Windows.Forms.Button bRefund;
		private System.Windows.Forms.Button bEndOfDay;
		private System.Windows.Forms.ComboBox cBaud;
        private System.Windows.Forms.CheckBox cPermStatus;
		private System.Windows.Forms.CheckBox cPermInterStatus;
		private System.Windows.Forms.CheckBox cPermAbort;
		private System.Windows.Forms.CheckBox cPermCompletion;
		private System.Windows.Forms.CheckBox cPermBlockPrint;
		private System.Windows.Forms.CheckBox cPermLinePrint;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cComPort;
		private System.Windows.Forms.CheckBox cLogToTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}

