using Sprout.Exam.Business.BusinessObject;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;
using System.Collections.Generic;

namespace Sprout.Exam.Business
{
    public interface IEmployeeService
    {
        ServiceResult<bool> AddEmployee(CreateEmployeeDto employee);
        ServiceResult<Employee> GetEmployee(int empId);
        ServiceResult<List<Employee>> GetEmployees();
        ServiceResult<bool> UpdateEmployee(EditEmployeeDto employeeEdited);
        ServiceResult<bool> DeleteEmployee(int empId);
    }
}