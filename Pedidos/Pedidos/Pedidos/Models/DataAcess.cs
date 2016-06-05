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
            connection.CreateTable<models.DeviceUser>();
        }
        
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
        
        public void Dispose()
        {
            connection.Dispose();
        }
        
        
    }
}