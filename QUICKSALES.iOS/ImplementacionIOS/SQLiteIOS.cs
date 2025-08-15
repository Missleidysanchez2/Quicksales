using System.IO;
using SQLite;
using Xamarin.Forms;
using QUICKSALES.iOS;
using QUICKSALES.Data;

[assembly: Dependency(typeof(SQLiteDbIOS))]
namespace QUICKSALES.iOS
{
    public class SQLiteDbIOS : ISQLiteDb
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "UserDatabase.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "..", "Library", dbName);
            System.Diagnostics.Debug.WriteLine($"Database path: {path}");
            return new SQLiteConnection(path);                                                                                                                                                       
        }
    }
}
