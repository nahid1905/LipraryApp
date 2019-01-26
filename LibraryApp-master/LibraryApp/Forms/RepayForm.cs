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
    public partial class RepayForm : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        Book userBook;
        User selectedUser;
        Order selectedOrder;

        public RepayForm()
        {
            InitializeComponent();
            FillCmb(); 
        }

        // istifadeci comboboxunu doldurmaq
        public void FillCmb()
        {
            foreach (User user in db.Users.OrderBy(u=>u.Number).ToList())
            {
                cmbUser.Items.Add(user.Number.ToString() + " " + user.Name + " " + user.Surname);
            }
        }

        //labellərə istifadəçi məlumatlarını doldurmaq
        public void FillLabel()
        {
            try
            {
                int num = Convert.ToInt32(cmbUser.SelectedItem.ToString().Substring(0, 6)); // cmbUser-deki uzvluk nomresini elde etmek
                selectedUser = db.Users.FirstOrDefault(u => u.Number == num);
                selectedOrder = db.Orders.FirstOrDefault(o => o.UserId == selectedUser.Id);

                userBook = db.Books.Find(selectedOrder.BookId);

                lblBook.Text = userBook.Name;
                lblStartDate.Text = selectedOrder.StartDate.ToString("dd/MMM/yyyy");
                lblEndDate.Text = selectedOrder.EndDate.ToString("dd/MMM/yyyy");

                //borcu tapmaq
                if (selectedOrder.EndDate >= DateTime.Now)
                {
                    lblPay.Text = "0 azn";
                 }
                else
                {
                    TimeSpan diff = DateTime.Now - selectedOrder.EndDate;
                    string borrow = (Math.Floor(diff.TotalDays)*selectedOrder.DelayPrice).ToString();
                    lblPay.Text = borrow + " azn";
                }
            }
            catch (Exception)
            {
                lblBook.Text = "Yoxdur";
                lblStartDate.Text = "";
                lblEndDate.Text = "";
                lblPay.Text = "";
            }
        }

        /* Her istifadecinin yalniz bir kitab secmek huququ oldugundan, 
        kitabi geri vererken yalniz istifadecini secmek kifayetdir ki 
        hansi kitabdan sohbet getdiyi bilinsin */
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabel();
        }

        // kitabi geri odemek
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUser.Text))
            {
                MessageBox.Show("İstifadəçini seçin");
                return;
            }
            Book book = db.Books.Find(selectedOrder.BookId);
            book.Count += 1;
            db.SaveChanges();

            Order order = db.Orders.Find(selectedOrder.Id);
            db.Orders.Remove(order);
            db.SaveChanges();

            MessageBox.Show("Kitab geri qaytarıldı və siz artıq yenidən kitab götürə bilərsiniz!");

            FillLabel();
        }
    }
}
