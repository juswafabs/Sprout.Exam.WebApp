using Sprout.Exam.Business.ConcreteTypes;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public class ConcreteEmployeeTypeFactory : EmployeeTypeFactory
    {
        public override IEmploymentType GetEmployeeType(int empTypeId)
        {
            var type = (EmployeeType) empTypeId;

            switch (type)
            {
                case (EmployeeType.Regular):
                    return new RegularEmployee();
                case (EmployeeType.Contractual):
                    return new ContractualEmployee();
                default:
                    throw new ApplicationException("Employee Type cannot be determined");

            };
        }
    }
}
