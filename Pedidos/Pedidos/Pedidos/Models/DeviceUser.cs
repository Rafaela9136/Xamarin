using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class DeviceUser
    {
        [PrimaryKey, AutoIncrement]
        public int DeviceUserId {get; set;}
        public string NickName { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int Password { get; set;}
    }
}
