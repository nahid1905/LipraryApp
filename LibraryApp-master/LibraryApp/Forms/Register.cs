using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApp.Models;

namespace LibraryApp.Forms
{
    public partial class Register : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Formanı tam doldurun");
                return;
            }
            else if (!txtEmail.Text.Contains('@'))
            {
                MessageBox.Show("Yalnış email adresi.");
            }
            else
            {
                Admin admin = new Admin()
                {
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text
                };

                db.Admins.Add(admin);
                db.SaveChanges();

                MessageBox.Show("Qeydiyyatdan keçdiniz");

                this.Close();
            }
        }
    }
}
