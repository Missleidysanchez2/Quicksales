using SQLite;
using QUICKSALES.Models;
using QUICKSALES.Data;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace QUICKSALES.Repositorio
{
    public class UserRepository
    {
        private SQLiteConnection _database;

        public UserRepository()
        {
            _database = DependencyService.Get<ISQLiteDb>().GetConnection();
            _database.CreateTable<Usuario>();
        }

        public IEnumerable<Usuario> GetUsers() => _database.Table<Usuario>().ToList();

        public Usuario GetUserByUsername(string username) =>
            _database.Table<Usuario>().FirstOrDefault(u => u.Nombre == username);

        public int SaveUser(Usuario user) => _database.Insert(user);
    }
}