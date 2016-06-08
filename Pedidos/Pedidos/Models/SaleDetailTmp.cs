using System;
using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class SaleDetailTmp
    {
        [PrimaryKey]
        public int ProductID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
        
        public override string ToString()
        {
            return string.Format ("ProductId ={0}, Description={1}, Price={2}, Quantity={3}", ProductID, Description, Price, Quantity);
        }
    }
}