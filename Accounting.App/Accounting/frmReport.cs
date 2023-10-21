using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.Utility.Convertor;
using Accounting.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.App
{
    public partial class frmReport : Form
    {
        public int typeId = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomersViewModel> list = new List<ListCustomersViewModel>();
                list.Add(new ListCustomersViewModel()
                {
                    customerId = 0,
                    fullName = "انتخاب کنید",
                });
                list.AddRange(db.customersRepository.getNameCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "CustomerId";

            }
            if (typeId == 1)
            {
                this.Text = "گزارش دریافتی‌ها";
            }
            else
            {
                this.Text = "گزارش پرداختی‌ها";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;

                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerId = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.accountingRepository.get(a => a.AccountingCategoryId == typeId && a.CustomerId == customerId));
                }
                else
                {
                    result.AddRange(db.accountingRepository.get(a => a.AccountingCategoryId == typeId));
                }

                if (txtFromDate.Text != "    /  /")
                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(r => r.DateTime >= startDate.Value).ToList();
                }
                if (txtToDate.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtFromDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(r => r.DateTime <= endDate.Value).ToList();

                }


                dgReport.Rows.Clear();
                foreach (var accounting in result)
                {
                    string customerName = db.customersRepository.getCustomerNameById(accounting.CustomerId);
                    dgReport.Rows.Add(accounting.AccountingId, customerName, accounting.Amount, accounting.DateTime.toShamsi(), accounting.Description);
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("آیا از حذف مطمئن هستید ؟", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {

                        db.accountingRepository.delete(id);
                        db.save();
                        Filter();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewTransaction frmNewTransaction = new frmNewTransaction();
                frmNewTransaction.accountingId = id;
                if (frmNewTransaction.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
