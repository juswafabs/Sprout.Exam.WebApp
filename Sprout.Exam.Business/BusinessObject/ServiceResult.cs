using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.BusinessObject
{
    public class ServiceResult<T>
    {
        public T data { get; set; }
        public bool isSuccessful { get; set; } = true;
        public string error { get; set; } = "";
    }
}
