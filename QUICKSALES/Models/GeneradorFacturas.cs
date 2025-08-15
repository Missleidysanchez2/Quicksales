using System;
using System.Collections.Generic;
using System.Text;

namespace QUICKSALES.Models
{
    public class GeneradorFacturas
    {
        public Factura GenerarFactura(string cliente, List<Producto> productos)
        {
            return new Factura
            {
                Cliente = cliente,
                Productos = productos
            };
        }
    }
}
