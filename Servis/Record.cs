using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgama.Servis
{

    public class DatabaseManager
    {
        private readonly string _dbPath;

        public DatabaseManager(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void InitializeDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Удаление таблицы, если она существует
                string dropTableQuery = "DROP TABLE IF EXISTS Records;";
                using (SQLiteCommand dropCmd = new SQLiteCommand(dropTableQuery, conn))
                {
                    dropCmd.ExecuteNonQuery();
                }

                // Создание таблицы заново
                string createTableQuery = @"
                CREATE TABLE Records (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    PhoneNumber TEXT NOT NULL,
                    Color TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Master TEXT NOT NULL,
                    Date TEXT NOT NULL DEFAULT (datetime('now', 'localtime')) 
                );";

                using (SQLiteCommand createCmd = new SQLiteCommand(createTableQuery, conn))
                {
                    createCmd.ExecuteNonQuery();
                }
            }
        }
    }
    public class Record
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Master { get; set; }
        public string Date { get; set; } // Поле для хранения даты
    }
}
