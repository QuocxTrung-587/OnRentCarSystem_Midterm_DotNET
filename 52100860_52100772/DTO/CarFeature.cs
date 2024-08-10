using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CarFeature
    {
        private string carID;
        private int carMap;
        private int carBluetooth;
        private int carBackwardCam;
        private int carCurbsideCam;
        private int carJourneyCam;
        private int carSpeedWarning;
        private int carTireSensor;
        private int carCollisionSensor;
        private int carSunroof;
        private int carGPS;
        private int carUSB;
        private int carSubTire;
        private int carTrunkLid;
        private int carCam360;

        public CarFeature(string carID, int carMap, int carBluetooth, int carBackwardCam
            , int carCurbsideCam, int carJourneyCam, int carSpeedWarning, int carTireSensor
            , int carCollisionSensor, int carSunroof, int carGPS, int carUSB, int carSubTire
            , int carTrunkLid, int carCam360)
        {
            this.carID = carID;
            this.carMap = carMap;
            this.carBluetooth = carBluetooth;
            this.carBackwardCam = carBackwardCam;
            this.carCurbsideCam = carCurbsideCam;
            this.carJourneyCam = carJourneyCam;
            this.carSpeedWarning = carSpeedWarning;
            this.carTireSensor = carTireSensor;
            this.carCollisionSensor = carCollisionSensor;
            this.carSunroof = carSunroof;
            this.carGPS = carGPS;
            this.carUSB = carUSB;
            this.carSubTire = carSubTire;
            this.carTrunkLid = carTrunkLid;
            this.carCam360 = carCam360;
        }

        public string CarID { get => carID; set => carID = value; }
        public int CarMap { get => carMap; set => carMap = value; }
        public int CarBluetooth { get => carBluetooth; set => carBluetooth = value; }
        public int CarBackwardCam { get => carBackwardCam; set => carBackwardCam = value; }
        public int CarCurbsideCam { get => carCurbsideCam; set => carCurbsideCam = value; }
        public int CarJourneyCam { get => carJourneyCam; set => carJourneyCam = value; }
        public int CarSpeedWarning { get => carSpeedWarning; set => carSpeedWarning = value; }
        public int CarTireSensor { get => carTireSensor; set => carTireSensor = value; }
        public int CarCollisionSensor { get => carCollisionSensor; set => carCollisionSensor = value; }
        public int CarSunroof { get => carSunroof; set => carSunroof = value; }
        public int CarGPS { get => carGPS; set => carGPS = value; }
        public int CarUSB { get => carUSB; set => carUSB = value; }
        public int CarSubTire { get => carSubTire; set => carSubTire = value; }
        public int CarTrunkLid { get => carTrunkLid; set => carTrunkLid = value; }
        public int CarCam360 { get => carCam360; set => carCam360 = value; }
    }
}
