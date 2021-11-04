namespace UsbDevBulkHostApp
{
    partial class Form1
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
            this.transmitButton = new System.Windows.Forms.Button();
            this.richTextBoxCommand = new System.Windows.Forms.RichTextBox();
            this.textBoxToTransmit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.channelCheckBox = new System.Windows.Forms.CheckedListBox();
            this.filterCheckBox = new System.Windows.Forms.CheckedListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.startStop = new System.Windows.Forms.Button();
            this.sampleRate = new System.Windows.Forms.TextBox();
            this.decFactorTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.decFactor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.TextBoxData = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // transmitButton
            // 
            this.transmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.transmitButton.Enabled = false;
            this.transmitButton.Location = new System.Drawing.Point(453, 228);
            this.transmitButton.Name = "transmitButton";
            this.transmitButton.Size = new System.Drawing.Size(88, 23);
            this.transmitButton.TabIndex = 1;
            this.transmitButton.Text = "Transmit";
            this.transmitButton.UseVisualStyleBackColor = true;
            this.transmitButton.Click += new System.EventHandler(this.transmitButton_Click);
            // 
            // richTextBoxCommand
            // 
            this.richTextBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxCommand.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxCommand.DetectUrls = false;
            this.richTextBoxCommand.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxCommand.Location = new System.Drawing.Point(10, 270);
            this.richTextBoxCommand.Name = "richTextBoxCommand";
            this.richTextBoxCommand.ReadOnly = true;
            this.richTextBoxCommand.Size = new System.Drawing.Size(518, 126);
            this.richTextBoxCommand.TabIndex = 1;
            this.richTextBoxCommand.TabStop = false;
            this.richTextBoxCommand.Text = "";
            // 
            // textBoxToTransmit
            // 
            this.textBoxToTransmit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxToTransmit.Location = new System.Drawing.Point(13, 228);
            this.textBoxToTransmit.Name = "textBoxToTransmit";
            this.textBoxToTransmit.Size = new System.Drawing.Size(426, 20);
            this.textBoxToTransmit.TabIndex = 0;
            this.textBoxToTransmit.Text = "start";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(552, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(281, 97);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Record";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // channelCheckBox
            // 
            this.channelCheckBox.CheckOnClick = true;
            this.channelCheckBox.FormattingEnabled = true;
            this.channelCheckBox.Items.AddRange(new object[] {
            "Ain  0",
            "Ain  1",
            "Ain  2",
            "Ain  3",
            "Ain  4",
            "Ain  5",
            "Ain  6",
            "Ain  7",
            "Din  8",
            "Rate 9",
            "Cnt  10"});
            this.channelCheckBox.Location = new System.Drawing.Point(10, 45);
            this.channelCheckBox.Name = "channelCheckBox";
            this.channelCheckBox.Size = new System.Drawing.Size(78, 169);
            this.channelCheckBox.TabIndex = 7;
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.CheckOnClick = true;
            this.filterCheckBox.FormattingEnabled = true;
            this.filterCheckBox.Items.AddRange(new object[] {
            "fil0",
            "fil1",
            "fil2",
            "fil3",
            "fil4",
            "fi5",
            "fil6",
            "fil7"});
            this.filterCheckBox.Location = new System.Drawing.Point(113, 45);
            this.filterCheckBox.Name = "filterCheckBox";
            this.filterCheckBox.Size = new System.Drawing.Size(71, 124);
            this.filterCheckBox.TabIndex = 8;
            // 
            // startStop
            // 
            this.startStop.Location = new System.Drawing.Point(451, 97);
            this.startStop.Name = "startStop";
            this.startStop.Size = new System.Drawing.Size(88, 23);
            this.startStop.TabIndex = 10;
            this.startStop.Text = "Push to Init";
            this.startStop.UseVisualStyleBackColor = true;
            this.startStop.Click += new System.EventHandler(this.startStop_Click);
            // 
            // sampleRate
            // 
            this.sampleRate.Location = new System.Drawing.Point(281, 45);
            this.sampleRate.Name = "sampleRate";
            this.sampleRate.Size = new System.Drawing.Size(89, 20);
            this.sampleRate.TabIndex = 11;
            this.sampleRate.Text = "6000";
            // 
            // decFactorTB
            // 
            this.decFactorTB.Location = new System.Drawing.Point(281, 71);
            this.decFactorTB.Name = "decFactorTB";
            this.decFactorTB.Size = new System.Drawing.Size(89, 20);
            this.decFactorTB.TabIndex = 12;
            this.decFactorTB.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "SRate";
            // 
            // decFactor
            // 
            this.decFactor.AutoSize = true;
            this.decFactor.Location = new System.Drawing.Point(210, 74);
            this.decFactor.Name = "decFactor";
            this.decFactor.Size = new System.Drawing.Size(60, 13);
            this.decFactor.TabIndex = 14;
            this.decFactor.Text = "Dec Factor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Packet Size";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.listBox1.Location = new System.Drawing.Point(451, 39);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(57, 52);
            this.listBox1.TabIndex = 17;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // TextBoxData
            // 
            this.TextBoxData.Location = new System.Drawing.Point(12, 414);
            this.TextBoxData.Multiline = true;
            this.TextBoxData.Name = "TextBoxData";
            this.TextBoxData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxData.Size = new System.Drawing.Size(524, 124);
            this.TextBoxData.TabIndex = 18;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(281, 120);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(66, 17);
            this.checkBox2.TabIndex = 19;
            this.checkBox2.Text = "Receive";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(281, 143);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(49, 17);
            this.checkBox3.TabIndex = 20;
            this.checkBox3.Text = "Dout";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Command";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "SyncTest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AcceptButton = this.transmitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 541);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.TextBoxData);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.decFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.decFactorTB);
            this.Controls.Add(this.sampleRate);
            this.Controls.Add(this.startStop);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.channelCheckBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxToTransmit);
            this.Controls.Add(this.richTextBoxCommand);
            this.Controls.Add(this.transmitButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DATAQ LIBUSB Test Program";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button transmitButton;
        private System.Windows.Forms.RichTextBox richTextBoxCommand;
        private System.Windows.Forms.TextBox textBoxToTransmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckedListBox channelCheckBox;
        private System.Windows.Forms.CheckedListBox filterCheckBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button startStop;
        private System.Windows.Forms.TextBox sampleRate;
        private System.Windows.Forms.TextBox decFactorTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label decFactor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox TextBoxData;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}

