using System;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Pedidos.Droid.Config))]

namespace Pedidos.Droid
{
    public class Config : Interface.IConfig
    {
        private string _diretorioBD;
        private ISQLitePlatform plataforma;

        public string DirectorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioBD))
                {
                    _diretorioBD = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                return _diretorioBD;
            }
        }

        public ISQLitePlatform Platforma
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return plataforma;
            }
        }
    }
}