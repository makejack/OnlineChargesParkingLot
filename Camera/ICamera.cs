using System;
using Camera.Model;

namespace Camera.Interface
{

    public delegate void FindCameraHandle(object sender, CameraEventArgs e);

    public delegate void PlateReceivedHandle(object sender, PlateEventArgs info);
    
    internal interface ICamera
    {

        event FindCameraHandle FindCameraChange;

        event PlateReceivedHandle PlateReceivedChange; 

        bool Close(ConnectionConfiguration configuration);

        void FindCamera();

        bool Connection(ConnectionConfiguration configuration);

        bool ManualCapture(ConnectionConfiguration configuration, string strFullPath);

        void UnInit();
    }
}
