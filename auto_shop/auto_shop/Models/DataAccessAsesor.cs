using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccessAsesor : IDisposable
    {
        private SQLiteConnection _connection;

        public DataAccessAsesor()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<User>();

        }

        public void InsertAsesor(User asesor)
        {
            _connection.Insert(asesor);
        }
        public List<User> GetAsesores()
        {
            return _connection.Query<User>("Select * from usuarios ");
        }
        public void DeleteAsesores()
        {
            _connection.Query<User>("delete from usuarios");
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
