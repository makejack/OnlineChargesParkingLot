using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Interface;
using Model;
using Dapper;

namespace DAL
{
    public class ParkingLotInfoDAL : IParkingLotInfoDAL, IDependency
    {
        public int Add(ParkingLotInfo t)
        {
            string sql = @"INSERT INTO CBParkingLotInfo(ParkingName
            , Description
            , RentChargeAmount
            , ChargeMode
            , OpenMode
            , FreeMinutes
            , DailyLimit
            , FirstCharge
            , FirstMoney
            , TwoCharge
            , TwoMoney
            , ThreeCharge
            , ThreeMoney
            , FourCharge
            , FourMoney
            , FiveCharge
            , FiveMoney
            , SixCharge
            , SixMoney
            , SevenCharge
            , SevenMoney
            , EightCharge
            , EightMoney
            , NineCharge
            , NineMoney
            , TenCharge
            , TenMoney) VALUES (@ParkingName
            , @Description
            , @RentChargeAmount
            , @ChargeMode
            , @OpenMode
            , @FreeMinutes
            , @DailyLimit
            , @FirstCharge
            , @FirstMoney
            , @TwoCharge
            , @TwoMoney
            , @ThreeCharge
            , @ThreeMoney
            , @FourCharge
            , @FourMoney
            , @FiveCharge
            , @FiveMoney
            , @SixCharge
            , @SixMoney
            , @SevenCharge
            , @SevenMoney
            , @EightCharge
            , @EightMoney
            , @NineCharge
            , @NineMoney
            , @TenCharge
            , @TenMoney); select LAST_INSERT_ROWID(); ";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<int>(sql, t).First();
            }
        }

        public void Delete(ParkingLotInfo t)
        {
            string sql = "DELETE FROM CBParkingLotInfo WHERE ID =@ID";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }

        public IEnumerable<ParkingLotInfo> GetModels()
        {
            string sql = "SELECT * FROM CBParkingLotInfo";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                return connection.Query<ParkingLotInfo>(sql).ToList();
            }
        }

        public void Update(ParkingLotInfo t)
        {
            string sql = @"UPDATE CBParkingLotInfo SET ParkingName = @ParkingName
            , Description = @Description
            , RentChargeAmount = @RentChargeAmount
            , ChargeMode = @ChargeMode
            , OpenMode = @OpenMode
            , FreeMinutes = @FreeMinutes
            , DailyLimit = @DailyLimit
            , FirstCharge = @FirstCharge
            , FirstMoney = @FirstMoney
            , TwoCharge = @TwoCharge
            , TwoMoney = @TwoMoney
            , ThreeCharge = @ThreeCharge
            , ThreeMoney = @ThreeMoney
            , FourCharge = @FourCharge
            , FourMoney = @FourMoney
            , FiveCharge = @FiveCharge
            , FiveMoney = @FiveMoney
            , SixCharge = @SixCharge
            , SixMoney = @SixMoney
            , SevenCharge = @SevenCharge
            , SevenMoney = @SevenMoney
            , EightCharge = @EightCharge
            , EightMoney = @EightMoney
            , NineCharge = @NineCharge
            , NineMoney = @NineMoney
            , TenCharge = @TenCharge
            , TenMoney = @TenMoney WHERE ID = @ID;";
            using (IDbConnection connection = DbConnectionFactory.Create())
            {
                connection.Execute(sql, t);
            }
        }
    }
}