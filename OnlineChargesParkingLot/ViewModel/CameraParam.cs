using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera;

namespace OnlineChargesParkingLot.ViewModel
{
    public class CameraParam
    {
        public CameraParam(string ip, ushort port, string brand)
        {
            this.Ip = ip;
            this.Port = port;
            this.Brand = brand;
        }

        public string Ip { get; set; }

        public ushort Port { get; set; }

        public string Brand { get; set; }

        public bool IsConnection { get; set; }

        public Directions Direction { get; set; }
    }

    public enum Directions
    {
        Enter = 0,
        Exit
    }
}
