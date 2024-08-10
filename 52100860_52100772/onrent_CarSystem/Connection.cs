using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public class Connection
    {
        private static SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-T0CB33S;Initial Catalog=OnRentCarSystemDB;Integrated Security=True");
        static SqlCommand myCmd;
        public static void GetRentedData(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getCustomer", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetCarData(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getCar", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetRentalData(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getBooking", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void DeleteCustomer(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "delete from Customer where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void DeleteCar(TextBox a, TextBox b, String id)
        {
            string query = "delete from Car where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
               
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void DeleteBooking(String id)
        {
            string query = "delete from Booking where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void AddCustomer(TextBox a, TextBox b, TextBox c)
        {
            string query = "insert into Customer(Name,Phone,Address) values('" + a.Text + "','" + b.Text + "','" + c.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
        }
        public static void AddBooking(TextBox a, TextBox b, DateTimePicker c, DateTimePicker d)
        {
                string query = "insert into Booking(Customer_ID,Car_ID,Start,Due,Status) values(" + Convert.ToInt32(a.Tag) + "," + Convert.ToInt32(b.Tag) + ",'" + c.Value.ToString("dd MMMM yy") + "','" + d.Value.ToString("dd MMMM yy") + "','Issue');";
                try
                {
                    myCon.Open();
                    myCmd = new SqlCommand(query, myCon);
                    myCmd.ExecuteReader();
                    myCon.Close();
                    MessageBox.Show("Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    myCon.Close();
                }
        }
        public static void AddCar(TextBox a, TextBox b)
        {
            string query = "insert into Car(Name,Cost) values('" + a.Text + "','" + b.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateCustomer(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "update Customer set Name='" + a.Text + "',Phone='" + b.Text + "', Address='" + c.Text + "' where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateCar(TextBox a, TextBox b, String id)
        {
            string query = "update Car set Name='" + a.Text + "', Cost='" + b.Text + "' where ID=" + Convert.ToInt32(id) +"; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
        
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateBooking(TextBox a, TextBox b, DateTimePicker c, DateTimePicker d, String id, int i)
        {
            string query = "update Booking set Customer_ID=" + Convert.ToInt32(a.Tag) + ", Car_ID=" + Convert.ToInt32(b.Tag) + ", Start='" + c.Value.ToString("dd MMMM yy") + "',Due='" + d.Value.ToString("dd MMMM yy") + "',Status='Return' where ID=" + Convert.ToInt32(id) + ";";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Total Rent Cost is " + i.ToString() + "$", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}