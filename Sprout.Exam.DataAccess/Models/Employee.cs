using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Models
{
    [Table("dbo.Employee")]

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
