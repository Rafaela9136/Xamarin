using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Pedidos.Pages;

namespace Pedidos
{
    public class App : Application
    {
        public App()
        {
            using (var datas = new DataAcess())
            {
                var deviceUser = datas.GetDeviceUsers().FirstOrDefault();
                if (deviceUser != null)
                {
                    MainPage = new NavigationPage(new MenuPage(deviceUser));
                } else {
                    MainPage = new NavigationPage(new Login());
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
