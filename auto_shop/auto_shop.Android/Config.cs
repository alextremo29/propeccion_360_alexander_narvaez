using System;
using System.IO;
using auto_shop.Droid;
using auto_shop.Interfaces;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Config))]
namespace auto_shop.Droid
{
    public class Config : IConfig
    {
        public SQLiteConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "AutoShop.db3");

            return new SQLiteConnection(path);
        }
    }
}