using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Customer_Admin : Form
    {
        private UsersBUS usersBUS = new UsersBUS();
        private int actionCode = 0;
        public const string phoneValid = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public Customer_Admin()
        {
            InitializeComponent();
            InitializeGenderComboBox();
        }

        private void InitializeGenderComboBox()
        {
            cbxUserGender.Items.Add("Male");
            cbxUserGender.Items.Add("Female");

            cbxUserGender.SelectedIndex = 0;
        }
        private void Customers_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            List<UsersDTO> users = usersBUS.GetAllUsers(1);
            customerGV.DataSource = users;
            txtPhoneNumber.Text = "";
            txtPass.Text = "";
            txtUserFullName.Text = "";
            cbxUserGender.SelectedIndex = 0;
            txtUserEmail.Text = "";
            txtUserAddress.Text = "";
        }
        private void bookBtn_Click(object sender, EventArgs e)
        {
            /*Booking b = new Booking();
            b.Show();
            this.Hide();*/
            Booking_Admin b = new Booking_Admin();
            b.Show();
            this.Hide();
        }
        private void carBtn_Click(object sender, EventArgs e)
        {
            Car_Admin car = new Car_Admin();
            car.Show();
            this.Hide();
        }
        private void customerBtn_Click(object sender, EventArgs e)
        {
            
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void customerGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow row = customerGV.Rows[e.RowIndex];
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
                txtPass.Text = row.Cells["Pass"].Value.ToString();
                txtUserFullName.Text = row.Cells["UserFullName"].Value.ToString();
                dtDateOfBirth.Value = Convert.ToDateTime(row.Cells["UserDateOfBirth"].Value);
                cbxUserGender.SelectedIndex = Convert.ToInt32(row.Cells["UserGender"].Value.ToString());
                txtUserEmail.Text = row.Cells["UserEmail"].Value.ToString();
                txtUserAddress.Text = row.Cells["UserAddress"].Value.ToString();
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
        
        private void Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            txtPhoneNumber.Text = "";
            txtPass.Text = "";
            txtUserFullName.Text = "";
            cbxUserGender.SelectedIndex = 0;
            txtUserEmail.Text = "";
            txtUserAddress.Text = "";
            dtDateOfBirth.Value = DateTime.Now;
            actionCode = 1;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 2;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 3;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
            Customers_Load(sender, e);
        }

        private bool checkEmpty()
        {
            if (txtPass.Text == "")
            {
                return true;
            }
            if (txtPhoneNumber.Text == "")
            {
                return true;
            }
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
                MessageBox.Show("Please make sure to fulfill all infomation!");
                return;
            }
            else
            {
                if (lblValidPhoneNumber.Visible == false && lblValidPassword.Visible == false)
                {
                    
                    switch (actionCode)
                    {
                        case 1:
                            string phoneNumber = txtPhoneNumber.Text;
                            string address = txtUserAddress.Text;
                            string email = txtUserEmail.Text;
                            string fullname = txtUserFullName.Text;
                            string password = txtPass.Text;
                            int gender = cbxUserGender.SelectedIndex;
                            DateTime dateOfBirth = dtDateOfBirth.Value;
                            UsersDTO user = usersBUS.getUserByPhoneNumber(phoneNumber);
                            if (user == null)
                            {
                                user = new UsersDTO(phoneNumber, password, fullname, dateOfBirth, gender, email, address, 1);
                                usersBUS.InsertUser(user);
                                pnlCustomer.Visible = false;
                                Customers_Load(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Phone number already exist");
                                return;
                            }
                            break;
                        case 2:
                            string phoneNumber1 = txtPhoneNumber.Text;
                            string address1 = txtUserAddress.Text;
                            string email1 = txtUserEmail.Text;
                            string fullname1 = txtUserFullName.Text;
                            string password1 = txtPass.Text;
                            int gender1 = cbxUserGender.SelectedIndex;
                            DateTime dateOfBirth1 = dtDateOfBirth.Value;
                            UsersDTO user1 = new UsersDTO(phoneNumber1, password1, fullname1, dateOfBirth1, gender1, email1, address1, 1);
                            usersBUS.UpdateUser(user1);
                            pnlCustomer.Visible = false;
                            Customers_Load(sender, e);
                            break;
                        case 3:
                            string phoneNumber2 = txtPhoneNumber.Text;
                            usersBUS.DeleteUser(phoneNumber2);
                            pnlCustomer.Visible = false;
                            Customers_Load(sender, e);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            txtPhoneNumber.Text = "";
            txtPass.Text = "";
            txtUserFullName.Text = "";
            cbxUserGender.SelectedIndex = 0;
            txtUserEmail.Text = "";
            txtUserAddress.Text = "";
            Customers_Load(sender, e);
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

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            if (!hasNumber.IsMatch(txtPass.Text))
            {
                lblValidPassword.Text = "Password must contain number";
                lblValidPassword.Visible = true;
            }
            else if (!hasUpperChar.IsMatch(txtPass.Text))
            {
                lblValidPassword.Text = "Password must contain uppercase";
                lblValidPassword.Visible = true;
            }
            else if (!hasLowerChar.IsMatch(txtPass.Text))
            {
                lblValidPassword.Text = "Password must contain lowercase";
                lblValidPassword.Visible = true;
            }
            else if (!hasMinimum8Chars.IsMatch(txtPass.Text))
            {
                lblValidPassword.Text = "Password must be at least 8 digits";
                lblValidPassword.Visible = true;
            }
            else
            {
                lblValidPassword.Visible = false;
            }
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            Report_Admin r = new Report_Admin();
            r.Show();
            this.Hide();
        }
    }
}