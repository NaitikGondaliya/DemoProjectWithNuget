using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENP.Model
{
    [Table(TableName = "Tbl_Course")]
    public class CourseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DurationInYear { get; set; }
    }
}
