using Model;
using System;
using DAL.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;

namespace DAL
{
    public class AdminInfoDAL : IAdminInfoDAL, IDependency
    {
        public int Add(AdminInfo t)
        {
            string sql = @"INSERT INTO CBManageInfo(ManageName
            ,UserName
            ,ManagePwd
            ,ManageType) VALUES(@ManageName
            ,@UserName
            ,@ManagePwd
            ,@ManageType); SELECT LAST_INSERT_ROWID();";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql).FirstOrDefault();
            }
        }

        public void Delete(AdminInfo t)
        {
            string sql = "DELETE FROM CBManageInfo WHERE ID =@ID";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public IEnumerable<AdminInfo> GetModels()
        {
            string sql = "SELECT * FROM CBManageInfo;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<AdminInfo>(sql).ToList();
            }
        }

        public void Update(AdminInfo t)
        {
            string sql = @"UPDATE CBManageInfo SET ManageName=@ManageName
            ,UserName=@UserName
            ,ManagePwd=@ManagePwd
            ,ManageType=@ManageType;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public AdminInfo Query(string account, string pwd)
        {
            string sql = "SELECT * FROM CBManageInfo WHERE ManageName = @ManageName AND ManagePwd = @ManagePwd";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<AdminInfo>(sql, new { ManageName = account, ManagePwd = pwd }).FirstOrDefault();
            }
        }
    }
}