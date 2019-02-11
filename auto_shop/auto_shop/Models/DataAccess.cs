using System;
using System.Collections.Generic;
using auto_shop.Interfaces;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;

namespace auto_shop.Models
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection _connection;
        
        public DataAccess()
        {
            _connection = DependencyService.Get<IConfig>().GetConnection();
            _connection.CreateTable<Prospecto>();

        }
        
        public void InsertProspecto(Prospecto prospecto)
        {
            _connection.Insert(prospecto);
        }
        public void UpdateProspectoSincronizado (int idserial)
        {
            _connection.Query<Prospecto>("update prospectos set sincronizado = ? where idserial = ?",true, idserial);
        }
        public void DeleteProspecto (Prospecto prospecto)
        {
            _connection.Delete(prospecto);
        }
        public Prospecto GetProspecto (int idserial)
        {
            return _connection.Table<Prospecto>().FirstOrDefault(c=>c.idserial==idserial);
        }
        public List<Prospecto> GetProspectos()
        {
            return _connection.Table<Prospecto>().OrderBy(c => c.nombre).ToList();
        }
        public List<Prospecto> GetSincronizarProspectos()
        {
            return _connection.Query<Prospecto>("Select * from prospectos where sincronizado = ?",false);
        }
        public void UpdateProspecto (Prospecto prospecto)
        {
            _connection.Update(prospecto);
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
        
    }
}
