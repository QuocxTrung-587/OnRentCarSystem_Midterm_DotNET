using BUS;
using DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Booking_Admin : Form
    {
        private BillBUS billBUS = new BillBUS();
        private CarsBUS carsBUS = new CarsBUS();
        private UsersBUS usersBUS = new UsersBUS();
        private DriverLisenseBUS driverLisenseBUS = new DriverLisenseBUS();
        private int actionCode = 0;
        private int billID = -1;
        public Booking_Admin()
        {
            InitializeComponent();
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


            List<UsersDTO> users = usersBUS.GetAllUsers(1);
            if (users == null)
            {
                cbxUser.Items.Add("null");
            }
            else
            {
                foreach (UsersDTO user in users)
                {
                    cbxUser.Items.Add(user.PhoneNumber);

                }
            }
            
            cbxUser.SelectedIndex = 0;
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            btnBooking.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            pnlBooking.Visible = false;
            dtDateCreate.Value = DateTime.Now;
            dtDateRent.Value = DateTime.Now;
            dtDateReturn.Value = DateTime.Now;
            txtTimeRent.Text = "";
            txtTotal.Text = "";
            bookingGV.DataSource = billBUS.GetAllBills();
        }
        private void bookBtn_Click(object sender, EventArgs e)
        {
        }
        private void custBtn_Click(object sender, EventArgs e)
        {
            Customer_Admin c = new Customer_Admin();
            c.Show();
            this.Hide();
        }
        private void carBtn_Click(object sender, EventArgs e)
        {
            Car_Admin v = new Car_Admin();
            v.Show();
            this.Hide();
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void bookingGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bookingGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow row = bookingGV.Rows[e.RowIndex];
                billID = Convert.ToInt32(row.Cells["BillID"].Value.ToString());
                cbxUser.SelectedItem = row.Cells["UserPhoneNumber"].Value.ToString();
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

        private void reportBtn_Click(object sender, EventArgs e)
        {
            Report_Admin r = new Report_Admin();
            r.Show();
            this.Hide();
        }

        private void scheduleBtn_Click(object sender, EventArgs e)
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

        private void btnBooking_Click(object sender, EventArgs e)
        {
            if (cbxUser.SelectedItem == null)
            {
                MessageBox.Show("Please choose a user");
            }
            else if (driverLisenseBUS.getDriverLisenseByPhoneNumber(cbxUser.SelectedItem.ToString()) == null)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = true;
            actionCode = 3;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = true;
            actionCode = 2;
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
                        Bill bill = new Bill(0, cbxUser.SelectedItem.ToString(), carID, dateCreate, 1, dateRent, dateReturn, timeRent, total);
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
                        Bill bill1 = new Bill(0, cbxUser.SelectedItem.ToString(), carID1, dateCreate1, 1, dateRent1, dateReturn1, timeRent1, total1);
                        billBUS.UpdateBill(bill1);
                        Booking_Load(sender, e);
                        break;
                    case 3:
                        if (billID != -1)
                        {
                            billBUS.DeleteBill(billID, cbxUser.SelectedItem.ToString());
                        }
                        
                        Booking_Load(sender, e);
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

                for (int i = 1; i <= bookingGV.Columns.Count; i++)
                {
                    workSheet.Cells[1, i].Value = bookingGV.Columns[i - 1].HeaderText;
                    workSheet.Cells[1, i].Style.Font.Bold = true;
                }

                for (int i = 0; i < bookingGV.Rows.Count; i++)
                {
                    for (int j = 0; j < bookingGV.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = bookingGV.Rows[i].Cells[j].Value.ToString();
                    }
                }

                for (int i = 1; i <= bookingGV.Columns.Count; i++)
                {
                    workSheet.Column(i).AutoFit();
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    File.WriteAllBytes(filePath, excel.GetAsByteArray());

                    MessageBox.Show("Export to Excel successful!");
                }

                excel.Dispose();
            }
    }
}