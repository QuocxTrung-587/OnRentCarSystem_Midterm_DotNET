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
    public partial class Register : Form
    {
        public const string phoneValid = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public UsersBUS usersBUS = new UsersBUS();

        public Register()
        {
            InitializeComponent();
        }

        private void lblReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (lblValidPhoneNumber.Visible == false && lblValidPassword.Visible == false && lblRetypeValid.Visible == false)
            {
                UsersDTO usersDTO = usersBUS.getUserByPhoneNumber(txtPhoneNumber.Text);
                if (usersDTO == null)
                {
                    usersDTO = new UsersDTO();
                    usersDTO.PhoneNumber = txtPhoneNumber.Text;
                    usersDTO.Pass = txtPassword.Text;
                    usersDTO.UserRole = 1;
                    btnRegister.Enabled = true;
                    if (usersBUS.InsertUser(usersDTO))
                    {
                        MessageBox.Show("Successful");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                    }
                }
                else
                {
                    MessageBox.Show("Phone number already exist");
                }
                
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = txtPhoneNumber.Text;
            if (!Regex.IsMatch(phoneNumber, phoneValid))
            {
                lblValidPhoneNumber.Visible = true;
            }
            else
            {
                lblValidPhoneNumber.Visible = false;
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            /*btnRegister.Enabled = false;*/
            txtPassword.PasswordChar = '*';
            txtRetypePassword.PasswordChar = '*';
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            if (!hasNumber.IsMatch(txtPassword.Text))
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

        private void txtRetypePassword_TextChanged(object sender, EventArgs e)
        {
            if (!txtRetypePassword.Text.Equals(txtPassword.Text))
            {
                lblRetypeValid.Visible = true;
            }
            else
            {
                lblRetypeValid.Visible = false;
            }
        }
    }
}
