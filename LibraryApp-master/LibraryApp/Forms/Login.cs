﻿using System;
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
    public partial class Login : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        public Admin admin = new Admin();

        public Login()
        {
            InitializeComponent();
        }

        // LOGIN 
        public void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Emailiniz və ya Parolunuz boş ola bilməz");
                return;
            }
            admin = db.Admins.FirstOrDefault(a => a.Email == txtEmail.Text && a.Password == txtPassword.Text);
            if (admin == null)
            {
                MessageBox.Show("Emailiniz və ya Parolunuz səhvdir");
                return;
            }
            else
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }

        }

        // REGISTER
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }
    }
}
