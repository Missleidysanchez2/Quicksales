using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QUICKSALES.Models
{
    public class Factura
    {
        public string Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        // public decimal Total => Productos.Sum(p => p.Precio);
        public int NumeroFactura { get; set; }
        public decimal Subtotal => Productos.Sum(p => p.Precio);
        public decimal IVA => Subtotal * 0.15m; // Tasa de IVA del 15%
        public decimal Total => Subtotal + IVA;
    }

}
