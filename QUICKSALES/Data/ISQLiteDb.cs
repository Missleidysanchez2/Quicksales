using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QUICKSALES.Data
{
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}
