using System;
using SQLite.Net;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Pedidos
{
    public class DataAcess : IDisposable
    {
        private SQLiteConnection connection;
        
        public DataAcess ()
        {
            var config = DependencyService.Get<Interface.IConfig>();
            connection = new SQLiteConnection(config.Platforma, System.IO.Path.Combine(config.DirectorioDB, "pedidos.db3"));
            
            connection.CreateTable<models.Customer>();
            connection.CreateTable<models.DeviceUser>();
            connection.CreateTable<models.Product>();
            connection.CreateTable<models.Sale>();
            connection.CreateTable<models.SaleDetail>();
            connection.CreateTable<models.SaleDetailTmp>();
        }
        #region DeviceUser
        public void InsertDeviceUser(models.DeviceUser deviceUser) 
        {
            connection.Insert(deviceUser);
        }
        
        public void UpdateDeviceUser(models.DeviceUser deviceUser) 
        {
            connection.Update(deviceUser);
        }
        
        public void DeleteDeviceUser(models.DeviceUser deviceUser) 
        {
            connection.Delete(deviceUser);
        }
        
        public models.DeviceUser GetDeviceUser(int id) 
        {
            return connection.Table<models.DeviceUser>().FirstOrDefault(du => du.DeviceUserId == id);
        }
        
        public List<models.DeviceUser> GetDeviceUsers()
        {
            return connection.Table<models.DeviceUser>().ToList();
        }
        #endregion

        #region Customer
        public void InsertCustomer(models.Customer customer)
        {
            connection.Insert(customer);
        }

        public void UpdateCustomer(models.Customer customer)
        {
            connection.Update(customer);
        }

        public void DeleteCustomer(models.Customer customer)
        {
            connection.Delete(customer);
        }

        public models.Customer GetCustomer(int id)
        {
            return connection.Table<models.Customer>().FirstOrDefault(c => c.CustomerID == id);
        }

        public List<models.Customer> GetCustomers()
        {
            return connection.Table<models.Customer>().ToList();
        }

        public void DeleteAllCustomers()
        {
            connection.DeleteAll<models.Customer>();
        }
        #endregion

        #region Products
        public void InsertProduct(models.Product product)
        {
            connection.Insert(product);
        }

        public void UpdateProduct(models.Product product)
        {
            connection.Update(product);
        }

        public void DeleteProduct(models.Product product)
        {
            connection.Delete(product);
        }

        public models.Product GetProduct(int id)
        {
            return connection.Table<models.Product>().FirstOrDefault(p => p.ProductID == id);
        }

        public List<models.Product> GetProducts()
        {
            return connection.Table<models.Product>().ToList();
        }

        public void DeleteAllProducts()
        {
            connection.DeleteAll<models.Product>();
        }
        #endregion

        #region SaleDetailsTmp
        public void InsertSaleDetailTmp(models.SaleDetailTmp saleDetailTmp)
        {
            connection.Insert(saleDetailTmp);
        }

        public void UpdateSaleDetailTmp(models.SaleDetailTmp saleDetailTmp)
        {
            connection.Update(saleDetailTmp);
        }

        public void DeleteSaleDetailTmp(models.SaleDetailTmp saleDetailTmp)
        {
            connection.Delete(saleDetailTmp);
        }

        public models.SaleDetailTmp GetSaleDetailTmp(int id)
        {
            return connection.Table<models.SaleDetailTmp>().FirstOrDefault(sdt => sdt.ProductID == id);
        }

        public List<models.SaleDetailTmp> GetSaleDetailTmp()
        {
            return connection.Table<models.SaleDetailTmp>().ToList();
        }

        public void DeleteAllSaleDetailTmp()
        {
            connection.DeleteAll<models.SaleDetailTmp>();
        }
        #endregion 
        
        #region Sale
        public void InsertSale(models.Sale sale)
        {
            connection.Insert(sale);
        }

        public void UpdateSale(models.Sale sale)
        {
            connection.Update(sale);
        }

        public void DeleteSale(models.Sale sale)
        {
            connection.Delete(sale);
        }

        public models.Sale GetSale(int id)
        {
            return connection.Table<models.Sale>().FirstOrDefault(s => s.SaleID == id);
        }

        public List<models.Sale> GetSale()
        {
            return connection.Table<models.Sale>().ToList();
        }

        public void DeleteAllSale()
        {
            connection.DeleteAll<models.Sale>();
        }
        #endregion 
        
        #region SaleDetails
        public void InsertSaleDetail(models.SaleDetail saleDetail)
        {
            connection.Insert(saleDetail);
        }

        public void UpdateSaleDetail(models.SaleDetailTmp saleDetail)
        {
            connection.Update(saleDetail);
        }

        public void DeleteSaleDetail(models.SaleDetailTmp saleDetail)
        {
            connection.Delete(saleDetail);
        }

        public models.SaleDetail GetSaleDetail(int id)
        {
            return connection.Table<models.SaleDetail>().FirstOrDefault(sd => sd.SaleDetailID == id);
        }

        public List<models.SaleDetail> GetSaleDetail()
        {
            return connection.Table<models.SaleDetail>().ToList();
        }

        public void DeleteAllSaleDetail()
        {
            connection.DeleteAll<models.SaleDetail>();
        }
        #endregion 
        
        public void Dispose()
        {
            connection.Dispose();
        }        
    }
}