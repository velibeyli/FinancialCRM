using FinancialCRM.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCRM
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        int count = 0;
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            var totalBalance = db.Banks.Sum(x=>x.BankBalance);
            lblTotalBalance.Text = totalBalance.ToString() + " tl";

            var lastBankProcessAmount = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
                .Take(1).Select(y => y.Amount).FirstOrDefault();
            lblLastBankProcessAmount.Text = lastBankProcessAmount.ToString() + " tl";

            // Chart 1 kodları
            var bankData = db.Banks.Select(x => new
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();

            chart1.Series.Clear();
            var series = chart1.Series.Add("Series1");
            foreach (var item in bankData)
            {
                series.Points.AddXY(item.BankTitle, item.BankBalance);
            }

            // Chart 2 kodları

            var billData = db.Bills.Select(x => new
            {
                x.BillTitle,
                x.BillAmount
            }).ToList();

            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            foreach (var item in billData)
            {
                series2.Points.AddXY(item.BillTitle, item.BillAmount);
            }
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if(count % 4 == 1)
            {
                var electricalBill = db.Bills.Where(x => x.BillTitle == "Elektrik Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektrik Faturası";
                lblBillAmount.Text = electricalBill.ToString() + " tl";
            }

            if (count % 4 == 2)
            {
                var gasBill = db.Bills.Where(x => x.BillTitle == "Doğalgaz Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz Faturası";
                lblBillAmount.Text = gasBill.ToString() + " tl";
            }

            if (count % 4 == 3)
            {
                var waterBill = db.Bills.Where(x => x.BillTitle == "Su Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su Faturası";
                lblBillAmount.Text = waterBill.ToString() + " tl";
            }

            if (count % 4 == 0)
            {
                var internetBill = db.Bills.Where(x => x.BillTitle == "İnternet Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "İnternet Faturası";
                lblBillAmount.Text = internetBill.ToString() + " tl";
            }
        }
    }
}
