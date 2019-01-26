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
    public partial class OrderForm : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        User selectedUser;
        Book selectedBook;
        Order SelectedOrder;

        public OrderForm()
        {
            InitializeComponent();

            FillCmb();
        }


        // Comboboxlari doldurmaq
        public void FillCmb()
        {
            foreach (User user in db.Users.OrderBy(o=>o.Number).ToList())
            {
                cmbUser.Items.Add(user.Number + " " + user.Name + " " + user.Surname);
            }

            foreach (Book book in db.Books.OrderBy(o=>o.Name).ToList())
            {
                cmbBook.Items.Add(book.Name);
            }
        }

        // sag terefde yerlese Istifadeci melumatlarini doldurmaq
        public void FillLabel()
        {
            int num = Convert.ToInt32(cmbUser.SelectedItem.ToString().Substring(0, 6)); // cmbUser-deki uzvluk nomresini elde etmek
            selectedUser = db.Users.FirstOrDefault(u => u.Number == num);
            SelectedOrder = db.Orders.FirstOrDefault(o => o.UserId == selectedUser.Id);

            lblNumber.Text = selectedUser.Number.ToString();
            lblName.Text = selectedUser.Name + " " + selectedUser.Surname;

            try
            {
                lblBook.Text = db.Books.First(b => b.Id == SelectedOrder.BookId).Name;
                lblStartDate.Text = SelectedOrder.StartDate.ToString("dd/MMM/yyyy");
                lblEndDate.Text = SelectedOrder.EndDate.ToString("dd/MMM/yyyy");
            }
            catch (Exception)
            {
                lblBook.Text = "Yoxdur";
                lblStartDate.Text = "";
                lblEndDate.Text = "";
            }
        }

        // User secildikde labelleri doldurmaq
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabel();
        }

        // kitabi secmek
        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = cmbBook.SelectedItem.ToString();

            selectedBook = db.Books.First(y => y.Name == value);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUser.Text))
            {
                MessageBox.Show("İstifadəçini seçin");
                return;
            }
            else if (string.IsNullOrEmpty(cmbBook.Text))
            {
                MessageBox.Show("Götürmək istədiyiniz kitabı seçin");
                return;
            }

            if (selectedUser.Orders.Count>=1) 
            {
                MessageBox.Show("Maximum 1 kitab götürmək icazəniz var. Götürdüyünüz kitabı qaytardıqdan sonra kitab əldə edə bilərsiniz.");
                return;
            }

            if (selectedBook.Count > 0) // database-də bu kitabdan varsa..
            {
                Order order = new Order
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    BookId = selectedBook.Id,
                    UserId = selectedUser.Id,
                    DelayPrice = 5,
                    AdminId = null,
                    IsFinish = false
                };
                db.Orders.Add(order);
                db.SaveChanges();

                db.Books.Find(selectedBook.Id).Count -= 1;
                db.SaveChanges();

                MessageBox.Show("Sifariş qeydə alındı");
                cmbBook.Text = "";

                FillLabel();
            }
            else
            {
                MessageBox.Show(selectedBook.Name + " kitablarının hamısı sifariş olunub!");
                return;
            }
        }

    }
}
