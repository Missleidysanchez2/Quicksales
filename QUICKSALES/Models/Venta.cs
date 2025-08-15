using System;
using System.Collections.Generic;
using System.Text;

namespace QUICKSALES.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public List<Producto> ProductosVendidos { get; set; }
        public decimal Total { get; set; }
    }
}

