using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewTransaction : Form
    {
        UnitOfWork db = new UnitOfWork();
        public int accountingId = 0;
        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.customersRepository.getNameCustomers(txtFilter.Text);
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.customersRepository.getNameCustomers();
            if (accountingId != 0)
            {
                var account = db.accountingRepository.getById(accountingId);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description.ToString();
                txtName.Text = db.customersRepository.getCustomerNameById(account.CustomerId);
                if (account.AccountingCategoryId == 1)
                {
                    rbRecieve.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbRecieve.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {

                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CustomerId = db.customersRepository.getCustomerIdByName(txtName.Text),
                        AccountingCategoryId = (rbRecieve.Checked) ? 1 : 2,
                        DateTime = DateTime.Now,
                        Description = txtDescription.Text,
                    };
                    if (accountingId == 0)
                    {
                        db.accountingRepository.insert(accounting);
                        db.save();
                    }
                    else
                    {
                        using (UnitOfWork db2 = new UnitOfWork())
                        {

                            accounting.AccountingId = accountingId;
                            db2.accountingRepository.update(accounting);
                            db2.save();
                        }

                    }

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    RtlMessageBox.Show("لطفاً نوع تراکنش را وارد کنید");
                }
            }
        }
    }
}
