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
            try { AppLog.PurgeOld(); } catch { }
            lblPrinter.Text = "打印机：" + LabelPrinter.PrinterName;
            lblLogPath.Text = "MES日志夹：" + AppLog.Dir("mes");
            LoadHistory();
            LoadScanHistory();
            ShowTabButtons();
            AppendLog("程序打开");
        }

        void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTabButtons();
            if (tabs.SelectedTab == tabScan)
            {
                txtScan.Focus();
                txtScan.SelectAll();
            }
        }

        void ShowTabButtons()
        {
            bool scan = tabs.SelectedTab == tabScan;
            btnDbConfig.Visible = scan;
            btnMesConfig.Visible = !scan;
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

            string codePart = AppLog.HistoryCode(lstHistory.SelectedItem.ToString());
            LabelPair pair;
            if (!LabelPair.TryParse(codePart, out pair) || string.IsNullOrEmpty(pair.Customer))
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
                RememberPrintHistory("mes", shown, lstHistory);
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

        void txtScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                PrintScan(false);
            }
        }

        void btnScanPrint_Click(object sender, EventArgs e)
        {
            PrintScan(false);
        }

        void btnScanReprint_Click(object sender, EventArgs e)
        {
            if (lstScanHistory.SelectedItem == null)
            {
                MessageBox.Show(this, "先在右边列表点一条，再点重打选中。", "重打", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtScan.Text = AppLog.HistoryCode(lstScanHistory.SelectedItem.ToString());
            PrintScan(true);
        }

        async void PrintScan(bool fromHistory)
        {
            string code = txtScan.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show(this, fromHistory ? "请先在右边选一条历史" : "请先扫入或手输指定码", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!fromHistory && AppLog.PrintedThisWeek("scan", code))
            {
                AppendLog("扫码拦截", code, "历史已打印");
                lblScanStatus.Text = "该二维码已打印";
                MessageBox.Show(this, "该二维码已打印，若需再次打印，请到历史中重打选中", "已打印", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (fromHistory)
            {
                DialogResult ask = MessageBox.Show(this, "重打历史码：\n" + code + "\n确认打一张？", "确认重打", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask != DialogResult.Yes)
                {
                    AppendLog("取消扫码重打", code);
                    return;
                }
            }

            btnScanPrint.Enabled = false;
            lblScanStatus.Text = "正在查库…";
            try
            {
                string lookupMsg = "";
                bool ok = await Task.Run(() => DbCodeLookup.Exists(code, out lookupMsg));
                if (!ok)
                {
                    AppendLog("扫码拦截", code, lookupMsg);
                    lblScanStatus.Text = lookupMsg;
                    MessageBox.Show(this, lookupMsg, "未打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblScanStatus.Text = "查库通过，正在连打印机…";
                LabelPrinter.PrintSpecified(code);
                RememberPrintHistory("scan", code, lstScanHistory);
                AppendLog(fromHistory ? "扫码重打成功" : "扫码打印成功", code);
                lblScanStatus.Text = fromHistory ? "重打成功" : "打印成功";
                txtScan.Clear();
                txtScan.Focus();
            }
            catch (Exception ex)
            {
                AppendLog(fromHistory ? "扫码重打失败" : "扫码打印失败", code, ex.Message);
                lblScanStatus.Text = "打印失败，可再打";
                MessageBox.Show(this, "打印失败。\n" + ex.Message, "打印失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnScanPrint.Enabled = true;
            }
        }

        void btnDbConfig_Click(object sender, EventArgs e)
        {
            using (FrmDbConfig dlg = new FrmDbConfig())
            {
                dlg.ShowDialog(this);
            }
        }

        void btnMesConfig_Click(object sender, EventArgs e)
        {
            using (FrmMesConfig dlg = new FrmMesConfig())
            {
                dlg.ShowDialog(this);
            }
        }

        void btnOpenLog_Click(object sender, EventArgs e)
        {
            OpenLogDir("mes");
        }

        void btnScanOpenLog_Click(object sender, EventArgs e)
        {
            OpenLogDir("scan");
        }

        void OpenLogDir(string kind)
        {
            string dir = AppLog.Dir(kind);
            try
            {
                Directory.CreateDirectory(dir);
                Process.Start(dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "打不开日志夹：\n" + dir + "\n" + ex.Message, "日志", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RememberPrintHistory(string kind, string text, ListBox list)
        {
            try
            {
                AppLog.RememberPrint(kind, text);
            }
            catch
            {
            }
            FillHistory(list, kind);
            if (list.Items.Count > 0)
            {
                list.SelectedIndex = 0;
            }
        }

        void LoadHistory()
        {
            FillHistory(lstHistory, "mes");
        }

        void LoadScanHistory()
        {
            FillHistory(lstScanHistory, "scan");
        }

        void FillHistory(ListBox list, string kind)
        {
            list.Items.Clear();
            try
            {
                foreach (string line in AppLog.LoadWeekHistory(kind))
                {
                    list.Items.Add(line);
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
                AppLog.Write(tabs.SelectedTab == tabScan ? "scan" : "mes", line);
            }
            catch
            {
            }
        }
    }
}