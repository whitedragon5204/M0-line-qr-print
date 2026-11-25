namespace PackLabelPrint
{
    partial class FrmPackLabel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnVoid;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnVoid = new System.Windows.Forms.Button();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLogPath = new System.Windows.Forms.Label();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.lblHistory = new System.Windows.Forms.Label();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.btnReprint = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 26);
            this.lblTitle.Text = "PACK / 客户码 纸条打印";
            //
            // lblCode
            //
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCode.Location = new System.Drawing.Point(18, 56);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(79, 20);
            this.lblCode.Text = "当前PACK";
            //
            // txtCode
            //
            this.txtCode.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtCode.Location = new System.Drawing.Point(22, 82);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(620, 29);
            this.txtCode.TabIndex = 0;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCustomer.Location = new System.Drawing.Point(18, 116);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(93, 20);
            this.lblCustomer.Text = "当前客户码";
            //
            // txtCustomer
            //
            this.txtCustomer.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtCustomer.Location = new System.Drawing.Point(22, 140);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(620, 29);
            this.txtCustomer.TabIndex = 1;
            //
            // btnFetch
            //
            this.btnFetch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnFetch.Location = new System.Drawing.Point(22, 180);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(140, 44);
            this.btnFetch.TabIndex = 1;
            this.btnFetch.Text = "领码";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            //
            // btnPrint
            //
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPrint.Location = new System.Drawing.Point(178, 180);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 44);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "打印当前";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // btnVoid
            //
            this.btnVoid.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnVoid.Location = new System.Drawing.Point(334, 180);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(140, 44);
            this.btnVoid.TabIndex = 4;
            this.btnVoid.Text = "作废当前";
            this.btnVoid.UseVisualStyleBackColor = true;
            this.btnVoid.Click += new System.EventHandler(this.btnVoid_Click);
            //
            // lblPrinter
            //
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblPrinter.Location = new System.Drawing.Point(22, 234);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(68, 17);
            this.lblPrinter.Text = "打印机：";
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(22, 258);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 19);
            this.lblStatus.Text = "未领码";
            //
            // lblLogPath
            //
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLogPath.Location = new System.Drawing.Point(22, 282);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(68, 17);
            this.lblLogPath.Text = "日志：";
            //
            // btnOpenLog
            //
            this.btnOpenLog.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOpenLog.Location = new System.Drawing.Point(490, 230);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(152, 32);
            this.btnOpenLog.TabIndex = 6;
            this.btnOpenLog.Text = "打开日志夹";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            //
            // lblHistory
            //
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblHistory.Location = new System.Drawing.Point(660, 56);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(177, 20);
            this.lblHistory.Text = "今日已领（点选后重打）";
            //
            // lstHistory
            //
            this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHistory.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.HorizontalScrollbar = true;
            this.lstHistory.ItemHeight = 14;
            this.lstHistory.Location = new System.Drawing.Point(664, 82);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(330, 228);
            this.lstHistory.TabIndex = 5;
            //
            // btnReprint
            //
            this.btnReprint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprint.Location = new System.Drawing.Point(664, 318);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(200, 40);
            this.btnReprint.TabIndex = 5;
            this.btnReprint.Text = "重打选中";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            //
            // lblLog
            //
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblLog.Location = new System.Drawing.Point(18, 320);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(37, 20);
            this.lblLog.Text = "日志";
            //
            // txtLog
            //
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(22, 364);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(972, 176);
            this.txtLog.TabIndex = 7;
            //
            // FrmPackLabel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 560);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.lblLogPath);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPrinter);
            this.Controls.Add(this.btnVoid);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnFetch);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Name = "FrmPackLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PACK / 客户码 纸条打印";
            this.Load += new System.EventHandler(this.FrmPackLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
