using System;
using System.Collections.Generic;
using System.Text;

namespace ENP.Model
{
    public class StudentWithCourseModel
    {
        public StudentModel student { get; set; }
        public StudentCourseMapping courseMapping { get; set; }
    }
}
