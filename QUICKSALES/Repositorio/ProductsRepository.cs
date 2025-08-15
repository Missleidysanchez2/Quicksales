using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using QUICKSALES.Models;
using QUICKSALES.Data;
using System;

namespace QUICKSALES.Repositorio
{
    public class ProductsRepository
    {
        private SQLiteConnection _database;

        public ProductsRepository()
        {
            _database = DependencyService.Get<ISQLiteDb>().GetConnection();
            _database.CreateTable<Producto>();
        }
        public bool RestarCantidadProducto(int productoId, int cantidad)
        {
            try
            {
                var producto = _database.Table<Producto>().SingleOrDefault(p => p.Id == productoId);
                if (producto != null && producto.Stock >= cantidad)
                {
                    producto.Stock -= cantidad;
                    _database.Update(producto);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al restar cantidad de producto: {ex.Message}");
                return false;
            }
        }

        public void UpdateProduct(Producto producto)
        {
            _database.Update(producto);
        }

        public void DeleteProduct(int productId)
        {
            _database.Delete<Producto>(productId);
        }

        public IEnumerable<Producto> GetProducts()
        {
            return _database.Table<Producto>().ToList();
        }

        public void AddProduct(Producto product)
        {
            _database.Insert(product);
        }

        public Producto GetProductById(int productId)
        {
            return _database.Table<Producto>().FirstOrDefault(p => p.Id == productId);
        }
    }
}
