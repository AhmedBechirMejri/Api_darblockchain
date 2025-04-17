using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entity.DTO
{
    public class LeaveReportDto
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public int AnnualLeaveDaysUsed { get; set; }
        public int SickLeaveDays { get; set; }
        public int TotalLeaveDaysUsed { get; set; }

    }
}
