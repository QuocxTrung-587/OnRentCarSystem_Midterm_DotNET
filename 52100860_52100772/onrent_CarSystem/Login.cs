using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Login : Form
    {
        public const string phoneValid = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public UsersBUS usersBUS = new UsersBUS();

        public Login()
        {
            InitializeComponent();
        }

        private void lblForget_Click(object sender, EventArgs e)
        {

        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (lblValidPhoneNumber.Visible == false && lblValidPassword.Visible == false)
            {
                UsersDTO usersDTO = usersBUS.Login(txtPhoneNumber.Text, txtPassword.Text);
                if (usersDTO!= null)
                {
                    MessageBox.Show("Successful");
                    if (usersDTO.UserRole == 0)
                    {
                        Customer_Admin customerAdmin = new Customer_Admin();
                        customerAdmin.Show();
                        this.Hide();
                    }
                    else if (usersDTO.UserRole == 1)
                    {
                        /*Booking booking = new Booking(usersDTO.PhoneNumber);
                        booking.Show();
                        this.Hide();*/

                        Customer_Users customer = new Customer_Users(usersDTO.PhoneNumber);
                        customer.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = txtPhoneNumber.Text;
            if (txtPhoneNumber.Text == "admin")
            {
                lblValidPhoneNumber.Visible = false;
            }
            else if (!Regex.IsMatch(phoneNumber, phoneValid))
            {
                lblValidPhoneNumber.Visible = true;
            }
            else
            {
                lblValidPhoneNumber.Visible = false;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            if (txtPassword.Text == "123456")
            {
                lblValidPassword.Visible = false;
            }
            else if (!hasNumber.IsMatch(txtPassword.Text))
            {
                lblValidPassword.Text = "Password must contain number";
                lblValidPassword.Visible = true;
            }
            else if (!hasUpperChar.IsMatch(txtPassword.Text))
            {
                lblValidPassword.Text = "Password must contain uppercase";
                lblValidPassword.Visible = true;
            }
            else if (!hasLowerChar.IsMatch(txtPassword.Text))
            {
                lblValidPassword.Text = "Password must contain lowercase";
                lblValidPassword.Visible = true;
            }
            else if (!hasMinimum8Chars.IsMatch(txtPassword.Text))
            {
                lblValidPassword.Text = "Password must be at least 8 digits";
                lblValidPassword.Visible = true;
            }
            else
            {
                lblValidPassword.Visible = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}
