namespace PackLabelPrint
{
    partial class FrmPackLabel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabMes;
        private System.Windows.Forms.TabPage tabScan;
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
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.Button btnScanPrint;
        private System.Windows.Forms.Button btnDbConfig;
        private System.Windows.Forms.Button btnMesConfig;
        private System.Windows.Forms.Label lblScanStatus;
        private System.Windows.Forms.Label lblScanHistory;
        private System.Windows.Forms.ListBox lstScanHistory;
        private System.Windows.Forms.Button btnScanReprint;
        private System.Windows.Forms.Button btnScanOpenLog;
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabMes = new System.Windows.Forms.TabPage();
            this.tabScan = new System.Windows.Forms.TabPage();
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
            this.lblScan = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.btnScanPrint = new System.Windows.Forms.Button();
            this.btnDbConfig = new System.Windows.Forms.Button();
            this.btnMesConfig = new System.Windows.Forms.Button();
            this.lblScanStatus = new System.Windows.Forms.Label();
            this.lblScanHistory = new System.Windows.Forms.Label();
            this.lstScanHistory = new System.Windows.Forms.ListBox();
            this.btnScanReprint = new System.Windows.Forms.Button();
            this.btnScanOpenLog = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tabMes.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 26);
            this.lblTitle.Text = "二维码打印";
            //
            // btnDbConfig
            //
            this.btnDbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDbConfig.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDbConfig.Location = new System.Drawing.Point(908, 8);
            this.btnDbConfig.Name = "btnDbConfig";
            this.btnDbConfig.Size = new System.Drawing.Size(180, 30);
            this.btnDbConfig.TabIndex = 9;
            this.btnDbConfig.Text = "数据库配置";
            this.btnDbConfig.UseVisualStyleBackColor = true;
            this.btnDbConfig.Visible = false;
            this.btnDbConfig.Click += new System.EventHandler(this.btnDbConfig_Click);
            //
            // btnMesConfig
            //
            this.btnMesConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMesConfig.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnMesConfig.Location = new System.Drawing.Point(908, 8);
            this.btnMesConfig.Name = "btnMesConfig";
            this.btnMesConfig.Size = new System.Drawing.Size(180, 30);
            this.btnMesConfig.TabIndex = 10;
            this.btnMesConfig.Text = "MES接口配置";
            this.btnMesConfig.UseVisualStyleBackColor = true;
            this.btnMesConfig.Click += new System.EventHandler(this.btnMesConfig_Click);
            //
            // tabs
            //
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabMes);
            this.tabs.Controls.Add(this.tabScan);
            this.tabs.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabs.Location = new System.Drawing.Point(12, 42);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1076, 360);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            //
            // tabMes
            //
            this.tabMes.Controls.Add(this.btnReprint);
            this.tabMes.Controls.Add(this.lstHistory);
            this.tabMes.Controls.Add(this.lblHistory);
            this.tabMes.Controls.Add(this.btnOpenLog);
            this.tabMes.Controls.Add(this.lblLogPath);
            this.tabMes.Controls.Add(this.lblStatus);
            this.tabMes.Controls.Add(this.lblPrinter);
            this.tabMes.Controls.Add(this.btnVoid);
            this.tabMes.Controls.Add(this.btnPrint);
            this.tabMes.Controls.Add(this.btnFetch);
            this.tabMes.Controls.Add(this.txtCustomer);
            this.tabMes.Controls.Add(this.lblCustomer);
            this.tabMes.Controls.Add(this.txtCode);
            this.tabMes.Controls.Add(this.lblCode);
            this.tabMes.Location = new System.Drawing.Point(4, 29);
            this.tabMes.Name = "tabMes";
            this.tabMes.Padding = new System.Windows.Forms.Padding(3);
            this.tabMes.Size = new System.Drawing.Size(1068, 327);
            this.tabMes.TabIndex = 0;
            this.tabMes.Text = "MES领码打印";
            this.tabMes.UseVisualStyleBackColor = true;
            //
            // tabScan
            //
            this.tabScan.Controls.Add(this.btnScanOpenLog);
            this.tabScan.Controls.Add(this.btnScanReprint);
            this.tabScan.Controls.Add(this.lstScanHistory);
            this.tabScan.Controls.Add(this.lblScanHistory);
            this.tabScan.Controls.Add(this.lblScanStatus);
            this.tabScan.Controls.Add(this.btnScanPrint);
            this.tabScan.Controls.Add(this.txtScan);
            this.tabScan.Controls.Add(this.lblScan);
            this.tabScan.Location = new System.Drawing.Point(4, 29);
            this.tabScan.Name = "tabScan";
            this.tabScan.Padding = new System.Windows.Forms.Padding(3);
            this.tabScan.Size = new System.Drawing.Size(1068, 327);
            this.tabScan.TabIndex = 1;
            this.tabScan.Text = "扫码/录入打印";
            this.tabScan.UseVisualStyleBackColor = true;
            //
            // lblCode
            //
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCode.Location = new System.Drawing.Point(12, 12);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(79, 20);
            this.lblCode.Text = "当前PACK";
            //
            // txtCode
            //
            this.txtCode.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtCode.Location = new System.Drawing.Point(16, 36);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(502, 29);
            this.txtCode.TabIndex = 0;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCustomer.Location = new System.Drawing.Point(12, 72);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(93, 20);
            this.lblCustomer.Text = "当前客户码";
            //
            // txtCustomer
            //
            this.txtCustomer.Font = new System.Drawing.Font("Consolas", 14F);
            this.txtCustomer.Location = new System.Drawing.Point(16, 96);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(502, 29);
            this.txtCustomer.TabIndex = 1;
            //
            // btnFetch
            //
            this.btnFetch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnFetch.Location = new System.Drawing.Point(16, 136);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(140, 44);
            this.btnFetch.TabIndex = 2;
            this.btnFetch.Text = "领码";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            //
            // btnPrint
            //
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPrint.Location = new System.Drawing.Point(172, 136);
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
            this.btnVoid.Location = new System.Drawing.Point(328, 136);
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
            this.lblPrinter.Location = new System.Drawing.Point(16, 190);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(68, 17);
            this.lblPrinter.Text = "打印机：";
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(16, 214);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 19);
            this.lblStatus.Text = "未领码";
            //
            // lblLogPath
            //
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLogPath.Location = new System.Drawing.Point(16, 238);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(68, 17);
            this.lblLogPath.Text = "日志：";
            //
            // btnOpenLog
            //
            this.btnOpenLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOpenLog.Location = new System.Drawing.Point(892, 272);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(160, 40);
            this.btnOpenLog.TabIndex = 5;
            this.btnOpenLog.Text = "打开日志夹";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            //
            // lblHistory
            //
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblHistory.Location = new System.Drawing.Point(534, 12);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(205, 20);
            this.lblHistory.Text = "领码历史（一周，点选后重打）";
            //
            // lstHistory
            //
            this.lstHistory.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.HorizontalScrollbar = true;
            this.lstHistory.ItemHeight = 14;
            this.lstHistory.Location = new System.Drawing.Point(534, 36);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(518, 228);
            this.lstHistory.TabIndex = 6;
            //
            // btnReprint
            //
            this.btnReprint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnReprint.Location = new System.Drawing.Point(534, 272);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(200, 40);
            this.btnReprint.TabIndex = 7;
            this.btnReprint.Text = "重打选中";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            //
            // lblScan
            //
            this.lblScan.AutoSize = true;
            this.lblScan.Location = new System.Drawing.Point(12, 12);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(233, 20);
            this.lblScan.Text = "扫入或手输指定码（回车打印）";
            //
            // txtScan
            //
            this.txtScan.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtScan.Location = new System.Drawing.Point(16, 36);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(394, 32);
            this.txtScan.TabIndex = 0;
            this.txtScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScan_KeyDown);
            //
            // btnScanPrint
            //
            this.btnScanPrint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnScanPrint.Location = new System.Drawing.Point(418, 32);
            this.btnScanPrint.Name = "btnScanPrint";
            this.btnScanPrint.Size = new System.Drawing.Size(100, 40);
            this.btnScanPrint.TabIndex = 1;
            this.btnScanPrint.Text = "打印";
            this.btnScanPrint.UseVisualStyleBackColor = true;
            this.btnScanPrint.Click += new System.EventHandler(this.btnScanPrint_Click);
            //
            // lblScanStatus
            //
            this.lblScanStatus.AutoSize = true;
            this.lblScanStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblScanStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblScanStatus.Location = new System.Drawing.Point(16, 88);
            this.lblScanStatus.Name = "lblScanStatus";
            this.lblScanStatus.Size = new System.Drawing.Size(121, 19);
            this.lblScanStatus.Text = "未扫码 / 未打印";
            //
            // lblScanHistory
            //
            this.lblScanHistory.AutoSize = true;
            this.lblScanHistory.Location = new System.Drawing.Point(534, 12);
            this.lblScanHistory.Name = "lblScanHistory";
            this.lblScanHistory.Size = new System.Drawing.Size(205, 20);
            this.lblScanHistory.Text = "打印历史（一周，点选后重打）";
            //
            // lstScanHistory
            //
            this.lstScanHistory.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstScanHistory.FormattingEnabled = true;
            this.lstScanHistory.HorizontalScrollbar = true;
            this.lstScanHistory.ItemHeight = 14;
            this.lstScanHistory.Location = new System.Drawing.Point(534, 36);
            this.lstScanHistory.Name = "lstScanHistory";
            this.lstScanHistory.Size = new System.Drawing.Size(518, 228);
            this.lstScanHistory.TabIndex = 3;
            //
            // btnScanReprint
            //
            this.btnScanReprint.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnScanReprint.Location = new System.Drawing.Point(534, 272);
            this.btnScanReprint.Name = "btnScanReprint";
            this.btnScanReprint.Size = new System.Drawing.Size(200, 40);
            this.btnScanReprint.TabIndex = 4;
            this.btnScanReprint.Text = "重打选中";
            this.btnScanReprint.UseVisualStyleBackColor = true;
            this.btnScanReprint.Click += new System.EventHandler(this.btnScanReprint_Click);
            //
            // btnScanOpenLog
            //
            this.btnScanOpenLog.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnScanOpenLog.Location = new System.Drawing.Point(892, 272);
            this.btnScanOpenLog.Name = "btnScanOpenLog";
            this.btnScanOpenLog.Size = new System.Drawing.Size(160, 40);
            this.btnScanOpenLog.TabIndex = 5;
            this.btnScanOpenLog.Text = "打开日志夹";
            this.btnScanOpenLog.UseVisualStyleBackColor = true;
            this.btnScanOpenLog.Click += new System.EventHandler(this.btnScanOpenLog_Click);
            //
            // lblLog
            //
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblLog.Location = new System.Drawing.Point(18, 410);
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
            this.txtLog.Location = new System.Drawing.Point(22, 434);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1056, 146);
            this.txtLog.TabIndex = 8;
            //
            // FrmPackLabel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnDbConfig);
            this.Controls.Add(this.btnMesConfig);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "FrmPackLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "二维码打印";
            this.Load += new System.EventHandler(this.FrmPackLabel_Load);
            this.tabs.ResumeLayout(false);
            this.tabMes.ResumeLayout(false);
            this.tabMes.PerformLayout();
            this.tabScan.ResumeLayout(false);
            this.tabScan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}