using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackLabelPrint
{
    public partial class FrmDbConfig : Form
    {
        public FrmDbConfig()
        {
            InitializeComponent();
        }

        void FrmDbConfig_Load(object sender, EventArgs e)
        {
            txtDbHost.Text = DbCodeLookup.Cfg("DbHost");
            txtDbPort.Text = DbCodeLookup.Cfg("DbPort");
            txtDbUser.Text = DbCodeLookup.Cfg("DbUser");
            txtDbPassword.Text = DbCodeLookup.Cfg("DbPassword");
            txtDbName.Text = DbCodeLookup.Cfg("DbName");
            txtDbTable.Text = DbCodeLookup.Cfg("DbTable");
            txtDbField.Text = DbCodeLookup.Cfg("DbField");
        }

        void SaveFields()
        {
            DbCodeLookup.Save(
                txtDbHost.Text.Trim(),
                txtDbPort.Text.Trim(),
                txtDbUser.Text.Trim(),
                txtDbPassword.Text,
                txtDbName.Text.Trim(),
                txtDbTable.Text.Trim(),
                txtDbField.Text.Trim());
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFields();
                MessageBox.Show(this, "已写入 PackLabelPrint.exe.config", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "保存失败。\n" + ex.Message, "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async void btnTest_Click(object sender, EventArgs e)
        {
            btnTest.Enabled = false;
            try
            {
                SaveFields();
                string msg = await Task.Run(() => DbCodeLookup.Test(
                    txtDbHost.Text.Trim(),
                    txtDbPort.Text.Trim(),
                    txtDbUser.Text.Trim(),
                    txtDbPassword.Text,
                    txtDbName.Text.Trim(),
                    txtDbTable.Text.Trim(),
                    txtDbField.Text.Trim()));
                MessageBox.Show(this, msg, "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "连接失败。\n" + ex.Message, "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTest.Enabled = true;
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}