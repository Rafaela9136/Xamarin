using Pedidos.models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace Pedidos.Pages
{
    public partial class MenuPage : ContentPage
    {
        private DeviceUser deviceUser;
        private List<models.Product> products;
        private List<models.Customer> customers;
        private List<models.SaleDetailTmp> details;

        public MenuPage(DeviceUser deviceUser)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
                new Thickness(3, 20, 3, 3),
                new Thickness(3,10, 3, 3),
                new Thickness(3, 10, 3, 3));

            this.deviceUser = deviceUser;
            welcomeLabel.Text = string.Format("Bem vindo {0} {1}", deviceUser.FirstName, deviceUser.LastName);

            this.LoadProducts();
            this.LoadCustomers();
            this.LoadDetail();

            closeSessionButton.Clicked += closeSessionButton_Clicked;
            addButton.Clicked += addButton_Clicked;
            newButton.Clicked += newButton_Clicked;
        }
        
        private async void newButton_Clicked(object sender, EventArgs e)
        {
            if(customerPicker.SelectedIndex == -1) {
                await DisplayAlert("Error", "Deve selecionar um cliente", "Ok");
                customerPicker.Focus();
                return;
            }
            
            if(details.Count() == 0) {
                await DisplayAlert("Error", "Deve adiconar, pelo menos, um produto", "Ok");
                productPicker.Focus();
                return;
            }
            
            var sale = new Sale{
                CustomerId = customers[customerPicker.SelectedIndex].CustomerID,
                DateSale = DateTime.Now,
                saved = false
            };
            
            bool wasSaved = true;
            
            waitActivityIndicator.IsRunning = true;
            newButton.IsEnabled = false;
            string result = string.Empty;
            
            var jsonRequest = JsonConvert.SerializeObject(sale);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
            
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080");
                string url = string.Format("/SaleAPI");
                var response = await client.PostAsync(url, content);
                result = response.Content.ReadAsStringAsync().Result;
                waitActivityIndicator.IsRunning = false;

                if (string.IsNullOrEmpty(result))
                {
                    await DisplayAlert("Error", "Sem resposta do servidor", "Ok");
                    waitActivityIndicator.IsRunning = false;
                    wasSaved = false;
                }

            } catch (Exception ex) {
                await DisplayAlert("Error", string.Format("Ocorreu o erro: {0}", ex.Message), "Ok");
                waitActivityIndicator.IsRunning = false;
                newButton.IsEnabled = true;
                wasSaved = false;
            }
            
            if (result == null || string.IsNullOrEmpty(result)) {
                await DisplayAlert("Error", "Sem resposta do servidor", "Ok");
                waitActivityIndicator.IsRunning = false;
                newButton.IsEnabled = true;
                wasSaved = false;
            }
            
            if(wasSaved) {
                sale = JsonConvert.DeserializeObject<Sale>(result);
                foreach (var detail in details) {
                    var detailToSave = new SaleDetail {
                        Description = detail.Description,
                        Price = detail.Price,
                        ProductID = detail.ProductID,
                        Quantity = detail.Quantity,
                        SaleID = sale.SaleID
                    };   
                    
                    var jsonRequest1 = JsonConvert.SerializeObject(detailToSave);
                    var content1 = new StringContent(jsonRequest1, Encoding.UTF8, "text/json");
            
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("http://localhost:8080");
                        string url = string.Format("/SaleAPI");
                        var response = await client.PostAsync(url, content1);
                        result = response.Content.ReadAsStringAsync().Result;
                    } catch (Exception) {

                    }
               }
           } else {
                using(var data = new DataAcess()) {
                    data.InsertSale(sale);
                    int saleID = data.GetSale().Select(s => s.SaleID).Max();
                    foreach (var detail in details) {
                        var saleDetail = new SaleDetail {
                            Description = detail.Description,
                            Price = detail.Price,
                            ProductID = detail.ProductID,
                            Quantity = detail.Quantity,
                            SaleID = saleID
                        };
                        data.InsertSaleDetail(saleDetail);
                        data.DeleteAllSaleDetailTmp();
                    }
                }
           }
           
           customerPicker.SelectedIndex = -1;
           details.Clear();
           detailsListView.ItemsSource = null;
           detailsListView.ItemsSource = details;
        }
        
        private async void addButton_Clicked(object sender, EventArgs e)
        {
            if(productPicker.SelectedIndex == -1) {
                await DisplayAlert("Error", "Deve selecionar um produto", "Ok");
                productPicker.Focus();
                return;
            }
            
            if(string.IsNullOrEmpty(quantityEntry.Text)) {
                await DisplayAlert("Error", "Deve inserir uma quantidade", "Ok");
                quantityEntry.Focus();
                return;
            }
            
            var quantity = float.Parse(quantityEntry.Text);
            
            if(quantity <= 0) {
                await DisplayAlert("Error", "Deve inserir uma quantidade maior que zero", "Ok");
                quantityEntry.Focus();
                return;
            }

            var saleDetail = new SaleDetailTmp
            {
                Description = products[productPicker.SelectedIndex].Description,
                Price = products[productPicker.SelectedIndex].Price,
                ProductID = products[productPicker.SelectedIndex].ProductID,
                Quantity = quantity
            };
            
            using(var data = new DataAcess())
            {
                var detailSaved = data.GetSaleDetailTmp(saleDetail.ProductID);
                if(detailSaved == null) {
                    data.InsertSaleDetailTmp(saleDetail);
                } else {
                    detailSaved.Quantity += saleDetail.Quantity;
                    data.UpdateSaleDetailTmp(detailSaved);
                }
                
                details = data.GetSaleDetailTmp();
                detailsListView.ItemsSource = details;
            }
            
            productPicker.SelectedIndex = -1;
            quantityEntry.Text = string.Empty;
        }

        private void LoadDetail()
        {
            using(var data = new DataAcess())
            {
                details = data.GetSaleDetailTmp();
            }
	        detailsListView.ItemsSource = details;
        }
        
        private async void LoadCustomers()
        {
            waitActivityIndicator.IsRunning = true;
            string result = string.Empty;
            bool wasLoad = true;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080");
                string url = string.Format("");
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(result))
                {
                    wasLoad = false;
                }

            }
            catch (Exception)
            {
                wasLoad = false;
            }

            waitActivityIndicator.IsRunning = false;
            if (wasLoad)
            {
                if (result != null)
                {
                    this.customers = JsonConvert.DeserializeObject<List<Customer>>(result);
                    // Salvar Localmente
                    this.SaveCustomers();
                }
            }
            else
            {
                using (var datas = new DataAcess())
                {
                    this.customers = datas.GetCustomers();
                }
            }

            this.FillCustomersPicker();
        }

        
        private void SaveCustomers()
        {
            using (var datas = new DataAcess())
            {
                datas.DeleteAllCustomers();
                foreach (var customer in this.customers)
                {
                    datas.InsertCustomer(customer);
                }
            }
        }

        private void FillCustomersPicker()
        {
            this.customers = this.customers.OrderBy(c => c.FullName).ToList(); // Olhar depois
            foreach (var customer in this.customers)
            {
                this.customerPicker.Items.Add(customer.FullName);
            }
        }

        private async void LoadProducts()
        {
            waitActivityIndicator.IsRunning = true;
            string result = string.Empty;
            bool wasLoad = true;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080");
                string url = string.Format("");
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(result))
                {
                    wasLoad = false;
                }

            }
            catch (Exception ex)
            {
                wasLoad = false;
            }

            waitActivityIndicator.IsRunning = false;
            if (wasLoad) {
                if (result != null)
                {
                    this.products = JsonConvert.DeserializeObject<List<Product>>(result);
                    // Salvar Localmente
                    this.SaveProducts();
                }
            } else {
                using (var datas = new DataAcess())
                {
                    this.products = datas.GetProducts();
                }
            }

            this.FillProductsPicker();
        }

        private void SaveProducts()
        {
            using (var datas = new DataAcess())
            {
                datas.DeleteAllProducts();
                foreach(var product in this.products)
                {
                    datas.InsertProduct(product);
                }
            }
        }

        private void FillProductsPicker()
        {
            this.products = this.products.OrderBy(p => p.Description).ToList(); // Olhar depois
            foreach (var product in this.products)
            {
                this.productPicker.Items.Add(product.Description);
            }
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
