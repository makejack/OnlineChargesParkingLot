﻿using System;
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

        public int ID { get; set; }

        public string PlateNumber { get; set; }

        public DateTime EntranceTime { get; set; }

        public int VehicleType { get; set; }
    }
}
