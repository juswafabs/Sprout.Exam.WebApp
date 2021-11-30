using Sprout.Exam.Business.BusinessObject;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.ConcreteTypes
{
    public class ContractualEmployee : IEmploymentType
    {
        const decimal ratePerDay = 500;

        public ServiceResult<decimal> ComputeSalary(int empId, decimal absentDays, decimal workedDays)
        {
            var result = new ServiceResult<decimal>();

            try
            {
                result.data = ratePerDay * workedDays;
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
