using SocialBrothersAPICase.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

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
            //sqlite_con.Close();
        }

        public static void CreateAddressTable()
        {
            if (sqlite_con != null)
            {

                string Createsql = "CREATE TABLE Adres (ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Straat VARCHAR(100) NOT NULL, " +
                    "Huisnummer INT NOT NULL, " +
                    "Toevoeging VARCHAR(1) NOT NULL, " +
                    "Postcode VARCHAR(10) NOT NULL, " +
                    "Plaats VARCHAR(100) NOT NULL, " +
                    "Land VARCHAR(60) NOT NULL);";
                sqlite_cmd = sqlite_con.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public static void CreateAddress(Address address)
        {
            if (sqlite_con != null)
            {
                sqlite_cmd = new SQLiteCommand();
                //sqlite_con.Open();
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
                //sqlite_con.Close();
            }
        }

        public static void UpdateAddress(int id, Address address)
        {
            sqlite_cmd = new SQLiteCommand();
            //sqlite_con.Open();
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
            //sqlite_con.Close();
        }

        public static void DeleteAddress(int id)
        {
            sqlite_cmd = new SQLiteCommand();
            //sqlite_con.Open();
            sqlite_cmd.Connection = sqlite_con;
            sqlite_cmd.CommandText = "delete from Adres where " +
                "ID = " + id + "";
            sqlite_cmd.ExecuteNonQuery();
            //sqlite_con.Close();
        }

        public static SQLiteDataReader ReadAll(Filters filters, string orderBy)
        {
            if (orderBy == null)
            {
                orderBy = "ID";
            }
            sqlite_cmd = new SQLiteCommand();
            //sqlite_con.Open();
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
    }
}
