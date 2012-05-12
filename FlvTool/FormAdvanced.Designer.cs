namespace FlvTool
{
    partial class FormAdvanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdvanced));
            this.label1 = new System.Windows.Forms.Label();
            this.upDownRate = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBlack = new System.Windows.Forms.Button();
            this.listViewFlv = new System.Windows.Forms.ListView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.checkTop = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.upDownRate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rate:";
            // 
            // upDownRate
            // 
            this.upDownRate.Location = new System.Drawing.Point(49, 288);
            this.upDownRate.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.upDownRate.Name = "upDownRate";
            this.upDownRate.Size = new System.Drawing.Size(55, 21);
            this.upDownRate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kbps";
            // 
            // buttonBlack
            // 
            this.buttonBlack.Location = new System.Drawing.Point(142, 287);
            this.buttonBlack.Name = "buttonBlack";
            this.buttonBlack.Size = new System.Drawing.Size(75, 23);
            this.buttonBlack.TabIndex = 5;
            this.buttonBlack.Text = "Black";
            this.buttonBlack.UseVisualStyleBackColor = true;
            this.buttonBlack.Click += new System.EventHandler(this.buttonBlack_Click);
            // 
            // listViewFlv
            // 
            this.listViewFlv.AllowColumnReorder = true;
            this.listViewFlv.AllowDrop = true;
            this.listViewFlv.Location = new System.Drawing.Point(12, 4);
            this.listViewFlv.Name = "listViewFlv";
            this.listViewFlv.Size = new System.Drawing.Size(375, 278);
            this.listViewFlv.TabIndex = 6;
            this.listViewFlv.UseCompatibleStateImageBehavior = false;
            this.listViewFlv.View = System.Windows.Forms.View.Details;
            this.listViewFlv.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFlv_DragDrop);
            this.listViewFlv.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFlv_DragEnter);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 318);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(405, 23);
            this.progressBar.TabIndex = 7;
            // 
            // checkTop
            // 
            this.checkTop.AutoSize = true;
            this.checkTop.Location = new System.Drawing.Point(257, 291);
            this.checkTop.Name = "checkTop";
            this.checkTop.Size = new System.Drawing.Size(84, 16);
            this.checkTop.TabIndex = 9;
            this.checkTop.Text = "Always Top";
            this.checkTop.UseVisualStyleBackColor = true;
            this.checkTop.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FormAdvanced
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 342);
            this.Controls.Add(this.checkTop);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listViewFlv);
            this.Controls.Add(this.buttonBlack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.upDownRate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAdvanced";
            this.Text = "Advanced";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAdvanced_FormClosed);
            this.Load += new System.EventHandler(this.FormAdvanced_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upDownRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown upDownRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBlack;
        private System.Windows.Forms.ListView listViewFlv;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox checkTop;
    }
}