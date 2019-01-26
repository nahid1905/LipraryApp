using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp.Forms
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            BookForm book = new BookForm();
            book.ShowDialog();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm order = new OrderForm();
            order.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            RepayForm backForm = new RepayForm();
            backForm.ShowDialog();
        }
    }
}
