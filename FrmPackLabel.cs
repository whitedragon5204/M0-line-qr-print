using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackLabelPrint
{
    public partial class FrmPackLabel : Form
    {
        bool _printedOk;

        public FrmPackLabel()
        {
            InitializeComponent();
        }

        void FrmPackLabel_Load(object sender, EventArgs e)
        {
            lblPrinter.Text = "打印机：" + LabelPrinter.PrinterName;
            lblLogPath.Text = "日志夹：" + AppLog.LogDir;
            LoadHistory();
            AppendLog("程序打开");
        }

        string CurrentPack
        {
            get { return txtCode.Text.Trim(); }
        }

        string CurrentCustomer
        {
            get { return txtCustomer.Text.Trim(); }
        }

        string CurrentDisplay
        {
            get
            {
                LabelPair pair = new LabelPair();
                pair.Pack = CurrentPack;
                pair.Customer = CurrentCustomer;
                return pair.Display;
            }
        }

        async void btnFetch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentPack) || !string.IsNullOrEmpty(CurrentCustomer))
            {
                string tip = _printedOk
                    ? "再领一对新码？当前这对已经打过（MES 收不回）。"
                    : "上一对还没打，再领会丢掉屏幕上的码（MES 收不回）。确认再领？";
                DialogResult ask = MessageBox.Show(this, tip, "确认领码", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask != DialogResult.Yes)
                {
                    AppendLog("取消领码", CurrentDisplay);
                    return;
                }
            }

            btnFetch.Enabled = false;
            lblStatus.Text = "正在领 PACK 和客户码…";
            try
            {
                PairFetchResult fetched = await Task.Run(() => MesCodeClient.FetchPair());
                if (fetched.Pack != null && fetched.Pack.Ok)
                {
                    txtCode.Text = fetched.Pack.Code;
                }
                if (fetched.Customer != null && fetched.Customer.Ok)
                {
                    txtCustomer.Text = fetched.Customer.Code;
                }
                if (!fetched.Ok)
                {
                    AppendLog("领码失败", CurrentDisplay, fetched.Message);
                    lblStatus.Text = "取码失败，未打印";
                    MessageBox.Show(this, "取码失败，未打印。\n" + fetched.Message, "领码失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _printedOk = false;
                RememberHistory(CurrentDisplay);
                AppendLog("领码成功", CurrentDisplay);
                lblStatus.Text = "已领一对，未打印";
            }
            catch (Exception ex)
            {
                AppendLog("领码异常", CurrentDisplay, ex.Message);
                lblStatus.Text = "取码失败，未打印";
                MessageBox.Show(this, "取码失败，未打印。\n" + ex.Message, "领码失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFetch.Enabled = true;
            }
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOne(CurrentPack, CurrentCustomer, false);
        }

        void btnReprint_Click(object sender, EventArgs e)
        {
            if (lstHistory.SelectedItem == null)
            {
                MessageBox.Show(this, "先在右边列表点一对码，再点重打选中。\n不会重新向 MES 领号。", "重打", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LabelPair pair;
            if (!LabelPair.TryParse(lstHistory.SelectedItem.ToString(), out pair) || string.IsNullOrEmpty(pair.Customer))
            {
                MessageBox.Show(this, "这条历史只有 PACK，没有客户码，不能按新版重打。请重新领一对。", "重打", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PrintOne(pair.Pack, pair.Customer, true);
        }

        void PrintOne(string pack, string customer, bool fromHistory)
        {
            pack = (pack ?? "").Trim();
            customer = (customer ?? "").Trim();
            if (string.IsNullOrEmpty(pack) || string.IsNullOrEmpty(customer))
            {
                MessageBox.Show(this, fromHistory ? "请先在右边选一对码" : "请先领码（PACK 和客户码都要有）", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AppendLog("打印拦截", "", fromHistory ? "未选历史码" : "请先领齐一对");
                return;
            }

            string shown = pack + LabelPair.Sep + customer;
            string askText = fromHistory
                ? "重打历史码，不再向 MES 领新号。\n" + shown + "\n确认打一张？"
                : (_printedOk ? "这对已经打过，确认再打一张？" : null);

            if (!string.IsNullOrEmpty(askText))
            {
                DialogResult ask = MessageBox.Show(this, askText, fromHistory ? "确认重打" : "重复打印", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask != DialogResult.Yes)
                {
                    AppendLog(fromHistory ? "取消重打历史" : "取消重复打印", shown);
                    return;
                }
                AppendLog(fromHistory ? "确认重打历史" : "确认重复打印", shown);
            }

            try
            {
                LabelPrinter.Print(pack, customer);
                if (!fromHistory)
                {
                    _printedOk = true;
                }
                AppendLog(fromHistory ? "历史重打成功" : "打印成功", shown);
                lblStatus.Text = fromHistory ? "历史重打成功" : "打印成功";
            }
            catch (Exception ex)
            {
                AppendLog(fromHistory ? "历史重打失败" : "打印失败", shown, ex.Message);
                lblStatus.Text = "打印失败，码还在，可再打";
                MessageBox.Show(this, "打印失败，码还在，可再打。\n" + ex.Message, "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void btnVoid_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPack) && string.IsNullOrEmpty(CurrentCustomer))
            {
                MessageBox.Show(this, "没有可作废的码", "作废", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string shown = CurrentDisplay;
            DialogResult ask = MessageBox.Show(this,
                "本窗作废屏幕上这对码，不再打印。\nMES 号已发出，无法收回。\n" + shown,
                "确认作废",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (ask != DialogResult.Yes)
            {
                AppendLog("取消作废", shown);
                return;
            }

            txtCode.Clear();
            txtCustomer.Clear();
            _printedOk = false;
            AppendLog("本窗作废，MES 号已发出无法收回", shown);
            lblStatus.Text = "已作废（MES 收不回）";
        }

        void btnOpenLog_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(AppLog.LogDir);
                Process.Start(AppLog.LogDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "打不开日志夹：\n" + AppLog.LogDir + "\n" + ex.Message, "日志", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RememberHistory(string code)
        {
            try
            {
                AppLog.RememberCode(code);
            }
            catch
            {
            }
            lstHistory.Items.Remove(code);
            lstHistory.Items.Insert(0, code);
            lstHistory.SelectedIndex = 0;
        }

        void LoadHistory()
        {
            lstHistory.Items.Clear();
            try
            {
                foreach (string code in AppLog.LoadTodayCodes())
                {
                    lstHistory.Items.Add(code);
                }
            }
            catch
            {
            }
        }

        void AppendLog(string action, string code = "", string detail = "")
        {
            string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                + "  " + action
                + (string.IsNullOrEmpty(code) ? "" : "  " + code)
                + (string.IsNullOrEmpty(detail) ? "" : "  " + detail);

            if (txtLog.Text.Length > 0)
            {
                txtLog.AppendText(Environment.NewLine);
            }
            txtLog.AppendText(line);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();

            try
            {
                AppLog.Write(line);
            }
            catch
            {
            }
        }
    }
}
