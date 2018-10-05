using CRUDSuppliers_DAL.Models;
using MVCSuppliers.Models;
using System.Collections.Generic;

namespace MVCSuppliers.Mapping
{
    public class ProductMapper
    {
        public static ProductPO DOToPO(ProductDO from)
        {
            ProductPO to = new ProductPO();

            to.ProductId = from.ProductId;
            to.ProductName = from.ProductName;
            to.SupplierId = from.SupplierId;
            to.CatagoryId = from.CatagoryId;
            to.QuantityPerUnit = from.QuantityPerUnit;
            to.UnitPrice = from.UnitPrice;
            to.UnitsInStock = from.UnitsInStock;
            to.UnitsOnOrder = from.UnitsOnOrder;
            to.ReorderLevel = from.ReorderLevel;
            to.Discontinued = from.Discontinued;

            return to;
        }

        public static List<ProductPO> DOToPO(List<ProductDO> from)
        {
            List<ProductPO> to = new List<ProductPO>();
            foreach (ProductDO product in from)
            {
                to.Add(ProductMapper.DOToPO(product));
            }
            return to;
        }
    }
}