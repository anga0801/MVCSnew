using CRUDSuppliers.Models;
using CRUDSuppliers_DAL.Models;
using System.Collections.Generic;

namespace CRUDSuppliers.Mapping
{
    public static class Mapper
    {
        public static List<SupplierPO> FromDoToPo(List<SupplierDO> from)
        {
            List<SupplierPO> newSuppList = new List<SupplierPO>();

            for (int field = 0; field < from.Count; field++)
            {
                SupplierPO suppInfo = new SupplierPO();

                suppInfo.SupplierID = from[field].SupplierID;
                suppInfo.CompanyName = from[field].CompanyName;
                suppInfo.ContactName = from[field].ContactName;
                suppInfo.ContactTitle = from[field].ContactTitle;
                suppInfo.Address = from[field].Address;
                suppInfo.City = from[field].City;
                suppInfo.Region = from[field].Region;
                suppInfo.PostalCode = from[field].PostalCode;
                suppInfo.Country = from[field].Country;
                suppInfo.Phone = from[field].Phone;
                suppInfo.Fax = from[field].Fax;
                suppInfo.HomePage = from[field].HomePage;

                newSuppList.Add(suppInfo);
            }
            return newSuppList;
        }

        public static SupplierDO PoToDo(SupplierPO from)
        {
            SupplierDO suppInfo = new SupplierDO();

            suppInfo.SupplierID = from.SupplierID;
            suppInfo.CompanyName = from.CompanyName;
            suppInfo.ContactName = from.ContactName;
            suppInfo.ContactTitle = from.ContactTitle;
            suppInfo.Address = from.Address;
            suppInfo.City = from.City;
            suppInfo.Region = from.Region;
            suppInfo.PostalCode = from.PostalCode;
            suppInfo.Country = from.Country;
            suppInfo.Phone = from.Phone;
            suppInfo.Fax = from.Fax;
            suppInfo.HomePage = from.HomePage;

            return suppInfo;
        }

        public static SupplierPO DoToPo(SupplierDO from)
        {
            SupplierPO suppInfo = new SupplierPO();

            suppInfo.SupplierID = from.SupplierID;
            suppInfo.CompanyName = from.CompanyName;
            suppInfo.ContactName = from.ContactName;
            suppInfo.ContactTitle = from.ContactTitle;
            suppInfo.Address = from.Address;
            suppInfo.City = from.City;
            suppInfo.Region = from.Region;
            suppInfo.PostalCode = from.PostalCode;
            suppInfo.Country = from.Country;
            suppInfo.Phone = from.Phone;
            suppInfo.Fax = from.Fax;
            suppInfo.HomePage = from.HomePage;

            return suppInfo;
        }
    }
}
