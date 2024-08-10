using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Booking : Form
    {
        private string phoneNumber;
        private BillBUS billBUS = new BillBUS();
        private CarsBUS carsBUS = new CarsBUS();
        private DriverLisenseBUS driverLisenseBUS = new DriverLisenseBUS();
        private int actionCode = 0;
        private int billID = -1;

        public Booking(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            List<Cars> cars = carsBUS.GetAllCars();
            if (cars == null)
            {
                cbxCar.Items.Add("null");
            }
            else
            {
                foreach (Cars car in cars)
                {
                    cbxCar.Items.Add(car.CarsID);

                }

            }
            cbxCar.SelectedIndex = 0;
            
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            lblPhoneNumber.Text = phoneNumber;
            btnHistory.Enabled = true;
            btnBooking.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            pnlBooking.Visible = false;
            dtDateCreate.Value = DateTime.Now;
            dtDateRent.Value = DateTime.Now;
            dtDateReturn.Value = DateTime.Now;
            txtTimeRent.Text = "";
            txtTotal.Text = "";
        }
        private void bookBtn_Click(object sender, EventArgs e)
        {
            
        }
        private void custBtn_Click(object sender, EventArgs e)
        {
            Customer_Users customer = new Customer_Users(phoneNumber);
            customer.Show();
            this.Hide();
        }
        private void carBtn_Click(object sender, EventArgs e)
        {
            Lisense_User lisense = new Lisense_User(phoneNumber);
            lisense.Show();
            this.Hide();
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }
        private void issueBtn_Click(object sender, EventArgs e)
        {
            bookingGV.DataSource = billBUS.GetAllBillsByPhoneNumber(phoneNumber);
        }
        private void returnBtn_Click(object sender, EventArgs e)
        {
            if (driverLisenseBUS.getDriverLisenseByPhoneNumber(phoneNumber) == null)
            {
                MessageBox.Show("User must have driver lisense to book car");
            }
            else
            {
                pnlBooking.Visible = true;
                dtDateCreate.Value = DateTime.Now;
                dtDateRent.Value = DateTime.Now;
                dtDateReturn.Value = DateTime.Now;
                txtTimeRent.Text = "";
                txtTotal.Text = "";
                actionCode = 1;
            }
            
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = true;
            actionCode = 3;
        }
        private void bookingGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bookingGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow row = bookingGV.Rows[e.RowIndex];
                billID = Convert.ToInt32(row.Cells["BillID"].Value.ToString());
                cbxCar.SelectedItem = row.Cells["CarID"].Value.ToString();
                dtDateCreate.Value = Convert.ToDateTime(row.Cells["DateCreate"].Value);
                dtDateRent.Value = Convert.ToDateTime(row.Cells["DateRent"].Value);
                dtDateReturn.Value = Convert.ToDateTime(row.Cells["DateReturn"].Value);
                txtTimeRent.Text = row.Cells["TimeRent"].Value.ToString();
                txtTotal.Text = row.Cells["Total"].Value.ToString();
                btnBooking.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
        private void bookingGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void bookingGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = false;
            Booking_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = true;
            actionCode = 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = false;
            Booking_Load(sender, e);
        }

        private bool checkEmpty()
        {
            if (cbxCar.SelectedItem == null)
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
                switch (actionCode)
                {
                    case 1:
                        string carID = cbxCar.SelectedItem.ToString();
                        DateTime dateRent = dtDateRent.Value;
                        DateTime dateCreate = DateTime.Now;
                        DateTime dateReturn = dtDateReturn.Value;
                        TimeSpan timeRent = (dateReturn - dateRent);
                        float total = carsBUS.getCarByID(carID).CarPrice;
                        Bill bill = new Bill(0, lblPhoneNumber.Text, carID, dateCreate, 1, dateRent, dateReturn, timeRent, total);
                        billBUS.InsertBill(bill);
                        Booking_Load(sender, e);
                        break;
                    case 2:
                        string carID1 = cbxCar.SelectedItem.ToString();
                        DateTime dateRent1 = dtDateRent.Value;
                        DateTime dateCreate1 = DateTime.Now;
                        DateTime dateReturn1 = dtDateReturn.Value;
                        TimeSpan timeRent1 = (dateReturn1 - dateRent1);
                        float total1 = carsBUS.getCarByID(carID1).CarPrice;
                        Bill bill1 = new Bill(0, lblPhoneNumber.Text, carID1, dateCreate1, 1, dateRent1, dateReturn1, timeRent1, total1);
                        billBUS.UpdateBill(bill1);
                        Booking_Load(sender, e);
                        break;
                    case 3:
                        if (billID != -1)
                        {
                            billBUS.DeleteBill(billID, phoneNumber);
                        }
                        Booking_Load(sender, e);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}