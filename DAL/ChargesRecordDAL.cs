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
    public class ChargesRecordDAL : IChargesRecordDAL, IDependency
    {
        public int Add(ChargesRecord t)
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

        public void Delete(ChargesRecord t)
        {
            string sql = "DELETE FROM CBTempChargeRecord WHERE ID = @ID ;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public IEnumerable<ChargesRecord> GetModels()
        {
            string sql = "SELECT * FROM CBTempChargeRecord ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<ChargesRecord>(sql).ToList();
            }
        }

        public void Update(ChargesRecord t)
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

        public ChargesRecord Query(string licensePlateNumber)
        {
            string sql = "SELECT * FROM CBTempChargeRecord WHERE  PlateNumber =@PlateNumber  ORDER BY ID DESC LIMIT 1";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<ChargesRecord>(sql, new { PlateNumber = licensePlateNumber }).FirstOrDefault();
            }
        }

        public IEnumerable<ChargesRecord> Query(string licensePlateNumber, DateTime now)
        {
            string sql = "SELECT * FROM CBTempChargeRecord WHERE PlateNumber=@PlateNumber AND ExportTime > @ExportTime ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<ChargesRecord>(sql, new { PlateNumber = licensePlateNumber, ExportTime = now }).ToList();
            }
        }
    }
}