using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccessMarca : IDisposable
    {
        private SQLiteConnection _connection;

        public DataAccessMarca()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<MarcasModel>();

        }

        public void InsertMarca(MarcasModel marca)
        {
            _connection.Insert(marca);
        }
        public List<MarcasModel> GetMarcas()
        {
            //return _connection.Table<MarcasModel>().OrderBy(c => c.descripcion).ToList();
            return _connection.Query<MarcasModel>("Select idserial, descripcion from marcas ");
        }
        public  void DeleteMarca ()
        {
            _connection.Query<MarcasModel>("delete from marcas");
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
