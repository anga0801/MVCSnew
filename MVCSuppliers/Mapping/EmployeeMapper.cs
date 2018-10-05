using CRUDSuppliers_DAL.Models;
using MVCSuppliers.Models;
using System.Collections.Generic;

namespace MVCSuppliers.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeePO DOToPO(EmployeeDO from)
        {
            EmployeePO to = new EmployeePO();

            to.EmployeeId = from.EmployeeId;
            to.LastName = from.LastName;
            to.FirstName = from.FirstName;
            to.Title = from.Title;
            to.TitleOfCourtesy = from.TitleOfCourtesy;

            to.BirthDate = from.BirthDate;
            to.HireDate = from.HireDate;

            to.Address = from.Address;
            to.City = from.City;
            to.Region = from.Region;
            to.PostalCode = from.PostalCode;
            to.Country = from.Country;
            to.HomePhone = from.HomePhone;
            to.Extension = from.Extension;
            //Removed photo
            to.Notes = from.Notes;
            to.ReportsTo = from.ReportsTo;
            to.PhotoPath = from.PhotoPath;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Role = from.Role;

            return to;
        }

        public static List<EmployeePO> DOToPO(List<EmployeeDO> from)
        {
            List<EmployeePO> to = new List<EmployeePO>();
            foreach (EmployeeDO employee in from)
            {
                to.Add(EmployeeMapper.DOToPO(employee));
            }
            return to;
        }
    }
}