using CRUDSuppliers_DAL.Models;
using System;
using System.Data.SqlClient;

namespace CRUDSuppliers_DAL.Mapping
{
    public class Mapper
    {
        public static SupplierDO ReaderToSupplier(SqlDataReader from)
        {
            SupplierDO to = new SupplierDO();

            to.SupplierID = from.GetInt32(0);
            to.CompanyName = from.GetValue(1) as string;
            to.ContactName = from.GetValue(2) as string;
            to.ContactTitle = from.GetValue(3) as string;
            to.Address = from.GetValue(4) as string;
            to.City = from.GetValue(5) as string;
            to.Region = from.GetValue(6) as string;
            to.PostalCode = from.GetValue(7) as string;
            to.Country = from.GetValue(8) as string;
            to.Phone = from.GetValue(9) as string;
            to.Fax = from.GetValue(10) as string;
            to.HomePage = from.GetValue(11) as string;

            return to;
        }

        public static EmployeeDO ReaderToEmployee(SqlDataReader from)
        {
            EmployeeDO to = new EmployeeDO();

            to.EmployeeId = (int)from["EmployeeId"];
            to.LastName = from["LastName"] as string;
            to.FirstName = from["FirstName"] as string;
            to.Title = from["Title"] as string;
            to.TitleOfCourtesy = from["TitleOfCourtesy"] as string;

            if (!(from["BirthDate"] is DBNull))
            {
                to.BirthDate = (DateTime)from["BirthDate"];
            }
            if (!(from["HireDate"] is DBNull))
            {
                to.HireDate = (DateTime)from["HireDate"];
            }

            to.Address = from["Address"] as string;
            to.City = from["City"] as string;
            to.Region = from["Region"] as string;
            to.PostalCode = from["PostalCode"] as string;
            to.Country = from["Country"] as string;
            to.HomePhone = from["HomePhone"] as string;
            to.Extension = from["Extension"] as string;
            //Removed photo
            to.Notes = from["Notes"] as string;
            to.ReportsTo = (int?)from["ReportsTo"];
            to.PhotoPath = from["PhotoPath"] as string;
            to.Username = from["Username"] as string;
            to.Password = from["Password"] as string;
            to.Role = (int)from["Role"];

            return to;
        }

        public static ProductDO ReaderToProduct(SqlDataReader from)
        {
            ProductDO to = new ProductDO();

            to.ProductId = from.GetInt32(0);
            to.ProductName = from.GetValue(1) as string;
            to.SupplierId = from.GetInt32(2);
            to.CatagoryId = from.GetInt32(3);
            to.QuantityPerUnit = from.GetValue(4) as string;
            to.UnitPrice = from.GetDecimal(5);
            to.UnitsInStock = from.GetInt16(6);
            to.UnitsOnOrder = from.GetInt16(7);
            to.ReorderLevel = from.GetInt16(8);
            to.Discontinued = from.GetBoolean(9);

            return to;
        }
    }
}
