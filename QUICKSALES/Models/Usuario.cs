using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QUICKSALES.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contreseña { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }

    }
    

   
}



