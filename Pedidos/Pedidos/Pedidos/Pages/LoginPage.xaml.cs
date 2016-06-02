using System;
using System.Collection.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Pedidos
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
                new Thickness(3, 20, 3, 3),
                new Thickness(3, 10, 3, 3),
                new Thickness(3, 10, 3, 3));
                
            enterButton.Clicked += enterButton_Clicked;
            
            private async void enterButton_Clicked(object sender, EventArgs e)
            {
                if(string.IsNullOrEmpty(userEntry.Text))
                {
                    await DisplayAlert("Error", "Deve inserir um usuário", "Ok");
                    userEntry.Focus();
                    return;
                }
                
                if(string.IsNullOrEmpty(passwordEntry.Text))
                {
                    await DisplayAlert("Error", "Deve inserir uma senha", "Ok");
                    passwordEntry.Focus();
                    return;
                }
                
                waitActivityIndicator.IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080");
                string url = string.Format("/{ip}", anoStepper.Value);
                
            }
        }
    }
}