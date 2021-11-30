using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.BusinessObject
{
    public class CalculatePayParams
    {
        public int id { get; set; }
        public decimal absentDays { get; set; }
        public decimal workedDays { get; set; }
    }
}
