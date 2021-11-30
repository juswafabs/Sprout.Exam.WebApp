﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    interface IEmployeeTypeFactory
    {
        IEmploymentType GetEmployeeType();
    }
}
