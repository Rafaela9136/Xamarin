using System;
using SQLite.Net.Attributes;

namespace Pedidos.models
{
    public class Product
    {
        [PrimaryKey]
        public int ProductID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public DateTime LastBuy
        {
            get;
            set;
        }

        public float Stock
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }

        public int TaxID
        {
            get;
            set;
        }
    }
}
