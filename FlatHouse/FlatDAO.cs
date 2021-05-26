using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace FlatHouse
{
    public class FlatDAO : DAO<Flat>
    {
        private DatabaseConnection dbConnection;
        private MySqlCommand cmd;
        public FlatDAO()
        {
            dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
        }
        public bool Delete(int id)
        {
            cmd = new MySqlCommand();
            cmd.Connection = dbConnection.GetConnection();
            string sql = "DELETE FROM flats WHERE flatNumber = @id";
            cmd.CommandText = sql;
            MySqlParameter flatNumber = new MySqlParameter("@id", MySqlDbType.Int32);
            flatNumber.Value = id;
            cmd.Parameters.Add(flatNumber);
            cmd.ExecuteNonQuery();
            return true;
        }

        public List<Flat> Get()
        {
            List<Flat> flats = new List<Flat>();
            try
            {
                cmd = new MySqlCommand();
                cmd.Connection = dbConnection.GetConnection();
                string sql = "SELECT * FROM flats";
                cmd.CommandText = sql;
                using(DbDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int flatNumberIndex = reader.GetOrdinal("flatNumber");
                        int flatNumber = reader.GetInt32(flatNumberIndex);

                        int floorAreaIndex = reader.GetOrdinal("floorArea");
                        float floorArea = reader.GetFloat(floorAreaIndex);

                        int isOccupiedIndex = reader.GetOrdinal("isOccupied");
                        bool isOccupied = reader.GetBoolean(isOccupiedIndex);

                        int residentNameIndex = reader.GetOrdinal("residentName");
                        string residentName = reader.GetString(residentNameIndex);

                        if (residentName == null)
                        {
                            flats.Add(new Flat(flatNumber, floorArea));
                        }
                        else
                        {
                            flats.Add(new Flat(flatNumber, floorArea, residentName));
                        }
                    }
                }
            }
            catch (Exception)
            {
                //TODO
            }
            return flats;
        }

        public Flat Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Flat entity)
        {
            try
            {
                cmd = new MySqlCommand();
                cmd.Connection = dbConnection.GetConnection();
                string sql = "INSERT INTO `flats`(`flatNumber`, `floorArea`, `isOccupied`, `residentName`) VALUES (@flatNumber,@floorArea,@isOccupied,@residentName";
                cmd.CommandText = sql;
                MySqlParameter flatNumber = new MySqlParameter("@flatNumber", MySqlDbType.Int32);
                flatNumber.Value = entity.FlatNumber;
                MySqlParameter floorArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
                floorArea.Value = entity.FloorArea;
                MySqlParameter isOccupied = new MySqlParameter("@isOccupied", MySqlDbType.Bit);
                isOccupied.Value = entity.IsOccupied;
                MySqlParameter residentName = entity.ResidentName == null ? new MySqlParameter("@residentName", MySqlDbType.Null): new MySqlParameter("@residentName", MySqlDbType.VarChar);
                residentName.Value = entity.ResidentName;
                cmd.Parameters.Add(flatNumber);
                cmd.Parameters.Add(floorArea);
                cmd.Parameters.Add(isOccupied);
                cmd.Parameters.Add(residentName);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(int id, Flat entity)
        {
            try
            {
                cmd = new MySqlCommand();
                cmd.Connection = dbConnection.GetConnection();
                string sql = "UPDATE flats SET " +
                    "flatNumber = @flatNumber, floorArea = @floorArea, isOccupied = @isOccupied, residentName = @residentName";
                cmd.CommandText = sql;
                MySqlParameter flatNumber = new MySqlParameter("@flatNumber", MySqlDbType.Int32);
                flatNumber.Value = entity.FlatNumber;
                MySqlParameter floorArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
                floorArea.Value = entity.FloorArea;
                MySqlParameter isOccupied = new MySqlParameter("@isOccupied", MySqlDbType.Bit);
                isOccupied.Value = entity.IsOccupied;
                MySqlParameter residentName = new MySqlParameter("@residentName", MySqlDbType.VarChar);
                residentName.Value = entity.ResidentName;
                cmd.Parameters.Add(flatNumber);
                cmd.Parameters.Add(floorArea);
                cmd.Parameters.Add(isOccupied);
                cmd.Parameters.Add(residentName);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
