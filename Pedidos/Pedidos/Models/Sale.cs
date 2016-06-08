using System;
using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int SaleID { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateSale { get; set; }
        public bool saved { get; set; }
    }
}