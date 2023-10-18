using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmAddOrEdit : Form
    {
        UnitOfWork db = new UnitOfWork();
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path+imageName);
                Customers customer = new Customers()
                {

                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    CustomerImage = imageName,

                };
                db.customersRepository.insertCustomer(customer);
                db.save();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
