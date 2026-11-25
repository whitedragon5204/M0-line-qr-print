using System;
using System.Windows.Forms;

namespace PackLabelPrint
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args != null && args.Length > 0 &&
                (string.Equals(args[0], "--apply-printer", StringComparison.OrdinalIgnoreCase)
                 || string.Equals(args[0], "/apply-printer", StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    MessageBox.Show(PrinterConfig.Apply(), "写入打印机配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "写入打印机配置失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            Application.Run(new FrmPackLabel());
        }
    }
}
