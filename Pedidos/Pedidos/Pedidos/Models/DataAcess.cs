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
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma, SYstem.IO.Path.Combine(config.DirectorioDB, "Pedidos.db3"));
            connection.CreateTable<DeviceUser>();
        }
        git config --global user.email "email"
        ''   ''       ''    user.name "my name"
        public void InsertDeviceUser(DeviceUser deviceUser) 
        {
            connection.Insert(deviceUser);
        }
        
        public void UpdateDeviceUser(DeviceUser deviceUser) 
        {
            connection.Update(deviceUser);
        }
        
        public void DeleteDeviceUser(DeviceUser deviceUser) 
        {
            connection.Delete(deviceUser);
        }
        
        public DeviceUser GetDeviceUser(int id) 
        {
            return connection.Table<DeviceUser>().FirstOrDefault(du => du.DeviceUserID == id);
        }
        
        public List<DeviceUser> GetDeviceUsers()
        {
            return connection.Table<DeviceUser>().ToList();
        }
        
        public void Dispose()
        {
            connection.Dispose();
        }
        
        
    }
}