
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sprout.Exam.DataAccess
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;
        public EmployeeRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }


        public Employee GetEmployee(int empId)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_dbConnection.ConnectionString))
                {
                    var employee = sqlConnection.Get<Employee>(empId);

                    return employee;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Employee> GetEmployees()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_dbConnection.ConnectionString))
                {
                    var employees = sqlConnection.GetAll<Employee>();

                    return employees.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_dbConnection.ConnectionString))
                {
                    var result = sqlConnection.Update(employee);

                    return result;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_dbConnection.ConnectionString))
                {
                    var resultId = sqlConnection.Insert(employee);

                    if (resultId != 0)
                        return true;

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
