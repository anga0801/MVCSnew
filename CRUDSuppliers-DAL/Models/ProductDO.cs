using System;

namespace CRUDSuppliers_DAL.Models
{
    public class ProductDO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int SupplierId { get; set; }

        public int CatagoryId { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public Int16 UnitsInStock { get; set; }

        public Int16 UnitsOnOrder { get; set; }

        public Int16 ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
