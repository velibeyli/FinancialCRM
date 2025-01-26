using FinancialCRM.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCRM
{
    public partial class FrmBilling : Form
    {
        public FrmBilling()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        private void FrmBilling_Load(object sender, EventArgs e)
        {
            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnBillList_Click(object sender, EventArgs e)
        {
            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string period = txtBillPeriod.Text;

            Bills bills = new Bills();
            bills.BillTitle = title;
            bills.BillAmount = amount;
            bills.BankPeriod = period;

            db.Bills.Add(bills);
            db.SaveChanges();
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sisteme Eklendi.","Ödemeler &" +
                " Faturalar",MessageBoxButtons.OK,MessageBoxIcon.Information);

            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;

        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBillid.Text);
            var removeValue = db.Bills.Find(id);
            db.Bills.Remove(removeValue);
            db.SaveChanges();

            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sistemden Silindi.", "Ödemeler &" +
                " Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBillid.Text);
            var updatedValue = db.Bills.Find(id);

            updatedValue.BillTitle = txtBillTitle.Text;
            updatedValue.BillAmount = decimal.Parse(txtBillAmount.Text);
            updatedValue.BankPeriod = txtBillPeriod.Text;

            db.SaveChanges();
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Sistemde Güncellendi.", "Ödemeler &" +
                " Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Close();
        }
    }
}
