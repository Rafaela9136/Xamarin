using Pedidos.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Iterop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Pedidos.iOS.Config))]

namespace Pedidos.iOS
{
    class Config : Pedidos.Interfaces.IConfig
    {
        public Config 
        {
            private string directorioDB;
            private IQSLitePlatform platforma;
            
            public string DirectorioDB
            {
                get
                {
                    if(string.IsNullOrEmpty(directorioDB))
                    {
                        var directorio = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        directorioDB = System.IO.Path.Combine(directorio, "..", "Library");
                    }
                    return directorioDB;
                }
            }
            
            public ISQLitePlatform Plataforma
            {
                get
                {
                    if(plataforma == null)
                    {
                        plataforma = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                    }
                    return plataforma;
                }
            }
        }  
    }
}
