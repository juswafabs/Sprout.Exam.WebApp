using Sprout.Exam.DataAccess.Models;
using System.Collections.Generic;

namespace Sprout.Exam.DataAccess
{
    public interface IEmployeeRepository
    {
        bool AddEmployee(Employee employee);
        Employee GetEmployee(int empId);
        List<Employee> GetEmployees();
        bool UpdateEmployee(Employee employee);
    }
}