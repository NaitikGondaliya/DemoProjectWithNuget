using System;
using System.Collections.Generic;
using System.Text;

namespace ENP.Model
{
    public class EmployeeRequestModel
    {
        public EmployeeModel Employee { get; set; }
        public EmployeeDetailModel EmployeeDetailModel { get; set; }
    }

    public class EmployeePersonalInfoModel
    {
        public string EmployeeName { get; set; }
        public string AssignedProject { get; set; }
    }
}
