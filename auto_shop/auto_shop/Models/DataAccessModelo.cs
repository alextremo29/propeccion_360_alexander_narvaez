using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccessModelo : IDisposable
    {
        private SQLiteConnection _connection;

        public DataAccessModelo()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<ModeloModel>();

        }

        public void InsertModelo(ModeloModel modelo)
        {
            _connection.Insert(modelo);
        }
        public List<ModeloModel> GetModelos(int id_marca)
        {
            return _connection.Query<ModeloModel>("Select * from modelos where id_marca = ?", id_marca);
        }
        public void DeleteModelos()
        {
            _connection.Query<MarcasModel>("delete from modelos");
        }
        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
