using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.BusinessObject;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeSvc;
        //ConcreteEmployeeTypeFactory _factory;
        public EmployeesController(IEmployeeService employeeSvc)
        {
            _employeeSvc = employeeSvc;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_employeeSvc.GetEmployees());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(_employeeSvc.GetEmployee(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto employeeEdited)
        {
            try
            {
                return Ok(_employeeSvc.UpdateEmployee(employeeEdited));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            try
            {
                return Ok(_employeeSvc.AddEmployee(input));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(_employeeSvc.DeleteEmployee(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(CalculatePayParams calculatePayParams)
        {
            try
            {
                var employee = _employeeSvc.GetEmployee(calculatePayParams.id);

                if (employee == null) return NotFound();

                EmployeeTypeFactory factory = new ConcreteEmployeeTypeFactory();
                IEmploymentType type = factory.GetEmployeeType(employee.data.EmployeeTypeId);

                return Ok(type.ComputeSalary(calculatePayParams.id, calculatePayParams.absentDays, calculatePayParams.workedDays));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error");
            }
        }

    }
}
