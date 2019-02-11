using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccessDetalleVersion : IDisposable
    {
        private SQLiteConnection _connection;

        public DataAccessDetalleVersion()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<DetalleVersionModel>();

        }

        public void InsertDetalle(DetalleVersionModel detalle)
        {
            _connection.Insert(detalle);
        }
        public List<DetalleVersionModel> GetDetalles(int id_version)
        {
            return _connection.Query<DetalleVersionModel>("Select * from detalleVersion where id_version = ?", id_version);
        }
        public void DeleteDetalles()
        {
            _connection.Query<DetalleVersionModel>("delete from detalleVersion");
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
