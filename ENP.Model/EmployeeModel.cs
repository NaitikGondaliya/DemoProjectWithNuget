using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENP.Model
{
    [Table(TableName = "Tbl_Employee")]
    public class EmployeeModel
    {
        [Key]
        public Guid EmpId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        
        [ExcludeColumn]
        //[ExcludeColumn(AllowRead = true)]
        public string EmployeeCode { get; set; }


    }


    [Table(TableName = "Tbl_EmployeeDetail")]
    public class EmployeeDetailModel
    {
        [Key]
        public Guid EmpDetailId { get; set; }
        public Guid EmpId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
    }
}
