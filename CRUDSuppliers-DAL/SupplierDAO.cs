using CRUDSuppliers_DAL.Logging;
using CRUDSuppliers_DAL.Mapping;
using CRUDSuppliers_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CRUDSuppliers_DAL
{
    public class SupplierDAO
    {
        private readonly string ConnectionString;
        private readonly string _LogPath;

        public SupplierDAO(string connectionString, string logPath)
        {
            this.ConnectionString = connectionString;
            this._LogPath = logPath;
        }

        public List<SupplierDO> ObtainAllSuppliers()
        {
            List<SupplierDO> Suppliers = new List<SupplierDO>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                using (SqlCommand storedcommand = new SqlCommand("VIEW_ALL_SUPPLIERS", sqlConnection))
                {
                    storedcommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader obtainAll = storedcommand.ExecuteReader();
                    while (obtainAll.Read())
                    {
                        SupplierDO supplier = Mapper.ReaderToSupplier(obtainAll);

                        Suppliers.Add(supplier);
                    }
                }
                return Suppliers;
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
        }

        public SupplierDO ObtainSupplierById(int supplierId)
        {
            SupplierDO supplier = new SupplierDO();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                using (SqlCommand getIdInfo = new SqlCommand("SELECT_BY_ID", sqlConnection))
                {
                    sqlConnection.Open();
                    getIdInfo.Parameters.AddWithValue("SupplierID", supplierId);
                    getIdInfo.CommandType = CommandType.StoredProcedure;
                    getIdInfo.CommandTimeout = 60;
                    SqlDataReader obtainOne = getIdInfo.ExecuteReader();

                    if (obtainOne.Read())
                    {
                        supplier = Mapper.ReaderToSupplier(obtainOne);
                    }
                }
                return supplier;
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
        }

        public void CreateSupplier(SupplierDO supplier)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                using (SqlCommand newSupplier = new SqlCommand("CREATE_NEW_SUPPLIER_ENTRY", sqlConnection))
                {
                    newSupplier.CommandType = CommandType.StoredProcedure;
                    newSupplier.CommandTimeout = 60;
                    sqlConnection.Open();

                    newSupplier.Parameters.AddWithValue("CompanyName", supplier.CompanyName);
                    newSupplier.Parameters.AddWithValue("ContactName", supplier.ContactName);
                    newSupplier.Parameters.AddWithValue("ContactTitle", supplier.ContactTitle);
                    newSupplier.Parameters.AddWithValue("Address", supplier.Address);
                    newSupplier.Parameters.AddWithValue("City", supplier.City);
                    newSupplier.Parameters.AddWithValue("Region", supplier.Region);
                    newSupplier.Parameters.AddWithValue("PostalCode", supplier.PostalCode);
                    newSupplier.Parameters.AddWithValue("Country", supplier.Country);
                    newSupplier.Parameters.AddWithValue("Phone", supplier.Phone);
                    newSupplier.Parameters.AddWithValue("Fax", supplier.Fax);
                    newSupplier.Parameters.AddWithValue("HomePage", supplier.HomePage);

                    newSupplier.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
        }

        public void UpdateSupplier(SupplierDO supplier)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                using (SqlCommand update = new SqlCommand("UPDATE_SUPPLIER_INFORMATION", sqlConnection))
                {
                    update.Parameters.AddWithValue("SupplierID", supplier.SupplierID);
                    update.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    update.CommandTimeout = 60;

                    update.Parameters.AddWithValue("CompanyName", supplier.CompanyName);
                    update.Parameters.AddWithValue("ContactName", supplier.ContactName);
                    update.Parameters.AddWithValue("ContactTitle", supplier.ContactTitle);
                    update.Parameters.AddWithValue("Address", supplier.Address);
                    update.Parameters.AddWithValue("City", supplier.City);
                    update.Parameters.AddWithValue("Region", supplier.Region);
                    update.Parameters.AddWithValue("PostalCode", supplier.PostalCode);
                    update.Parameters.AddWithValue("Country", supplier.Country);
                    update.Parameters.AddWithValue("Phone", supplier.Phone);
                    update.Parameters.AddWithValue("Fax", supplier.Fax);
                    update.Parameters.AddWithValue("HomePage", supplier.HomePage);

                    update.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                using (SqlCommand delete = new SqlCommand("DELETE_SUPPLIER_ENTRY", sqlConnection))
                {
                    delete.CommandType = CommandType.StoredProcedure;
                    delete.CommandTimeout = 60;
                    sqlConnection.Open();

                    delete.Parameters.AddWithValue("SupplierID", supplierId);
                    delete.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.ErrorLogPath = _LogPath;
                Logger.SqlExceptionLog(sqlEx);

                throw sqlEx;
            }
        }

    }
}
