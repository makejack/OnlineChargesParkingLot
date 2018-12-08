using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 对应数据库CBLprDelayRecord表
    /// </summary>
    public class OwnerDelayRecord
    {

        public int ID { get; set; }

        public string LprUserName { get; set; }

        public string LprUserPlate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public int ChargeAmount { get; set; }

        public string Operator { get; set; }

        public DateTime RecordTime { get; set; }

    }
}
