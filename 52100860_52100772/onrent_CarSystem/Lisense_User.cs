using BUS;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Lisense_User : Form
    {
        private string phoneNumber;
        private DriverLisenseBUS driverLisenseBUS = new DriverLisenseBUS();
        private int actionCode = 0;

        public Lisense_User(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
        }
        
        
        private void bookBtn_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking(phoneNumber);
            booking.Show();
            this.Hide();
        }
        
        private void customerBtn_Click(object sender, EventArgs e)
        {
            Customer_Users customer = new Customer_Users(phoneNumber);
            customer.Show();
            this.Hide();
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
            txtLisenseID.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            pbDriverImage.Enabled = true;
            actionCode = 3;
            allowEdit(true);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtLisenseID.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            pbDriverImage.Enabled = true;
            actionCode = 1;
            allowEdit(true);
        }

        private void lisenseBtn_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtLisenseID.Text = "";
            txtDriverFullName.Text = "";
            txtPhoneNumber.Text = phoneNumber;
            pbDriverImage.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            Lisense_User_Load(sender, e);
        }

        private void pbDriverImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg; *.jpeg;*.png)|*.jpg|*.jpeg|*.png", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbDriverImage.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private bool checkEmpty()
        {
            if (txtLisenseID.Text == "")
            {
                return true;
            }
            if (txtDriverFullName.Text == "")
            {
                return true;
            }
            if (pbDriverImage.Image == null)
            {
                return true;
            }
            return false;
        }

        private void allowEdit(bool check)
        {
            txtDriverFullName.Enabled = check;
            txtPhoneNumber.Enabled = check;
            dtDateOfBirth.Enabled = check;
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
                switch (actionCode)
                {
                    case 1:
                        string lisenseID = txtLisenseID.Text;
                        string driverFullName = txtDriverFullName.Text;
                        DateTime dateOfBirth = dtDateOfBirth.Value;
                        string phoneNumber = txtPhoneNumber.Text;
                        byte[] image = ConvertImageToBytes(pbDriverImage.Image);
                        DriverLisense driver = driverLisenseBUS.getDriverLisenseByPhoneNumber(phoneNumber);
                        if (driver == null)
                        {
                            driver = new DriverLisense(lisenseID, driverFullName, dateOfBirth, image, phoneNumber);
                            driverLisenseBUS.InsertLisense(driver);
                            Lisense_User_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Lisense ID already exist");
                        }
                        
                        break;
                    case 2:
                        string lisenseID1 = txtLisenseID.Text;
                        string phoneNumber1 = txtPhoneNumber.Text;
                        driverLisenseBUS.DeleteDriverLisense(lisenseID1, phoneNumber1);
                        Lisense_User_Load(sender, e);
                        break;
                    case 3:
                        string lisenseID2 = txtLisenseID.Text;
                        string driverFullName2 = txtDriverFullName.Text;
                        DateTime dateOfBirth2 = dtDateOfBirth.Value;
                        string phoneNumber2 = txtPhoneNumber.Text;
                        byte[] image2 = ConvertImageToBytes(pbDriverImage.Image);
                        DriverLisense driver2 = new DriverLisense(lisenseID2, driverFullName2, dateOfBirth2, image2, phoneNumber2);
                        driverLisenseBUS.UpdateDriverLisense(driver2);
                        Lisense_User_Load(sender, e);
                        break;
                    default:
                        break;
                }
                Lisense_User_Load(sender, e);
            }
            
        }

        private byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void Lisense_User_Load(object sender, EventArgs e)
        {
            /*customerGV.Enabled = false;*/
            allowEdit(false);
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            pbDriverImage.Enabled = false;
            txtPhoneNumber.Enabled = false;
            txtLisenseID.Enabled = false;
            txtLisenseID.Text = "";
            txtDriverFullName.Text = "";
            txtPhoneNumber.Text = phoneNumber;
            DriverLisense driver = driverLisenseBUS.getDriverLisenseByPhoneNumber(phoneNumber);
            if (driver == null)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtLisenseID.Text = driver.LisenseID;
                txtDriverFullName.Text = driver.DriverFullName;
                dtDateOfBirth.Text = driver.DriverDateOfBirth.Date.ToString();
                txtPhoneNumber.Text = driver.PhoneNumber;
                MemoryStream ms = new MemoryStream(driver.DriverImage);
                pbDriverImage.Image = new Bitmap(ms);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtLisenseID.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            pbDriverImage.Enabled = true;
            actionCode = 2;
            allowEdit(true);
        }
    }
}