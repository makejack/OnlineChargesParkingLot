using System;
using System.Collections.Generic;
using DAL.Interface;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Linq;
using Model;

namespace DAL
{
    public class OwnerInfoDAL : IOwnerInfoDAL, IDependency
    {
        public int Add(OwnerInfo t)
        {
            string sql = @"INSERT INTO CBLprUserInfo(UserName
            ,UserPlate
            ,StartTime
            ,StopTime
            ,UserType
            ,UserSex
            ,UserAge
            ,UserPhone
            ,UserAddress
            ,RegistrationTime
            ,PlateType) VALUES(@UserName
            ,@UserPlate
            ,@StartTime
            ,@StopTime
            ,@UserType
            ,@UserSex
            ,@UserAge
            ,@UserPhone
            ,@UserAddress
            ,@RegistrationTime
            ,@PlateType); SELECT LAST_INSERT_ROWID();";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql).FirstOrDefault();
            }
        }

        public void Delete(OwnerInfo t)
        {
            string sql = "DELETE FROM CBLprUserInfo WHERE ID=@ID;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }

        public IEnumerable<OwnerInfo> GetModels()
        {
            string sql = "SELECT * FROM CBLprUserInfo ; ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<OwnerInfo>(sql).ToList();
            }
        }

        public void Update(OwnerInfo t)
        {
            string sql = @"UPDATE CBLprUserInfo SET UserName=@UserName
            ,UserPlate=@UserPlate
            ,StartTime=@StartTime
            ,StopTime=@StopTime
            ,UserType=@UserType
            ,UserSex=@UserSex
            ,UserAge=@UserAge
            ,UserPhone=@UserPhone
            ,UserAddress=@UserAddress
            ,RegistrationTime=@RegistrationTime
            ,PlateType=@PlateType WHERE ID=@ID;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }

        public OwnerInfo Query(string licensePlateNumber)
        {
            string sql = "SELECT * FROM CBLprUserInfo where UserPlate = @UserPlate ;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Query<OwnerInfo>(sql, new { UserPlate = licensePlateNumber }).FirstOrDefault();
            }
        }
    }
}