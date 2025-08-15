using QUICKSALES.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace QUICKSALES.Data
{
    public class DataBase
    {
        private readonly SQLiteConnection _database;

        public DataBase(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
        }

        public SQLiteConnection GetConnection()
        {
            return _database;
        }
    }
}
