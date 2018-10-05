using System.Collections.Generic;

namespace MVCSuppliers.Models
{
    public class SupplierVM
    {
        public SupplierVM()
        {
            this.SupplierData = new SupplierPO();

            this.ProductsBySupplier = new List<ProductPO>();
        }
        public SupplierPO SupplierData { get; set; }

        public List<ProductPO> ProductsBySupplier { get; set; }
    }
}