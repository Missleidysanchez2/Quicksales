using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace QUICKSALES.Models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }

        private string productosVendidosJson;

        [Ignore]
        public List<Producto> ProductosVendidos
        {
            get => string.IsNullOrEmpty(productosVendidosJson) ? new List<Producto>() : JsonConvert.DeserializeObject<List<Producto>>(productosVendidosJson);
            set => productosVendidosJson = value != null ? JsonConvert.SerializeObject(value) : null;
        }
       
        // Otros campos y propiedades de la venta...

        public void AgregarProductos(List<Producto> productos)
        {
            ProductosVendidos.AddRange(productos);
            // Aquí puedes actualizar cualquier otra información de la venta según sea necesario
        }
        public decimal Total { get; set; }
    }
}
