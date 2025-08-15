using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
namespace QUICKSALES.Models
{
    public class Producto
    {
        internal string ImagePath;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NombreP { get; set; }
        public string Talla { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int Cantidad { get; internal set; }
    }
}
