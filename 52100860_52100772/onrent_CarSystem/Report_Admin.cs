using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace OnRentCarSystem
{
    public partial class Report_Admin : Form
    {
        private CarsBUS carsBUS = new CarsBUS();
        private BillBUS billBUS = new BillBUS();

        public Report_Admin()
        {
            InitializeComponent();
        }

        

        private void Car_Load(object sender, EventArgs e)
        {
            
        }
        private void bookBtn_Click(object sender, EventArgs e)
        {
            Booking_Admin b = new Booking_Admin();
            b.Show();
            this.Hide();
        }
        private void custBtn_Click(object sender, EventArgs e)
        {
            Customer_Admin c = new Customer_Admin();
            c.Show();
            this.Hide();
        }
        private void carBtn_Click(object sender, EventArgs e)
        {
            Car_Admin car_Admin = new Car_Admin();
            car_Admin.Show();
            this.Hide();
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtStart.Value;
            DateTime endDate = dtEnd.Value.AddDays(1);

            var bills = billBUS.GetBillsByDateRange(startDate, endDate);

            chBill.Series.Clear();
            chBill.Series.Add("Number of Bills");
            chBill.Series.Add("Total Amount");

            foreach (var group in bills.GroupBy(b => b.DateCreate.Date))
            {
                DateTime date = group.Key;
                int numberOfBills = group.Count();
                float totalAmount = group.Sum(b => b.Total);

                chBill.Series["Number of Bills"].Points.AddXY(date, numberOfBills);
                chBill.Series["Total Amount"].Points.AddXY(date, totalAmount);
            }
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveDialog.RestoreDirectory = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                try
                {
                    PdfWriter.GetInstance(pdfDoc, new FileStream(saveDialog.FileName, FileMode.Create));

                    pdfDoc.Open();

                    Bitmap chartImage = new Bitmap(chBill.Width, chBill.Height);
                    chBill.DrawToBitmap(chartImage, new System.Drawing.Rectangle(0, 0, chBill.Width, chBill.Height));

                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(chartImage, ImageFormat.Bmp);

                    pdfImage.ScaleToFit(pdfDoc.PageSize.Width - pdfDoc.LeftMargin - pdfDoc.RightMargin, pdfDoc.PageSize.Height - pdfDoc.TopMargin - pdfDoc.BottomMargin);

                    pdfDoc.Add(pdfImage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail:  " + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Successful");
                    pdfDoc.Close();
                }
            }
        }
    }
}