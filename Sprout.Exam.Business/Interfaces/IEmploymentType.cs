using Sprout.Exam.Business.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmploymentType
    {
        ServiceResult<decimal> ComputeSalary(int empId, decimal absentDays, decimal workedDays);
    }
}
