using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera.Model
{
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration(string Brand, string ip, ushort port, IntPtr mainHwnd, IntPtr containerHwnd, int streamType)
        {
            this.Brand = Brand;
            this.IP = ip;
            this.Port = port;
            this.MainHwnd = mainHwnd;
            this.ContainerHwnd = containerHwnd;
            this.StreamType = streamType;
        }

        /// <summary>
        /// 摄像机的品牌名称
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// 窗体的句柄
        /// </summary>
        public IntPtr MainHwnd { get; set; }

        /// <summary>
        /// 容器的句柄
        /// </summary>
        public IntPtr ContainerHwnd { get; set; }

        /// <summary>
        /// 摄像机的句柄
        /// </summary>
        internal int CameraHwnd { get; set; }

        /// <summary>
        /// 播放的句柄
        /// 只在HuoYan中使用
        /// </summary>
        internal int PlayHwnd { get; set; }

        /// <summary>
        /// 0 -> 主流码 1 -> 辅流码
        /// </summary>
        public int StreamType { get; set; }

    }
}
