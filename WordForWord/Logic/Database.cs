using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordForWord.Logic
{
    class Database
    {
        private SQLiteConnection m_dbConnection;

        public Database()
        {
            //SQLiteConnection.CreateFile("WordDB.sqlite");            
            m_dbConnection = new SQLiteConnection("Data Source=WordDB.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public int GetParts(string str)
        {
            //int i = 0;
            string sql = "select count(word) from words where words.word like '" + str +"%'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            int RowCount = 0;

            RowCount = Convert.ToInt32(command.ExecuteScalar());

            //while (reader.Read())
            //Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            //i++;
            return RowCount;
        }

        public bool GetWord(string str)
        {
            string sql = "select count(word) from words where words.word like '" + str + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            int RowCount = 0;

            RowCount = Convert.ToInt32(command.ExecuteScalar());

            if (RowCount == 1)
            {
                return true;
            }
            return false;
        }

        private void CreateBDAndAddWords()
        {
            SQLiteConnection.CreateFile("WordDB.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=WordDB.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "CREATE TABLE words (word VARCHAR(54))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            var lines = System.IO.File.ReadAllLines(@"C:\Users\max\documents\visual studio 2015\Projects\SlovoZaSlovo\SlovoZaSlovo\Res\lop2v2.txt");
            //string[] lines = new string[] { "село", "гол", "лаг", "стог", "вол", "гот", "сел", "сало", "селова", "лесть" };

            foreach (var line in lines)
            {
                sql = "insert into words (word) values ('" + line + "')";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }

        }
    }

}
