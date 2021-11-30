using Sprout.Exam.Business.BusinessObject;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.ConcreteTypes
{
    public class RegularEmployee : IEmploymentType
    {
        const decimal monthlySalary = 20000;
        const decimal taxPercent = 0.12M;
        public ServiceResult<decimal> ComputeSalary(int empId, decimal absentDays, decimal workedDays)
        {
            var result = new ServiceResult<decimal>();

            try
            {
                decimal netIncome;
                decimal deduction = (absentDays * (monthlySalary / 22));
                decimal tax = (monthlySalary * taxPercent);

                netIncome = (monthlySalary - (deduction)) - tax;

                result.data = netIncome;
            }
            catch (Exception)
            {
                result.isSuccessful = false;
                result.error = "Error occured while computing salary";
            }

            return result;
        }
    }
}
