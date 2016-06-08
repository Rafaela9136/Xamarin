using System;
using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class SaleDetail
    {
        [PrimaryKey, AutoIncrement]
        public int SaleDetailID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
    }
}