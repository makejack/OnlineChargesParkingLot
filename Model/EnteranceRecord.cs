using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 对应数据库CBEnteranceRecrd表
    /// </summary>
    public class EnteranceRecord
    {
        public EnteranceRecord()
        {

        }

        public EnteranceRecord(string licensePlateNumber, DateTime entranceTime, int vehicleType)
        {
            this.PlateNumber = licensePlateNumber;
            this.EntranceTime = entranceTime;
            this.VehicleType = vehicleType;
        }

        public int ID { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 进入的时间
        /// </summary>
        public DateTime EntranceTime { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }
    }
}
