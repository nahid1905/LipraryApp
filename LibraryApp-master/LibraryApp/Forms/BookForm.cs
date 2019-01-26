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
    public partial class BookForm : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        private Models.Book selectedBook;

        public BookForm()
        {
            InitializeComponent();
            FillBooks();
        }

        // Butun kitablari cedvelde gostermek
        public void FillBooks()
        {
            dgvBooks.Rows.Clear();

            foreach (Models.Book book in db.Books.OrderBy(o=>o.Name).ToList())
            {
                dgvBooks.Rows.Add(book.Id,book.Name,book.Count);
            }
            db.SaveChanges();
        }

        // reset methodu
        public void Reset()
        {
            txtName.Text = "";
            numCount.Value = 0;

            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = true;

            FillBooks();

        }

        // Kitab elave etmek
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Kitabın adını yazın");
                return;
            }
            else if (numCount.Value==0)
            {
                MessageBox.Show("Kitabın sayını yazın");
            }
            // eger eyni adli kitab yoxdursa yenisini elave etmek
            else if (db.Books.FirstOrDefault(a => a.Name.ToLower() == txtName.Text.ToLower())==null)
            {
                Book book = new Book
                {
                    Name = txtName.Text,
                    Count = Convert.ToInt32(numCount.Value)
                };

                db.Books.Add(book);

                db.SaveChanges();

                MessageBox.Show("Yeni kitab əlavə edildi.");

                Reset();
            }
            // eger eyni adli kitab varsa sayini artirmaq
            else
            {
                Book existBook = db.Books.First(x=> x.Name.ToLower() == txtName.Text.ToLower());
                existBook.Count += Convert.ToInt32(numCount.Value);

                db.SaveChanges();

                MessageBox.Show("Kitab əlavə edildi.");

                Reset();
            }
        }


        // Setirlerin double klikinde melumatlari text boxlara getirmek

        private void GetBook(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells[0].Value.ToString());
            selectedBook = db.Books.Find(id);
            txtName.Text = selectedBook.Name;
            numCount.Value = selectedBook.Count;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        // Kitabları yeniləmək
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Kitabın adını yazın");
                return;
            }
            else
            {
                selectedBook.Name = txtName.Text;
                selectedBook.Count = Convert.ToInt32(numCount.Value.ToString());
                db.SaveChanges();
                FillBooks();
                
            }

            MessageBox.Show("Uğurla yeniləndi");
            Reset();
        }


        // kitabları silmək
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Silmək istəyirsiniz mi?", "Silmək", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                db.Books.Remove(selectedBook);

                db.SaveChanges();

                Reset();
            }
        }
    }
}
