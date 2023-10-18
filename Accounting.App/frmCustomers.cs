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

namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            dgCustomers.AutoGenerateColumns = false;
            BindGrid();
        }

        void BindGrid()
        {

            using (UnitOfWork db = new UnitOfWork())
            {
                dgCustomers.DataSource = db.customersRepository.getAllCustomers();
            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilterCustomer.Text = "";
            BindGrid();
        }

        private void txtFilterCustomer_Click(object sender, EventArgs e)
        {

        }

        private void txtFilterCustomer_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgCustomers.DataSource = db.customersRepository.getCustomersByFilter(txtFilterCustomer.Text);

            }
        }
    }
}
