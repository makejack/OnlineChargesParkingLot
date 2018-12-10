using System;

namespace Model
{
    /// <summary>
    /// 对应数据库中的CBManageInfo表
    /// </summary>
    public class AdminInfo
    {
        public int ID { get; set; }

        public string ManageName { get; set; }

        public string UserName { get; set; }

        public string ManagePwd { get; set; }

        public int ManageType { get; set; }

    }
}