using Newtonsoft.Json;
using Pedidos.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pedidos.Pages
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
              new Thickness(3, 20, 3, 3),
              new Thickness(3, 10, 3, 3),
              new Thickness(3, 10, 3, 3));

            enterButton.Clicked += enterButton_Clicked;
        }

        private async void enterButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text))
            {
                await DisplayAlert("Error", "Deve inserir um usuário", "Ok");
                userEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Deve inserir uma senha", "Ok");
                passwordEntry.Focus();
                return;
            }

            waitActivityIndicator.IsRunning = true;
            enterButton.IsEnabled = false;
            string result = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080");
                string url = string.Format("/{0}/{1}", userEntry.Text, passwordEntry.Text);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;
                waitActivityIndicator.IsRunning = false;

                if (string.IsNullOrEmpty(result))
                {
                    await DisplayAlert("Error", "Sem resposta do servidor", "Ok");
                    waitActivityIndicator.IsRunning = false;
                    enterButton.IsEnabled = true;
                    return;
                }

            } catch (Exception ex)
            {
                await DisplayAlert("Error", string.Format("Ocorreu o erro: {0}", ex.Message), "Ok");
                waitActivityIndicator.IsRunning = false;
                enterButton.IsEnabled = true;
                return;
            }

            waitActivityIndicator.IsRunning = false;
            enterButton.IsEnabled = true;
            if (result == null)
            {
                await DisplayAlert("Error", "Usuário ou senha incorreta", "Ok");
                passwordEntry.Text = string.Empty;
                userEntry.Focus();
                return;
            }

            var deviceUser = JsonConvert.DeserializeObject<DeviceUser>(result);
            if (rememberMeSwitch.IsToggled)
            {
                using (var datas = new DataAcess()) {
                    datas.InsertDeviceUser(deviceUser);
                }
            }

            await Navigation.PushAsync(new MenuPage(deviceUser));
        }
    }
}
