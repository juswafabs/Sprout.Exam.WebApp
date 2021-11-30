using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprout.Exam.Business;
using Sprout.Exam.Business.ConcreteTypes;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Data;
using Xunit.Sdk;

namespace UnitTestHRPayrollApp
{
    [TestClass]
    public class UnitTestEmployee
    {
        private readonly IEmployeeService _employeeSvc;
        IServiceCollection services;
        public UnitTestEmployee(IEmployeeService employeeSvc)
        {
            services = new ServiceCollection();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmploymentType, RegularEmployee>();
            services.AddScoped<IEmploymentType, ContractualEmployee>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //var serviceProvider = service
        }


        [TestMethod]
        public void TestAddEmployee()
        {
            var newEmployee = new CreateEmployeeDto
            {
                FullName = "Jane Doe",
                Birthdate = "12/25/2000",
                Tin = "1231234",
                TypeId = 1
            };

            var result = _employeeSvc.AddEmployee(newEmployee);

            Assert.IsTrue(result.isSuccessful);
        }
    }
}
