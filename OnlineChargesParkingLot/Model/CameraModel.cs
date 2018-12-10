using System;
using OnlineChargesParkingLot.Interface;

namespace OnlineChargesParkingLot.Model
{
    public class CameraModel
    {
        public CameraModel(string brand, string ipAddress, ushort port)
        {
            this.Brand = brand;
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        public string Brand { get; set; }

        public string IpAddress { get; set; }

        public ushort Port { get; set; }

        public bool IsConnection { get; set; }

        public Directions Direction { get; set; }

        public IDoor Door { get; set; }
    }

    public enum Directions
    {
        Enter = 0,
        Exit
    }
}