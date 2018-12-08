using System;
using Camera.Model;

namespace Camera.Interface
{
    internal interface ICamera
    {
        void Close(ConnectionConfiguration configuration);

        void FindCamera();

        bool Connection(ConnectionConfiguration configuration);

        bool ManualCapture(ConnectionConfiguration configuration, string strFullPath);

        void UnInit();
    }
}
