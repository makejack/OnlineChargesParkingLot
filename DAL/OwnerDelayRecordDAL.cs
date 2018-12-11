using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Dapper;
using DAL.Interface;
using Model;

namespace DAL
{
    public class OwnerDelayRecordDAL : IOwnerDelayRecordDAL,IDependency
    {
        public int Add(OwnerDelayRecord t)
        {
            string sql = @"INSERT INTO CBLprDelayRecord(LprUserName
,LprUserPlate
,StartTime
,StopTime
,ChargeAmount
,Operator
,RecordTime) VALUES(@LprUserName
,@LprUserPlate
,@StartTime
,@StopTime
,@ChargeAmount
,@Operator
,@RecordTime);SELECT LAST_INSERT_ROWID();";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql).FirstOrDefault();
            }
        }

        public void Delete(OwnerDelayRecord t)
        {
            string sql = "DELETE FROM CBLprDelayRecord WHERE ID=@ID";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }

        public IEnumerable<OwnerDelayRecord> GetModels()
        {
            string sql = "SELECT * FROM CBLprDelayRecord";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<OwnerDelayRecord>(sql).ToList();
            }
        }

        public void Update(OwnerDelayRecord t)
        {
            string sql = @"UPDATE CBLprDelayRecord SET LprUserName=@LprUserName
,LprUserPlate=@LprUserPlate
,StartTime=@StartTime
,StopTime=@StopTime
,ChargeAmount=@ChargeAmount
,Operator=@Operator
,RecordTime=@RecordTime WHERE ID=@ID;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }
    }
}
