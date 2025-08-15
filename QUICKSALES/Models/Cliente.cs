using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QUICKSALES.Models
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
