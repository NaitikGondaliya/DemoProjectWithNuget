using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENP.Model
{
    [Table(TableName = "Tbl_StudentCourseMapping")]
    public class StudentCourseMapping
    {
        [Key]
        public long Id { get; set; }
        public long StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
