using System;
using DAL.Interface;
using Dapper;
using System.Collections.Generic;
using System.Collections;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using Model;

namespace DAL
{
    public class EnteranceRecordDAL : IEnteranceRecordDAL, IDependency
    {
        public int Add(EnteranceRecord t)
        {
            string sql = "INSERT INTO CBEnteranceRecrd(PlateNumber,EntranceTime,VehicleType) VALUES(@PlateNumber,@EntranceTime,@VehicleType); SELECT LAST_INSERT_ROWID(); ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql).FirstOrDefault();
            }
        }

        public void Delete(EnteranceRecord t)
        {
            string sql = "DELETE FROM CBEnteranceRecrd WHERE ID =@ID;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }

        public IEnumerable<EnteranceRecord> GetModels()
        {
            string sql = "SELECT * FROM CBEnteranceRecrd;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<EnteranceRecord>(sql).ToList();
            }
        }

        public void Update(EnteranceRecord t)
        {
            string sql = "UPDATE CBEnteranceRecrd SET PlateNumber=@PlateNumber,EntranceTime=@EntranceTime,VehicleType=@VehicleType WHERE ID = @ID";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql);
            }
        }

        public EnteranceRecord Query(string licensePlateNumber)
        {
            string sql = "SELECT * FROM CBEnteranceRecrd WHERE PlateNumber = @PlateNumber ;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Query<EnteranceRecord>(sql, new { PlateNumber = licensePlateNumber }).FirstOrDefault();
            }
        }
    }
}