using CRUDSuppliers_DAL.Logging;
using CRUDSuppliers_DAL.Mapping;
using CRUDSuppliers_DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDSuppliers_DAL
{
    public class ProductDAO
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;

        public ProductDAO(string connectionString, string logPath)
        {
            this._ConnectionString = connectionString;
            this._LogPath = logPath;
        }

        public List<ProductDO> ObtainProductsBySupplierID(int id)
        {
            List<ProductDO> supplierProducts = new List<ProductDO>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand viewProductBySupplierID = new SqlCommand("PRODUCT_VIEW_BY_SUPPLIERID", sqlConnection))
                {
                    viewProductBySupplierID.CommandType = CommandType.StoredProcedure;
                    viewProductBySupplierID.CommandTimeout = 90;

                    viewProductBySupplierID.Parameters.AddWithValue("@SupplierID", id);

                    sqlConnection.Open();

                    using (SqlDataReader reader = viewProductBySupplierID.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductDO productDO = Mapper.ReaderToProduct(reader);

                            supplierProducts.Add(productDO);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
            return supplierProducts;
        }
    }
}
