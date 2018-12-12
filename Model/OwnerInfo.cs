using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 对应数据库CBLprUserInfo表
    /// </summary>
    public class OwnerInfo
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string UserPlate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        /// <summary>
        /// 黑白名单 0 -> 白  1 -> 黑
        /// </summary>
        public int UserType { get; set; }

        public int UserSex { get; set; }

        public int UserAge { get; set; }

        public string UserPhone { get; set; }

        public string UserAddress { get; set; }

        public DateTime RegistrationTime { get; set; }


        /// <summary>
        /// 用户类型 0 -> 月租车辆 1 -> 固定车辆 2 -> 定距卡车辆
        /// </summary>
        public int PlateType { get; set; }

    }
}
