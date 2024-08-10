using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OnRentCarSystem
{
    public partial class Car_Admin : Form
    {
        private CarsBUS carsBUS = new CarsBUS();
        private int actionCode = 0;
        private List<string> selectedFeatures = new List<string>();
        private List<string> selectedFuelTypes = new List<string>();

        public Car_Admin()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            List<CarType> carTypes = carsBUS.GetAllCarTypes();
            foreach (CarType carType in carTypes)
            {
                cbxCarType.Items.Add(carType.CarTypeName);
            }
            cbxCarType.SelectedIndex = 0;

            List<Fuel> fuels = carsBUS.GetAllFuels();
            foreach (Fuel fuel in fuels)
            {
                cbxFuel.Items.Add(fuel.FuelName);
            }
            cbxFuel.SelectedIndex = 0;
        }

        private void Car_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            carGV.DataSource = carsBUS.loadData();
            txtCarID.Text = "";
            txtPrice.Text = "";
            cbxCarType.SelectedIndex = 0;
            cbxFuel.SelectedIndex = 0;
            chbRent.Checked = false;
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
            
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            pnlCar.Visible = true;
            txtCarID.Text = "";
            txtPrice.Text = "";
            cbxCarType.SelectedIndex = 0;
            cbxFuel.SelectedIndex = 0;
            chbRent.Checked = false;
            actionCode = 1;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            pnlCar.Visible = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 2;
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            pnlCar.Visible = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            actionCode = 3;

        }
        private void carGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (carGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow row = carGV.Rows[e.RowIndex];
                txtCarID.Text = row.Cells["CarID"].Value.ToString();
                txtPrice.Text = row.Cells["CarPrice"].Value.ToString();
                cbxCarType.SelectedIndex = Convert.ToInt32(row.Cells["CarTypeID"].Value.ToString());
                cbxFuel.SelectedIndex = Convert.ToInt32(row.Cells["FuelID"].Value.ToString());
                if (Convert.ToInt32(row.Cells["IsRent"].Value.ToString()) == 0)
                {
                    chbRent.Checked = false;
                }
                else
                {
                    chbRent.Checked = true;
                }
                CarFeature carFeature = carsBUS.GetCarFeatureByCarID(row.Cells["CarID"].Value.ToString());
                if (carFeature.CarMap == 1)
                {
                    chbCarMap.Checked = true;
                }
                if (carFeature.CarBluetooth == 1)
                {
                    chbCarBluetooth.Checked = true;
                }
                if (carFeature.CarBackwardCam == 1)
                {
                    chbCarBackwardCam.Checked = true;
                }
                if (carFeature.CarCurbsideCam == 1)
                {
                    chbCarCurbsideCam.Checked = true;
                }
                if (carFeature.CarJourneyCam == 1)
                {
                    chbCarJourneyCam.Checked = true;
                }
                if (carFeature.CarSpeedWarning == 1)
                {
                    chbCarSpeedWarning.Checked = true;
                }
                if (carFeature.CarTireSensor == 1)
                {
                    chbCarTireSensor.Checked = true;
                }
                if (carFeature.CarCollisionSensor == 1)
                {
                    chbCarCollisionSensor.Checked = true;
                }
                if (carFeature.CarSunroof == 1)
                {
                    chbCarSunroof.Checked = true;
                }
                if (carFeature.CarGPS == 1)
                {
                    chbCarGPS.Checked = true;
                }
                if (carFeature.CarUSB == 1)
                {
                    chbCarUSB.Checked = true;
                }
                if (carFeature.CarSubTire == 1)
                {
                    chbCarSubTire.Checked = true;
                }
                if (carFeature.CarTrunkLid == 1)
                {
                    chbCarTrunkLid.Checked = true;
                }
                if (carFeature.CarCam360 == 1)
                {
                    chbCarCam360.Checked = true;
                }
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            txtCarID.Text = "";
            txtPrice.Text = "";
            cbxCarType.SelectedIndex = 0;
            cbxFuel.SelectedIndex = 0;
            chbRent.Checked = false;
            Car_Load(sender, e);
        }

        private bool checkEmpty()
        {
            if (txtCarID.Text == "")
            {
                return true;
            }
            if (txtPrice.Text == "")
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
                        string carID = txtCarID.Text;
                        int price = Convert.ToInt32(txtPrice.Text);
                        int carType = cbxCarType.SelectedIndex;
                        int fuel = cbxFuel.SelectedIndex;
                        int rent = 0;
                        if (chbRent.Checked)
                        {
                            rent = 1;
                        }
                        Cars car = carsBUS.getCarByID(carID);
                        if (car == null)
                        {
                            car = new Cars(carID, carType, fuel, rent, price);
                            carsBUS.InsertCar(car);
                            pnlCar.Visible = false;
                            resetFilter();
                            pnlFilter.Visible = true;
                            /*Car_Load(sender, e);*/
                        }
                        else
                        {
                            MessageBox.Show("Car ID already exist");
                        }
                        break;
                    case 2:
                        string carID1 = txtCarID.Text;
                        int price1 = Convert.ToInt32(txtPrice.Text);
                        int carType1 = cbxCarType.SelectedIndex;
                        int fuel1 = cbxFuel.SelectedIndex;
                        int rent1 = 0;
                        if (chbRent.Checked)
                        {
                            rent = 1;
                        }
                        Cars car1 = new Cars(carID1, carType1, fuel1, rent1, price1);
                        carsBUS.UpdateCar(car1);
                        /*Car_Load(sender, e);*/
                        pnlCar.Visible = false;
                        pnlFilter.Visible = true;
                        break;
                    case 3:
                        string carID2 = txtCarID.Text;
                        carsBUS.DeleteCarFeature(carID2);
                        carsBUS.DeleteCar(carID2);
                        Car_Load(sender, e);
                        pnlCar.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlCar.Visible = false;
            Car_Load(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            pnlFilter.Visible = true;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            actionCode = 4;
        }

        private void btnSaveFilter_Click(object sender, EventArgs e)
        {
            if (actionCode == 1)
            {
                btnCancelFilter.Enabled = false;
                btnSaveFilter.Enabled = true;
                int carMap = 0;
                if (chbCarMap.Checked == true)
                {
                    carMap = 1;
                }
                int carBluetooth = 0;
                if (chbCarBluetooth.Checked == true)
                {
                    carBluetooth = 1;
                }
                int carBackwardCam = 0;
                if (chbCarBackwardCam.Checked == true)
                {
                    carBackwardCam = 1;
                }
                int carCurbsideCam = 0;
                if (chbCarCurbsideCam.Checked == true)
                {
                    carCurbsideCam = 1;
                }
                int carJourneyCam = 0;
                if (chbCarJourneyCam.Checked == true)
                {
                    carJourneyCam = 1;
                }
                int carSpeedWarning = 0;
                if (chbCarSpeedWarning.Checked == true)
                {
                    carSpeedWarning = 1;
                }
                int carTireSensor = 0;
                if (chbCarTireSensor.Checked == true)
                {
                    carTireSensor = 1;
                }
                int carCollisionSensor = 0;
                if (chbCarCollisionSensor.Checked == true)
                {
                    carCollisionSensor = 1;
                }
                int carSunroof = 0;
                if (chbCarSunroof.Checked == true)
                {
                    carSunroof = 1;
                }
                int carGPS = 0;
                if (chbCarGPS.Checked == true)
                {
                    carGPS = 1;
                }
                int carUSB = 0;
                if (chbCarUSB.Checked == true)
                {
                    carUSB = 1;
                }
                int carSubTire = 0;
                if (chbCarSubTire.Checked == true)
                {
                    carSubTire = 1;
                }
                int carTrunkLid = 0;
                if (chbCarTrunkLid.Checked == true)
                {
                    carTrunkLid = 1;
                }
                int carCam360 = 0;
                if (chbCarCam360.Checked == true)
                {
                    carCam360 = 1;
                }
                string carID = txtCarID.Text;
                CarFeature carFeature = new CarFeature(carID, carMap, carBluetooth, carBackwardCam, carCurbsideCam,
                    carJourneyCam, carSpeedWarning, carTireSensor, carCollisionSensor, carSunroof, carGPS, carUSB, carSubTire,
                    carTrunkLid, carCam360);
                carsBUS.InsertCarFeature(carFeature);
                pnlFilter.Visible = false;
                resetFilter();
                Car_Load(sender, e);
            }
            else if (actionCode == 2)
            {
                btnCancelFilter.Enabled = false;
                btnSaveFilter.Enabled = true;
                int carMap = 0;
                if (chbCarMap.Checked == true)
                {
                    carMap = 1;
                }
                int carBluetooth = 0;
                if (chbCarBluetooth.Checked == true)
                {
                    carBluetooth = 1;
                }
                int carBackwardCam = 0;
                if (chbCarBackwardCam.Checked == true)
                {
                    carBackwardCam = 1;
                }
                int carCurbsideCam = 0;
                if (chbCarCurbsideCam.Checked == true)
                {
                    carCurbsideCam = 1;
                }
                int carJourneyCam = 0;
                if (chbCarJourneyCam.Checked == true)
                {
                    carJourneyCam = 1;
                }
                int carSpeedWarning = 0;
                if (chbCarSpeedWarning.Checked == true)
                {
                    carSpeedWarning = 1;
                }
                int carTireSensor = 0;
                if (chbCarTireSensor.Checked == true)
                {
                    carTireSensor = 1;
                }
                int carCollisionSensor = 0;
                if (chbCarCollisionSensor.Checked == true)
                {
                    carCollisionSensor = 1;
                }
                int carSunroof = 0;
                if (chbCarSunroof.Checked == true)
                {
                    carSunroof = 1;
                }
                int carGPS = 0;
                if (chbCarGPS.Checked == true)
                {
                    carGPS = 1;
                }
                int carUSB = 0;
                if (chbCarUSB.Checked == true)
                {
                    carUSB = 1;
                }
                int carSubTire = 0;
                if (chbCarSubTire.Checked == true)
                {
                    carSubTire = 1;
                }
                int carTrunkLid = 0;
                if (chbCarTrunkLid.Checked == true)
                {
                    carTrunkLid = 1;
                }
                int carCam360 = 0;
                if (chbCarCam360.Checked == true)
                {
                    carCam360 = 1;
                }
                string carID = txtCarID.Text;
                CarFeature carFeature = new CarFeature(carID, carMap, carBluetooth, carBackwardCam, carCurbsideCam,
                    carJourneyCam, carSpeedWarning, carTireSensor, carCollisionSensor, carSunroof, carGPS, carUSB, carSubTire,
                    carTrunkLid, carCam360);
                carsBUS.UpdateCarFeature(carFeature);
                pnlFilter.Visible = false;
                resetFilter();
                Car_Load(sender, e);
            }
            else if (actionCode == 4)
            {
                btnCancelFilter.Enabled = true;
                btnSaveFilter.Enabled = true;

                selectedFeatures.Clear();
                if (chbCarMap.Checked)
                {
                    selectedFeatures.Add(chbCarMap.Text);
                }
                if (chbCarBluetooth.Checked)
                {
                    selectedFeatures.Add(chbCarBluetooth.Text);
                }
                if (chbCarBackwardCam.Checked)
                {
                    selectedFeatures.Add(chbCarBackwardCam.Text);
                }
                if (chbCarCurbsideCam.Checked)
                {
                    selectedFeatures.Add(chbCarCurbsideCam.Text);
                }
                if (chbCarJourneyCam.Checked)
                {
                    selectedFeatures.Add(chbCarJourneyCam.Text);
                }
                if (chbCarSpeedWarning.Checked)
                {
                    selectedFeatures.Add(chbCarSpeedWarning.Text);
                }
                if (chbCarTireSensor.Checked)
                {
                    selectedFeatures.Add(chbCarTireSensor.Text);
                }
                if (chbCarCollisionSensor.Checked)
                {
                    selectedFeatures.Add(chbCarCollisionSensor.Text);
                }
                if (chbCarSunroof.Checked)
                {
                    selectedFeatures.Add(chbCarSunroof.Text);
                }
                if (chbCarGPS.Checked)
                {
                    selectedFeatures.Add(chbCarGPS.Text);
                }
                if (chbCarUSB.Checked)
                {
                    selectedFeatures.Add(chbCarUSB.Text);
                }
                if (chbCarSubTire.Checked)
                {
                    selectedFeatures.Add(chbCarSubTire.Text);
                }
                if (chbCarTrunkLid.Checked)
                {
                    selectedFeatures.Add(chbCarTrunkLid.Text);
                }
                if (chbCarCam360.Checked)
                {
                    selectedFeatures.Add(chbCarCam360.Text);
                }
                selectedFuelTypes.Clear();
                if (chbGasoline.Checked)
                {
                    selectedFuelTypes.Add(chbGasoline.Text);
                }
                if (chbDiesel.Checked)
                {
                    selectedFuelTypes.Add(chbDiesel.Text);
                }
                if (chbElectricity.Checked)
                {
                    selectedFuelTypes.Add(chbElectricity.Text);
                }
                pnlFilter.Visible = false;
                carGV.DataSource = carsBUS.LoadDataWithFilters(selectedFeatures, selectedFuelTypes);
                resetFilter();
                

            }
        }

        private void resetFilter()
        {
            chbCarMap.Checked = false;
            chbCarBluetooth.Checked = false;
            chbCarBackwardCam.Checked = false;
            chbCarCurbsideCam.Checked = false;
            chbCarJourneyCam.Checked = false;
            chbCarSpeedWarning.Checked = false;
            chbCarTireSensor.Checked = false;
            chbCarCollisionSensor.Checked = false;
            chbCarSunroof.Checked = false;
            chbCarGPS.Checked = false;
            chbCarUSB.Checked = false;
            chbCarSubTire.Checked = false;
            chbCarTrunkLid.Checked = false;
            chbCarCam360.Checked = false;
            chbGasoline.Checked = false;
            chbDiesel.Checked = false;
            chbElectricity.Checked = false;
        }

        private void btnCancelFilter_Click(object sender, EventArgs e)
        {
            pnlFilter.Visible = false;
            Car_Load(sender, e);
        }

        private void btnFeature_Click(object sender, EventArgs e)
        {
            

        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            Report_Admin r = new Report_Admin();
            r.Show();
            this.Hide();
        }


    }
}