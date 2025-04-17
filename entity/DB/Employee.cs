using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entity.DB
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public int AnnualLeaveDaysUsed { get; set; }
        public int SickLeaveDays { get; set; }
        public int TotalLeaveDaysUsed { get; set; }

    }
}
