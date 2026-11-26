using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackLabelPrint
{
    public partial class FrmMesConfig : Form
    {
        public FrmMesConfig()
        {
            InitializeComponent();
        }

        void FrmMesConfig_Load(object sender, EventArgs e)
        {
            txtGenerateUrl.Text = MesCodeClient.GenerateUrl;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MesCodeClient.SaveGenerateUrl(txtGenerateUrl.Text.Trim());
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
                MesCodeClient.SaveGenerateUrl(txtGenerateUrl.Text.Trim());
                string url = txtGenerateUrl.Text.Trim();
                string msg = await Task.Run(() => MesCodeClient.TestUrl(url));
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
