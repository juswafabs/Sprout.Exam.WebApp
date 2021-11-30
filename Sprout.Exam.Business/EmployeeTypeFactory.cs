using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public abstract class EmployeeTypeFactory
    {
        public abstract IEmploymentType GetEmployeeType(int empTypeId);
    }
}
