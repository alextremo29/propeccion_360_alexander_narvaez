using Xamarin.Forms;
using auto_shop.Interfaces;
using auto_shop.iOS;
using SQLite;
using System;
using System.IO;

[assembly: Dependency(typeof(Config))]
namespace auto_shop.iOS
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