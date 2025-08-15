using System;
using System.IO;
using QuickSales.Droid;
using QUICKSALES.Data;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDbA))]
namespace QuickSales.Droid
{
    public class SQLiteDbA : ISQLiteDb
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "QuickSales.db3";
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}
