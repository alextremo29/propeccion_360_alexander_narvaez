using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Interfaces
{
    public interface IConfig
    {
        SQLiteConnection GetConnection();
        
    }
}
