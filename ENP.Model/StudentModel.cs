using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENP.Model
{
    [Table(TableName = "Tbl_Student")]
    public class StudentModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }

        [ExcludeColumn(AllowRead =true)]
        public string RollNo { get; set; }

        
        [ExcludeColumn]
        public string CourseName { get; set; }
        [ExcludeColumn]
        public decimal DurationInYear { get; set; }
    }
}
