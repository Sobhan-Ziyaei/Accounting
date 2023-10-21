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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            frmNewTransaction frmNewTransaction = new frmNewTransaction();
            frmNewTransaction.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.typeId = 2;
            frmReport.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.typeId = 1;
            frmReport.ShowDialog();
        }
    }
}
