using QUICKSALES.Data;
using QUICKSALES.Models;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QUICKSALES.Repositorio
{
    class ClientsRepository
    {
        private SQLiteConnection _database;

        public ClientsRepository()
        {
            _database = DependencyService.Get<ISQLiteDb>().GetConnection();
            _database.CreateTable<Cliente>();
        }

        public void AgregarCliente(Cliente nuevoCliente)
        {
            _database.Insert(nuevoCliente);
        }

        //internal IEnumerable GetClients()
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<Cliente> GetClients()
        {
            return _database.Table<Cliente>().ToList();
        }



        //public void UpdateClient(Cliente client)
        //{
        //    _database.Update(client);
        //}

        //public void DeleteClient(Cliente client)
        //{
        //    _database.Delete(client);
        //}
    }
}
