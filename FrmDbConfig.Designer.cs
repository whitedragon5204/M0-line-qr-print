namespace PackLabelPrint
{
    partial class FrmDbConfig
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblDbHost;
        private System.Windows.Forms.TextBox txtDbHost;
        private System.Windows.Forms.Label lblDbPort;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label lblDbUser;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.Label lblDbPassword;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label lblDbTable;
        private System.Windows.Forms.TextBox txtDbTable;
        private System.Windows.Forms.Label lblDbField;
        private System.Windows.Forms.TextBox txtDbField;
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
            this.lblDbHost = new System.Windows.Forms.Label();
            this.txtDbHost = new System.Windows.Forms.TextBox();
            this.lblDbPort = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.lblDbUser = new System.Windows.Forms.Label();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.lblDbPassword = new System.Windows.Forms.Label();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.lblDbTable = new System.Windows.Forms.Label();
            this.txtDbTable = new System.Windows.Forms.TextBox();
            this.lblDbField = new System.Windows.Forms.Label();
            this.txtDbField = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblDbHost.AutoSize = true;
            this.lblDbHost.Location = new System.Drawing.Point(24, 24);
            this.lblDbHost.Name = "lblDbHost";
            this.lblDbHost.Size = new System.Drawing.Size(41, 12);
            this.lblDbHost.Text = "主机";
            this.txtDbHost.Location = new System.Drawing.Point(90, 20);
            this.txtDbHost.Name = "txtDbHost";
            this.txtDbHost.Size = new System.Drawing.Size(200, 21);
            this.txtDbHost.TabIndex = 0;
            this.lblDbPort.AutoSize = true;
            this.lblDbPort.Location = new System.Drawing.Point(310, 24);
            this.lblDbPort.Name = "lblDbPort";
            this.lblDbPort.Size = new System.Drawing.Size(41, 12);
            this.lblDbPort.Text = "端口";
            this.txtDbPort.Location = new System.Drawing.Point(360, 20);
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Size = new System.Drawing.Size(80, 21);
            this.txtDbPort.TabIndex = 1;
            this.lblDbUser.AutoSize = true;
            this.lblDbUser.Location = new System.Drawing.Point(24, 60);
            this.lblDbUser.Name = "lblDbUser";
            this.lblDbUser.Size = new System.Drawing.Size(41, 12);
            this.lblDbUser.Text = "用户名";
            this.txtDbUser.Location = new System.Drawing.Point(90, 56);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(200, 21);
            this.txtDbUser.TabIndex = 2;
            this.lblDbPassword.AutoSize = true;
            this.lblDbPassword.Location = new System.Drawing.Point(310, 60);
            this.lblDbPassword.Name = "lblDbPassword";
            this.lblDbPassword.Size = new System.Drawing.Size(41, 12);
            this.lblDbPassword.Text = "密码";
            this.txtDbPassword.Location = new System.Drawing.Point(360, 56);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(160, 21);
            this.txtDbPassword.TabIndex = 3;
            this.txtDbPassword.UseSystemPasswordChar = true;
            this.lblDbName.AutoSize = true;
            this.lblDbName.Location = new System.Drawing.Point(24, 96);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(41, 12);
            this.lblDbName.Text = "库名";
            this.txtDbName.Location = new System.Drawing.Point(90, 92);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(200, 21);
            this.txtDbName.TabIndex = 4;
            this.lblDbTable.AutoSize = true;
            this.lblDbTable.Location = new System.Drawing.Point(24, 132);
            this.lblDbTable.Name = "lblDbTable";
            this.lblDbTable.Size = new System.Drawing.Size(41, 12);
            this.lblDbTable.Text = "表名";
            this.txtDbTable.Location = new System.Drawing.Point(90, 128);
            this.txtDbTable.Name = "txtDbTable";
            this.txtDbTable.Size = new System.Drawing.Size(200, 21);
            this.txtDbTable.TabIndex = 5;
            this.lblDbField.AutoSize = true;
            this.lblDbField.Location = new System.Drawing.Point(24, 168);
            this.lblDbField.Name = "lblDbField";
            this.lblDbField.Size = new System.Drawing.Size(41, 12);
            this.lblDbField.Text = "字段名";
            this.txtDbField.Location = new System.Drawing.Point(90, 164);
            this.txtDbField.Name = "txtDbField";
            this.txtDbField.Size = new System.Drawing.Size(200, 21);
            this.txtDbField.TabIndex = 6;
            this.btnSave.Location = new System.Drawing.Point(90, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnTest.Location = new System.Drawing.Point(214, 210);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(110, 32);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            this.btnClose.Location = new System.Drawing.Point(338, 210);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 32);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 268);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDbField);
            this.Controls.Add(this.lblDbField);
            this.Controls.Add(this.txtDbTable);
            this.Controls.Add(this.lblDbTable);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.lblDbName);
            this.Controls.Add(this.txtDbPassword);
            this.Controls.Add(this.lblDbPassword);
            this.Controls.Add(this.txtDbUser);
            this.Controls.Add(this.lblDbUser);
            this.Controls.Add(this.txtDbPort);
            this.Controls.Add(this.lblDbPort);
            this.Controls.Add(this.txtDbHost);
            this.Controls.Add(this.lblDbHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDbConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库配置";
            this.Load += new System.EventHandler(this.FrmDbConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}