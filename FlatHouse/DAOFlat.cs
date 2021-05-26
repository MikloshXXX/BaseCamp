using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace FlatHouse
{
    public class DAOFlat : DAO<Flat>
    {
        private MySqlCommand cmd;
        private DatabaseConnection dbConnection;
        public DAOFlat()
        {
            dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();
        }
        public bool Delete(int id)
        {
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
            string sql = "DELETE FROM `flats` WHERE `apartmentNumber` = @aptNum";
            cmd.CommandText = sql;
            MySqlParameter aptNum = new MySqlParameter("@aptNum", MySqlDbType.Int32);
            aptNum.Value = id;
            cmd.Parameters.Add(aptNum);
            cmd.ExecuteNonQuery();
            return true;
        }

        public List<Flat> Get()
        {
            List<Flat> flats = new List<Flat>();
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
            string sql = "SELECT `apartmentNumber`, `floorArea`, `isOccupied`, `residentName` FROM `flats`";
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int flatNumberIndex = reader.GetOrdinal("apartmentNumber");
                    int flatNumber = reader.GetInt32(flatNumberIndex);

                    int floorAreaIndex = reader.GetOrdinal("floorArea");
                    float floorArea = reader.GetFloat(floorAreaIndex);

                    int isOccupiedIndex = reader.GetOrdinal("isOccupied");
                    bool isOccupied = reader.GetBoolean(isOccupiedIndex);

                    int residentNameIndex = reader.GetOrdinal("residentName");
                    string residentName = reader.GetString(residentNameIndex);

                    if (residentName == string.Empty)
                    {
                        flats.Add(new Flat(flatNumber, floorArea));
                    }
                    else
                    {
                        flats.Add(new Flat(flatNumber, floorArea, residentName));
                    }
                }
                return flats;
            }
        }

        public Flat Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Flat entity)
        {
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
            string sql = "INSERT INTO `flats`(`apartmentNumber`, `floorArea`, `isOccupied`, `residentName`) VALUES (@apartmentNumber,@floorArea,@isOccupied,@residentName)";
            cmd.CommandText = sql;
            MySqlParameter aptNum = new MySqlParameter("@apartmentNumber", MySqlDbType.Int32);
            aptNum.Value = entity.ApartmentNumber;
            cmd.Parameters.Add(aptNum);
            MySqlParameter flArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
            flArea.Value = entity.FloorArea;
            cmd.Parameters.Add(flArea);
            MySqlParameter isOc = new MySqlParameter("@isOccupied", MySqlDbType.Bool);
            isOc.Value = entity.IsOccupied;
            cmd.Parameters.Add(isOc);
            MySqlParameter resNm = new MySqlParameter("@residentName", MySqlDbType.VarChar);
            resNm.Value = entity.ResidentName;
            cmd.Parameters.Add(resNm);
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool Update(int id, Flat entity)
        {
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
            string sql = "UPDATE `flats` SET " +
                "`floorArea` = @floorArea, `isOccupied` = @isOccupied, `residentName` = @residentName WHERE `apartmentNumber` = @aptNum";
            cmd.CommandText = sql;
            MySqlParameter aptNum = new MySqlParameter("@aptNum", MySqlDbType.Int32);
            aptNum.Value = entity.ApartmentNumber;
            MySqlParameter floorArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
            floorArea.Value = entity.FloorArea;
            MySqlParameter isOccupied = new MySqlParameter("@isOccupied", MySqlDbType.Bit);
            isOccupied.Value = entity.IsOccupied;
            MySqlParameter residentName = new MySqlParameter("@residentName", MySqlDbType.VarChar);
            residentName.Value = entity.ResidentName;
            cmd.Parameters.Add(aptNum);
            cmd.Parameters.Add(floorArea);
            cmd.Parameters.Add(isOccupied);
            cmd.Parameters.Add(residentName);
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}
