namespace PackLabelPrint
{
    partial class FrmMesConfig
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblGenerateUrl;
        private System.Windows.Forms.TextBox txtGenerateUrl;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnClose;

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
            this.lblGenerateUrl = new System.Windows.Forms.Label();
            this.txtGenerateUrl = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblGenerateUrl.AutoSize = true;
            this.lblGenerateUrl.Location = new System.Drawing.Point(24, 24);
            this.lblGenerateUrl.Name = "lblGenerateUrl";
            this.lblGenerateUrl.Size = new System.Drawing.Size(65, 20);
            this.lblGenerateUrl.Text = "领码地址";
            this.txtGenerateUrl.Location = new System.Drawing.Point(28, 50);
            this.txtGenerateUrl.Name = "txtGenerateUrl";
            this.txtGenerateUrl.Size = new System.Drawing.Size(640, 25);
            this.txtGenerateUrl.TabIndex = 0;
            this.lblHint.Location = new System.Drawing.Point(28, 86);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(640, 48);
            this.lblHint.Text = "和刻码同一套。一次领一对：PACK type=1，客户码 type=2。\r\n测试连接只探能不能通，不会向 MES 领码。";
            this.btnSave.Location = new System.Drawing.Point(28, 148);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnTest.Location = new System.Drawing.Point(152, 148);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(110, 32);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            this.btnClose.Location = new System.Drawing.Point(276, 148);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 208);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.txtGenerateUrl);
            this.Controls.Add(this.lblGenerateUrl);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMesConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MES接口配置";
            this.Load += new System.EventHandler(this.FrmMesConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
