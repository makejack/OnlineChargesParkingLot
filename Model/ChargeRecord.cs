using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 对应数据中CBTempChargeRecord表
    /// </summary>
    public class ChargeRecord
    {
        public int ID { get; set; }

        public string  CardNumber { get; set; }

        public string PlateNumber { get; set; }

        public DateTime EntranceTime { get; set; }

        public DateTime ExportTime { get; set; }

        public double ChargeAmount { get; set; }

        public string ManageName { get; set; }

        public double ActualAmount { get; set; }

        public int ExitNumber { get; set; }

        public int VehicleType { get; set; }

        public int FreeType { get; set; }

        public double FreeAmount { get; set; }
    }
}
