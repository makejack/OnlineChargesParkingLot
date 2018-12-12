using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using DAL.Interface;
using Dapper;
using Model;

namespace DAL
{
    public class ChargeRecordDAL : IChargeRecordDAL, IDependency
    {
        public int Add(ChargeRecord t)
        {
            string sql = @"insert into CBTempChargeRecord(CardNumber
            ,PlateNumber
            ,EntranceTime
            ,ExportTime
            ,ChargeAmount
            ,ManageName
            ,ActualAmount
            ,ExitNumber
            ,VehicleType
            ,FreeType
            ,FreeAmount) 
            VALUES(@CardNumber
            ,@PlateNumber
            ,@EntranceTime
            ,@ExportTime
            ,@ChargeAmount
            ,@ManageName
            ,@ActualAmount
            ,@ExitNumber
            ,@VehicleType
            ,@FreeType
            ,@FreeAmount); select last_insert_rowid(); ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql, t).FirstOrDefault();
            }
        }

        public void Delete(ChargeRecord t)
        {
            string sql = "DELETE FROM CBTempChargeRecord WHERE ID = @ID ;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public IEnumerable<ChargeRecord> GetModels()
        {
            string sql = "SELECT * FROM CBTempChargeRecord ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<ChargeRecord>(sql).ToList();
            }
        }

        public void Update(ChargeRecord t)
        {
            string sql = @"UPDATE CBTempChargeRecord SET CardNumber= @CardNumber
            ,PlateNumber=@PlateNumber
            ,EntranceTime= @EntranceTime
            ,ExportTime= @ExportTime
            ,ChargeAmount= @ChargeAmount
            ,ManageName= @ManageName
            ,ActualAmount= @ActualAmount
            ,ExitNumber= @ExitNumber
            ,FreeType= @FreeType
            ,FreeAmount= @FreeAmount 
            WHERE ID=@ID ;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public int Query(string licensePlateNumber)
        {
            string sql = "SELECT VehicleType FROM CBTempChargeRecord WHERE  PlateNumber =@PlateNumber  ORDER BY ID DESC LIMIT 1";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql, new { PlateNumber = licensePlateNumber }).FirstOrDefault();
            }
        }
    }
}