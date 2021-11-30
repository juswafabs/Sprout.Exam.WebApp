using Microsoft.Extensions.Options;
using Sprout.Exam.Business.BusinessObject;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprout.Exam.Business
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ServiceResult<Employee> GetEmployee(int empId)
        {
            var result = new ServiceResult<Employee>();

            try
            {
                var employee = _employeeRepository.GetEmployee(empId);

                if (employee == null)
                {
                    result.isSuccessful = false;
                    result.error = "Error occurred while fetching employee.";
                }
                else
                    result.data = employee;

            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = "Error occurred while fetching employee.";
            }

            return result;
        }

        public ServiceResult<List<Employee>> GetEmployees()
        {
            var result = new ServiceResult<List<Employee>>();

            try
            {
                var employees = _employeeRepository.GetEmployees().Where(x => x.IsDeleted == false).ToList();
                result.data = employees;
            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = "Error occurred while fetching employees.";
            }

            return result;
        }

        public ServiceResult<bool> AddEmployee(CreateEmployeeDto employee)
        {
            var result = new ServiceResult<bool>();
            var errorString = "Error occurred while adding employee.";

            try
            {
                var bdate = new DateTime();
                if (!DateTime.TryParse(employee.Birthdate, out bdate))
                {
                    result.isSuccessful = false;
                    result.error = "Invalid date.";
                    return result;
                }

                var newEmployee = new Employee
                {
                    FullName = employee.FullName,
                    Birthdate = bdate,
                    TIN = employee.Tin,
                    EmployeeTypeId = employee.TypeId,
                    IsDeleted = false
                };

                var addResult = _employeeRepository.AddEmployee(newEmployee);

                result.isSuccessful = addResult;
                if (!addResult)
                    result.error = errorString;

                result.data = addResult;
            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = errorString;
            }


            return result;
        }

        public ServiceResult<bool> UpdateEmployee(EditEmployeeDto employeeEdited)
        {
            var result = new ServiceResult<bool>();
            var errorString = "Error occurred while updating employee.";
            try
            {
                var bdate = new DateTime();

                if (!DateTime.TryParse(employeeEdited.Birthdate, out bdate))
                {
                    result.isSuccessful = false;
                    result.error = "Invalid date.";
                    return result;
                }            

                var employee = new Employee() { 
                    Id = employeeEdited.Id,
                    FullName = employeeEdited.FullName,
                    Birthdate = bdate,
                    TIN = employeeEdited.Tin,
                    EmployeeTypeId = employeeEdited.TypeId
                };

                var updateIsSuccessful = _employeeRepository.UpdateEmployee(employee);

                if (!updateIsSuccessful)
                {
                    result.isSuccessful = false;
                    result.error = errorString;
                }

            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = errorString;
            }

            return result;
        }

        public ServiceResult<bool> DeleteEmployee(int empId)
        {
            var result = new ServiceResult<bool>();
            var errorString = "Error occurred while deleting employee.";

            try
            {
                var employee = _employeeRepository.GetEmployee(empId);

                if (employee == null)
                {
                    result.isSuccessful = false;
                    result.error = errorString;
                    return result;
                }

                employee.IsDeleted = true;

                var updateIsSuccessful = _employeeRepository.UpdateEmployee(employee);

                if (!updateIsSuccessful)
                {
                    result.isSuccessful = false;
                    result.error = errorString;
                }
            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = errorString;
            }

            return result;
        }
    }
}
