using CRUDSuppliers_DAL.Logging;
using CRUDSuppliers_DAL.Mapping;
using CRUDSuppliers_DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDSuppliers_DAL
{
    public class EmployeeDAO
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;

        public EmployeeDAO(string connectionString, string logPath)
        {
            this._ConnectionString = connectionString;
            this._LogPath = logPath;
        }

        //View by username
        public EmployeeDO ViewEmployeeByUsername(string username)
        {
            EmployeeDO response = new EmployeeDO();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand viewSupplierByID = new SqlCommand("VIEW_EMPLOYEE_BY_USERNAME", sqlConnection))
                {
                    viewSupplierByID.CommandType = CommandType.StoredProcedure;
                    viewSupplierByID.CommandTimeout = 90;

                    viewSupplierByID.Parameters.AddWithValue("@Username", username);

                    sqlConnection.Open();

                    using (SqlDataReader reader = viewSupplierByID.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response = Mapper.ReaderToEmployee(reader);
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
            return response;
        }
        
        //View by id
        public EmployeeDO ViewEmployeeById(int id)
        {
            EmployeeDO response = new EmployeeDO();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand viewSupplierByID = new SqlCommand("VIEW_EMPLOYEE_BY_ID", sqlConnection))
                {
                    viewSupplierByID.CommandType = CommandType.StoredProcedure;
                    viewSupplierByID.CommandTimeout = 90;

                    viewSupplierByID.Parameters.AddWithValue("@Id", id);

                    sqlConnection.Open();

                    using (SqlDataReader reader = viewSupplierByID.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response = Mapper.ReaderToEmployee(reader);
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
            return response;
        }
        
        //View all
        public List<EmployeeDO> ViewAllEmployees()
        {
            List<EmployeeDO> allEmployees = new List<EmployeeDO>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                using (SqlCommand viewSupplierByID = new SqlCommand("VIEW_ALL_EMPLOYEES", sqlConnection))
                {
                    viewSupplierByID.CommandType = CommandType.StoredProcedure;
                    viewSupplierByID.CommandTimeout = 90;

                    sqlConnection.Open();

                    using (SqlDataReader reader = viewSupplierByID.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeDO item = new EmployeeDO();
                            item = Mapper.ReaderToEmployee(reader);
                            allEmployees.Add(item);
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
            return allEmployees;
        }
    }
}
