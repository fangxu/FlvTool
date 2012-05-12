namespace FlvTool
{
    partial class FlvToolMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlvToolMain));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textVideo = new System.Windows.Forms.TextBox();
            this.textAudio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkAudio = new System.Windows.Forms.CheckBox();
            this.buttonMerger = new System.Windows.Forms.Button();
            this.textStat = new System.Windows.Forms.TextBox();
            this.comboBoxPart = new System.Windows.Forms.ComboBox();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.buttonAnaly = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textFlv = new System.Windows.Forms.TextBox();
            this.upDownRate = new System.Windows.Forms.NumericUpDown();
            this.buttonBlack = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkTop = new System.Windows.Forms.CheckBox();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonAdvanced = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.upDownRate)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 442);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(521, 23);
            this.progressBar.TabIndex = 0;
            // 
            // textVideo
            // 
            this.textVideo.AllowDrop = true;
            this.textVideo.Location = new System.Drawing.Point(59, 7);
            this.textVideo.Name = "textVideo";
            this.textVideo.Size = new System.Drawing.Size(378, 21);
            this.textVideo.TabIndex = 2;
            this.textVideo.DragDrop += new System.Windows.Forms.DragEventHandler(this.textVideo_DragDrop);
            this.textVideo.DragEnter += new System.Windows.Forms.DragEventHandler(this.textVideo_DragEnter);
            // 
            // textAudio
            // 
            this.textAudio.AllowDrop = true;
            this.textAudio.Enabled = false;
            this.textAudio.Location = new System.Drawing.Point(59, 34);
            this.textAudio.Name = "textAudio";
            this.textAudio.Size = new System.Drawing.Size(378, 21);
            this.textAudio.TabIndex = 3;
            this.textAudio.DragDrop += new System.Windows.Forms.DragEventHandler(this.textAudio_DragDrop);
            this.textAudio.DragEnter += new System.Windows.Forms.DragEventHandler(this.textAudio_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "video:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "audio:";
            // 
            // checkAudio
            // 
            this.checkAudio.AutoSize = true;
            this.checkAudio.Checked = true;
            this.checkAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAudio.Location = new System.Drawing.Point(444, 37);
            this.checkAudio.Name = "checkAudio";
            this.checkAudio.Size = new System.Drawing.Size(42, 16);
            this.checkAudio.TabIndex = 6;
            this.checkAudio.Text = "m4a";
            this.checkAudio.UseVisualStyleBackColor = true;
            this.checkAudio.CheckedChanged += new System.EventHandler(this.checkAudio_CheckedChanged);
            // 
            // buttonMerger
            // 
            this.buttonMerger.Location = new System.Drawing.Point(11, 127);
            this.buttonMerger.Name = "buttonMerger";
            this.buttonMerger.Size = new System.Drawing.Size(75, 23);
            this.buttonMerger.TabIndex = 7;
            this.buttonMerger.Text = "Merger";
            this.buttonMerger.UseVisualStyleBackColor = true;
            this.buttonMerger.Click += new System.EventHandler(this.buttonMerger_Click);
            // 
            // textStat
            // 
            this.textStat.Location = new System.Drawing.Point(0, 156);
            this.textStat.Multiline = true;
            this.textStat.Name = "textStat";
            this.textStat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textStat.Size = new System.Drawing.Size(520, 280);
            this.textStat.TabIndex = 8;
            // 
            // comboBoxPart
            // 
            this.comboBoxPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPart.FormattingEnabled = true;
            this.comboBoxPart.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxPart.Location = new System.Drawing.Point(60, 88);
            this.comboBoxPart.Name = "comboBoxPart";
            this.comboBoxPart.Size = new System.Drawing.Size(71, 20);
            this.comboBoxPart.TabIndex = 9;
            // 
            // buttonSplit
            // 
            this.buttonSplit.Location = new System.Drawing.Point(175, 127);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(75, 23);
            this.buttonSplit.TabIndex = 10;
            this.buttonSplit.Text = "Split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // buttonAnaly
            // 
            this.buttonAnaly.Location = new System.Drawing.Point(93, 127);
            this.buttonAnaly.Name = "buttonAnaly";
            this.buttonAnaly.Size = new System.Drawing.Size(75, 23);
            this.buttonAnaly.TabIndex = 11;
            this.buttonAnaly.Text = "Analy";
            this.buttonAnaly.UseVisualStyleBackColor = true;
            this.buttonAnaly.Click += new System.EventHandler(this.buttonAnaly_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "flv:";
            // 
            // textFlv
            // 
            this.textFlv.AllowDrop = true;
            this.textFlv.Location = new System.Drawing.Point(59, 61);
            this.textFlv.Name = "textFlv";
            this.textFlv.Size = new System.Drawing.Size(378, 21);
            this.textFlv.TabIndex = 13;
            this.textFlv.DragDrop += new System.Windows.Forms.DragEventHandler(this.textFlv_DragDrop);
            this.textFlv.DragEnter += new System.Windows.Forms.DragEventHandler(this.textFlv_DragEnter);
            // 
            // upDownRate
            // 
            this.upDownRate.Location = new System.Drawing.Point(210, 88);
            this.upDownRate.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.upDownRate.Name = "upDownRate";
            this.upDownRate.Size = new System.Drawing.Size(52, 21);
            this.upDownRate.TabIndex = 16;
            this.upDownRate.ThousandsSeparator = true;
            // 
            // buttonBlack
            // 
            this.buttonBlack.Location = new System.Drawing.Point(257, 127);
            this.buttonBlack.Name = "buttonBlack";
            this.buttonBlack.Size = new System.Drawing.Size(75, 23);
            this.buttonBlack.TabIndex = 17;
            this.buttonBlack.Text = "Black";
            this.buttonBlack.UseVisualStyleBackColor = true;
            this.buttonBlack.Click += new System.EventHandler(this.buttonBlack_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "Parts:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "Kbps";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "Rate:";
            // 
            // checkTop
            // 
            this.checkTop.AutoSize = true;
            this.checkTop.Location = new System.Drawing.Point(323, 91);
            this.checkTop.Name = "checkTop";
            this.checkTop.Size = new System.Drawing.Size(84, 16);
            this.checkTop.TabIndex = 21;
            this.checkTop.Text = "Always Top";
            this.checkTop.UseVisualStyleBackColor = true;
            this.checkTop.CheckedChanged += new System.EventHandler(this.checkTop_CheckedChanged);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(421, 127);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(75, 23);
            this.buttonAbout.TabIndex = 22;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Location = new System.Drawing.Point(339, 127);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(75, 23);
            this.buttonAdvanced.TabIndex = 23;
            this.buttonAdvanced.Text = "Advanced";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // FlvToolMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 462);
            this.Controls.Add(this.buttonAdvanced);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.checkTop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonBlack);
            this.Controls.Add(this.upDownRate);
            this.Controls.Add(this.textFlv);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonAnaly);
            this.Controls.Add(this.buttonSplit);
            this.Controls.Add(this.comboBoxPart);
            this.Controls.Add(this.textStat);
            this.Controls.Add(this.buttonMerger);
            this.Controls.Add(this.checkAudio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textAudio);
            this.Controls.Add(this.textVideo);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FlvToolMain";
            this.Text = "FlvTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlvToolMain_FormClosing);
            this.Load += new System.EventHandler(this.FlvToolMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upDownRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textVideo;
        private System.Windows.Forms.TextBox textAudio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkAudio;
        private System.Windows.Forms.Button buttonMerger;
        public System.Windows.Forms.TextBox textStat;
        private System.Windows.Forms.ComboBox comboBoxPart;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.Button buttonAnaly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textFlv;
        private System.Windows.Forms.NumericUpDown upDownRate;
        private System.Windows.Forms.Button buttonBlack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkTop;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonAdvanced;
    }
}

