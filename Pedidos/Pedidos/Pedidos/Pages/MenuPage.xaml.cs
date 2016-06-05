using Pedidos.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Pedidos.Pages
{
    public partial class MenuPage : ContentPage
    {
        private DeviceUser deviceUser;
        public MenuPage(DeviceUser deviceUser)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
                new Thickness(3, 20, 3, 3),
                new Thickness(3,10, 3, 3),
                new Thickness(3, 10, 3, 3));

            this.deviceUser = deviceUser;
            welcomeLabel.Text = string.Format("Bem vindo {0} {1}", deviceUser.FirstName, deviceUser.LastName);
            closeSessionButton.Clicked += closeSessionButton_Clicked;
        }

        private async void closeSessionButton_Clicked(object sender, EventArgs e)
        {
            using (var datas = new DataAcess())
            {
                datas.DeleteDeviceUser(this.deviceUser);
            }

            await Navigation.PushAsync(new Login());
        }
    }
}
