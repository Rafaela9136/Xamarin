using Pedidos.Droid;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Iterop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Pedidos.Droid.Config))]

namespace Pedidos.Droid
{
    class Config : Pedidos.Interfaces.IConfig
    {
        public Config() //Caso dê erro aqui, remove esses parenteses
        {
            private string directorioDB;
            private ISQLPlatform plataforma;
            
            public string DirectorioDB
            {
                get
                {
                    if(string.IsNullOrEmpty(directorioDB))
                    {
                        directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    }
                    return directorioDB;
                }
            }
            
            public ISQLPlatform Plataforma
            {
                get
                {
                    if(plataforma == null) 
                    {
                        plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                    }
                    return plataforma;
                }
            }
        }   
    }
}