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
    public partial class UserForm : Form
    {
        LibraryDbEntities db = new LibraryDbEntities();

        string num;

        User selectedUser;

        public UserForm()
        {
            InitializeComponent();
            FillUsers();
        }

        // Cedvele userleri doldurmaq
        public void FillUsers()
        {
            dgvUsers.Rows.Clear();
            foreach (User user in db.Users.OrderBy(o=>o.Number).ToList())
            {
                dgvUsers.Rows.Add(user.Id, user.Number, user.Name, user.Surname, user.Phone, user.Email);
            }

            db.SaveChanges();
        }

        // Reset 
        public void Reset()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

            FillUsers();

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        // Uzvluk nomresi ucun unikal eded tapmaq
        public string RandomCount()
        {
            num = (DateTime.Now.Millisecond +""
                    + DateTime.Now.Second+""
                    + DateTime.Now.Minute+""
                    +DateTime.Now.Year).ToString();
            if (num.Length>6)
            {
                num = num.Substring(0,6);
                return num;
            }
            else if (num.Length<6)
            {
                num = Math.Pow(Convert.ToInt32(num),Convert.ToInt32(num)).ToString();
                num.Substring(0, 6);
                return num;
            }
            return num;
        }

        // User elave etmek
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || 
                string.IsNullOrEmpty(txtSurname.Text) || 
                string.IsNullOrEmpty(txtPhone.Text) || 
                string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Formanı düzgün doldurun");
                return;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email adresiniz yalnışdır");
                return;
            }
            RandomCount();
            int sameNumber  = Convert.ToInt32(num);
            if (db.Users.FirstOrDefault(n=>n.Number == sameNumber) != null)
            {
                MessageBox.Show("Bir daha cəhd edin!");
                return;
            }
            User user = new User
            {
                Number = Convert.ToInt32(num),
                Name = txtName.Text,
                Surname = txtSurname.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text
            };
           
            MessageBox.Show("Yeni istifadəçi əlavə edildi");
            MessageBox.Show("Sizin üzvlük nömrəniz: "  + user.Number.ToString());
            db.Users.Add(user);

            db.SaveChanges();
            Reset();
        }

        // USerleri text boxlara doldurmaq
        public void GetUsers(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells[0].Value.ToString());

            selectedUser = db.Users.Find(id);

            txtName.Text = selectedUser.Name;
            txtSurname.Text = selectedUser.Surname;
            txtPhone.Text = selectedUser.Phone;
            txtEmail.Text = selectedUser.Email;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        // Userleri yenilemek
        public void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)||string.IsNullOrEmpty(txtSurname.Text)||string.IsNullOrEmpty(txtPhone.Text)||string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Formanı düzgün doldurun");
                return;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email adresiniz yalnışdır");
                return;
            }
            selectedUser.Name = txtName.Text;
            selectedUser.Surname = txtSurname.Text;
            selectedUser.Phone = txtPhone.Text;
            txtEmail.Text = txtEmail.Text;

            db.SaveChanges();
            MessageBox.Show("Uğurla yeniləndi");
            Reset();
        }

        // Userleri silmek
        private void btnDelete_Click(object sender, EventArgs e)
        {
            db.Users.Remove(selectedUser);

            DialogResult result = MessageBox.Show("Silmək istəyirsinizmi?", "Silmək", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                db.Users.Remove(selectedUser);
                db.SaveChanges();
                Reset();
            }
        }
    }
}
