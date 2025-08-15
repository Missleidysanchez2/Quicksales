using QUICKSALES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;
using QUICKSALES.Data;

namespace QUICKSALES.Repositorio
{
    public class SalesRepository
    {
        private SQLiteConnection _database;

        public SalesRepository()
        {
            _database = DependencyService.Get<ISQLiteDb>().GetConnection();
            _database.CreateTable<Sale>();            
        }
        public void UpdateSale(Sale venta)
        {
            _database.Update(venta);
        }
        public List<Sale> GetRecentSales(int count)
        {
            return _database.Table<Sale>().OrderByDescending(s => s.Fecha).Take(count).ToList();
        }

        public void DeleteSale(int saleId)
        {
            _database.Delete<Sale>(saleId);
        }

        public Sale GetSaleById(int saleId)
        {
            return _database.Table<Sale>().FirstOrDefault(s => s.Id == saleId);
        }

        public Sale GetLastSale()
        {
            return _database.Table<Sale>().OrderByDescending(v => v.Id).FirstOrDefault();
        }
        public void AddSale(Sale sale)
        {
            _database.Insert(sale);
        }

      
    }
}
