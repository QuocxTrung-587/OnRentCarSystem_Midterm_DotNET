using BUS;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Customer_Users : Form
    {
        private string phoneNumber;
        private UsersBUS usersBus = new UsersBUS();
        private int actionCode = 0;

        public Customer_Users(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
            InitializeGenderComboBox();
        }
        
        
        private void bookBtn_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking(phoneNumber);
            booking.Show();
            this.Hide();
        }
        
        private void customerBtn_Click(object sender, EventArgs e)
        {
            
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 3;
            allowEdit(true);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 1;
        }

        private void lisenseBtn_Click(object sender, EventArgs e)
        {
            Lisense_User lisense = new Lisense_User(phoneNumber);
            lisense.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPhoneNumber.Text = phoneNumber;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            Customer_Users_Load(sender, e);
        }

        private void pbDriverImage_Click(object sender, EventArgs e)
        {
            /*using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg; *.jpeg;*.png)|*.jpg|*.jpeg|*.png", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbDriverImage.Image = Image.FromFile(ofd.FileName);
                }
            }*/
        }

        private bool checkEmpty()
        {
            if (txtUserAddress.Text == "")
            {
                return true;
            }
            if (txtUserEmail.Text == "")
            {
                return true;
            }
            if (txtUserFullName.Text == "")
            {
                return true;
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Please make sure to fulfill all information!");
                return;
            }
            else
            {
                UsersDTO user = usersBus.getUserByPhoneNumber(phoneNumber);
                string fullname = txtUserFullName.Text;
                string email = txtUserEmail.Text;
                string address = txtUserAddress.Text;
                int gender = cbxUserGender.SelectedIndex;
                DateTime dateOfBirth = dtDateOfBirth.Value;

                user.UserFullName = fullname;
                user.UserEmail = email;
                user.UserAddress = address;
                user.UserGender = gender;
                user.UserDateOfBirth = dateOfBirth;

                usersBus.UpdateUser(user);
                Customer_Users_Load(sender, e);
            }

        }


        private void InitializeGenderComboBox()
        {
            cbxUserGender.Items.Add("Male");
            cbxUserGender.Items.Add("Female");

            cbxUserGender.SelectedIndex = 0;
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 2;
        }

        private void allowEdit(bool check)
        {
            txtUserAddress.Enabled = check;
            txtUserEmail.Enabled = check;
            txtUserFullName.Enabled = check;
            cbxUserGender.Enabled = check;
            dtDateOfBirth.Enabled = check;
        }

        private void Customer_Users_Load(object sender, EventArgs e)
        {
            allowEdit(false);
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtPhoneNumber.Enabled = false;

            txtUserAddress.Text = "";
            txtUserEmail.Text = "";
            txtUserFullName.Text = "";
            txtPhoneNumber.Text = phoneNumber;
            UsersDTO user = usersBus.getUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                btnEdit.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                if (user.UserGender == 0)
                {
                    cbxUserGender.SelectedIndex = 0;
                }
                else
                {
                    cbxUserGender.SelectedIndex = 1;
                }
                txtUserAddress.Text = user.UserAddress;
                txtUserEmail.Text = user.UserEmail;
                txtUserFullName.Text = user.UserFullName;
                dtDateOfBirth.Value = user.UserDateOfBirth;
            }
        }
    }
}