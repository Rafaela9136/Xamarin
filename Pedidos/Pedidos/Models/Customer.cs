using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class Customer
    {
        [PrimaryKey]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public int DocumentTypeID { get; set; }
        public string FullName { get; set; }
      
    }
}
