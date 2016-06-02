using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.models
{
    class DeviceUser
    {
        [PrimaryKey, AutoIncrement]
        public int DeviceUserId {get; set;}
        public string NickName { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int Password { get; set;}
    }
}
