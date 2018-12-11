using System;
using System.Drawing;
using System.IO;

namespace Camera
{
    public abstract class Device
    {
        public string ImagePath { get; set; }

        public Action<string, ushort, string> FindCameraCallback { get; set; }

        public Action<int, string, string, LicensePlateTypes, Color, Image, Image, DateTime> PlateReceviedCallback { get; set; }

        public abstract bool Close(ConnectionConfiguration configuration);

        public abstract void FindCamera();

        public abstract bool Connection(ConnectionConfiguration configuration);

        public abstract bool ManualCapture(ConnectionConfiguration configuration, string strFullPath);

        public abstract void UnInit();

        /// <summary>
        /// 设置图片的文件名称和路径
        /// </summary>
        /// <param name="pImagePath">全景图片</param>
        /// <param name="vImagePath">车牌图片</param>
        /// <returns></returns>
        public virtual DateTime SetImagePath(string licensePlatenumber, out string pImagePath, out string vImagePath)
        {
            DateTime now = DateTime.Now;
            string dateDirectory = ImagePath + @"\" + now.ToString("yyyyMMdd");
            if (!Directory.Exists(dateDirectory))
            {
                Directory.CreateDirectory(dateDirectory);
            }
            string dateTimeDirectory = now.ToString("yyyyMMddHHmmss");
            pImagePath = dateDirectory + $@"\{dateTimeDirectory}_{licensePlatenumber}.jpg";
            vImagePath = dateDirectory + $@"\{dateTimeDirectory}_{licensePlatenumber}_plate.jpg";
            return now;
        }
    }
}