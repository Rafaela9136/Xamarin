using System;
using System.Collection.Generic;
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
        }
    }
}