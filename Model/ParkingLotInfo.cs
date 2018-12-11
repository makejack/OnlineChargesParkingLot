using System;

namespace Model
{
    public class ParkingLotInfo
    {
        public int ID { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkingName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 月租金额
        /// </summary>
        public double RentChargeAmount { get; set; }

        /// <summary>
        /// 收费模式
        /// </summary>
        public int ChargeMode { get; set; }

        /// <summary>
        /// 开门模式
        /// </summary>
        public int OpenMode { get; set; }

        /// <summary>
        /// 免费分钟数
        /// </summary>
        public int FreeMinutes { get; set; }

        /// <summary>
        /// 每日限额
        /// </summary>
        public double DailyLimit { get; set; }

        public double FirstCharge { get; set; }
        public double FirstMoney { get; set; }
        public double TwoCharge { get; set; }
        public double TwoMoney { get; set; }
        public double ThreeCharge { get; set; }
        public double ThreeMoney { get; set; }
        public double FourCharge { get; set; }
        public double FourMoney { get; set; }
        public double FiveCharge { get; set; }
        public double FiveMoney { get; set; }
        public double SixCharge { get; set; }
        public double SixMoney { get; set; }
        public double SevenCharge { get; set; }
        public double SevenMoney { get; set; }
        public double EightCharge { get; set; }
        public double EightMoney { get; set; }
        public double NineCharge { get; set; }
        public double NineMoney { get; set; }
        public double TenCharge { get; set; }
        public double TenMoney { get; set; }
    }
}