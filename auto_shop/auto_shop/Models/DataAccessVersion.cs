using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccessVersion : IDisposable
    {
        private SQLiteConnection _connection;

        public DataAccessVersion()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<VersionModel>();

        }

        public void InserVersion(VersionModel version)
        {
            _connection.Insert(version);
        }
        public List<VersionModel> Getversiones(int id_modelo)
        {
            return _connection.Query<VersionModel>("Select * from version where id_modelo = ?", id_modelo);
        }
        public void DeleteVersiones()
        {
            _connection.Query<MarcasModel>("delete from version");
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
