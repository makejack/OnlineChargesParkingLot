using System;

namespace Camera
{
    public class CameraEventArgs : EventArgs
    {
        public CameraEventArgs(string ip, ushort port, string brand)
        {
            this.IpAddress = ip;
            this.Port = port;
            this.Brand = brand;
        }

        public string IpAddress { get; }

        public ushort Port { get; }

        public string Brand { get; }
    }
}