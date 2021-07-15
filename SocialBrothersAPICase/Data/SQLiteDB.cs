using SocialBrothersAPICase.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialBrothersAPICase.Data
{
    public class SQLiteDB
    {
        private static SQLiteConnection sqlite_con;
        private static SQLiteCommand sqlite_cmd;
        public static void CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source= SBAPI.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            sqlite_con = sqlite_conn;
        }

        public static void CreateAddressTable()
        {
            if (sqlite_con != null)
            {
                string deletesql = "DROP TABLE IF EXISTS Adres;";
                sqlite_cmd = sqlite_con.CreateCommand();
                sqlite_cmd.CommandText = deletesql;
                sqlite_cmd.ExecuteNonQuery();
                string createsql = "CREATE TABLE Adres (ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Straat VARCHAR(100) NOT NULL, " +
                    "Huisnummer INT NOT NULL, " +
                    "Toevoeging VARCHAR(5) NOT NULL, " +
                    "Postcode VARCHAR(10) NOT NULL, " +
                    "Plaats VARCHAR(100) NOT NULL, " +
                    "Land VARCHAR(60) NOT NULL);";
                sqlite_cmd = sqlite_con.CreateCommand();
                sqlite_cmd.CommandText = createsql;
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public static void CreateAddress(Address address)
        {
            try
            {
                if (sqlite_con != null)
                {
                    sqlite_cmd = new SQLiteCommand();
                    sqlite_cmd.Connection = sqlite_con;
                    sqlite_cmd.CommandText = "insert into Adres(Straat," +
                        "Huisnummer," +
                        "Toevoeging," +
                        "Postcode," +
                        "Plaats," +
                        "Land) " +
                        "values ('" + address.Straat + "'," +
                        "'" + address.Huisnummer + "'," +
                        "'" + address.Toevoeging + "'," +
                        "'" + address.Postcode + "'," +
                        "'" + address.Plaats + "'," +
                        "'" + address.Land + "')";
                    sqlite_cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public static void UpdateAddress(int id, string newStraat, int? newHuisnummer, string newToevoeging, string newPostcode, string newPlaats, string newLand)
        {
            try
            {
                Address address = GetByID(id);
                address = ReplaceValues(address, newStraat, newHuisnummer, newToevoeging, newPostcode, newPlaats, newLand);
                sqlite_cmd = new SQLiteCommand();
                sqlite_cmd.Connection = sqlite_con;
                sqlite_cmd.CommandText = "update Adres set " +
                    "Straat='" + address.Straat + "'," +
                    "Huisnummer='" + address.Huisnummer + "'," +
                    "Toevoeging='" + address.Toevoeging + "'," +
                    "Postcode='" + address.Postcode + "'," +
                    "Plaats='" + address.Plaats + "'," +
                    "Land='" + address.Land + "'" +
                    " where " +
                    "ID = " + id + ";";
                sqlite_cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public static void DeleteAddress(int id)
        {
            try
            {

                sqlite_cmd = new SQLiteCommand();
                sqlite_cmd.Connection = sqlite_con;
                sqlite_cmd.CommandText = "delete from Adres where " +
                    "ID = " + id + "";
                sqlite_cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public static SQLiteDataReader ReadAll(Filters filters, string orderBy)
        {
            try
            {

                if (orderBy == null)
                {
                    orderBy = "ID";
                }
                sqlite_cmd = new SQLiteCommand();
                sqlite_cmd.Connection = sqlite_con;
                sqlite_cmd.CommandText = "select * from Adres " +
                    "where instr(Straat, '" + filters.Straat + "') > 0 " +
                    "and instr(Toevoeging, '" + filters.Toevoeging + "') > 0 " +
                    "and instr(Postcode, '" + filters.Postcode + "') > 0 " +
                    "and instr(Plaats, '" + filters.Plaats + "') > 0 " +
                    "and instr(Land, '" + filters.Land + "') > 0 " +
                    "and Huisnummer like '%" + filters.Huisnummer + "%' " +
                    "order by " + orderBy +
                    ";";
                SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
                return reader;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public static Address GetByID(int id)
        {
            sqlite_cmd = new SQLiteCommand();
            sqlite_cmd.Connection = sqlite_con;
            sqlite_cmd.CommandText = "select * from Adres where ID == " + id + ";";
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            if (reader.Read())
            {
                Address address = new Address
                {
                    Id = reader.GetFieldValue<Int64>(reader.GetOrdinal("ID")),
                    Straat = reader.GetFieldValue<string>(reader.GetOrdinal("Straat")),
                    Huisnummer = reader.GetFieldValue<int>(reader.GetOrdinal("Huisnummer")),
                    Toevoeging = reader.GetFieldValue<string>(reader.GetOrdinal("Toevoeging")),
                    Postcode = reader.GetFieldValue<string>(reader.GetOrdinal("Postcode")),
                    Plaats = reader.GetFieldValue<string>(reader.GetOrdinal("Plaats")),
                    Land = reader.GetFieldValue<string>(reader.GetOrdinal("Land"))
                };
                return address;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        private static Address ReplaceValues(Address address, string newStraat, int? newHuisnummer, string newToevoeging, string newPostcode, string newPlaats, string newLand)
        {
            try
            {

                if (newStraat != null)
                {
                    address.Straat = newStraat;
                }
                if (newHuisnummer != null)
                {
                    address.Huisnummer = (int)newHuisnummer;
                }
                if (newToevoeging.Equals("."))
                {
                    address.Toevoeging = null;
                }
                else if (newToevoeging != null)
                {
                    address.Toevoeging = newToevoeging;
                }
                if (newPostcode != null)
                {
                    address.Postcode = newPostcode;
                }
                if (newPlaats != null)
                {
                    address.Plaats = newPlaats;
                }
                if (newLand != null)
                {
                    address.Land = newLand;
                }
                return address;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
