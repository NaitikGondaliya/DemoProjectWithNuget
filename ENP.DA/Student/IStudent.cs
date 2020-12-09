using ENP.Model;
using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENP.DA
{
    public interface IStudent
    {
        ApiResponseModel Add(StudentModel studentModel);
        ApiResponseModel Update(StudentModel studentModel);
        ApiResponseModel GetById(long Id);
        ApiResponseModel GetAll();
        ApiResponseModel GetAll(int PageNo,int PageSize,string OrderBy,string SearchBy);
        ApiResponseModel Delete(long Id);
        ApiResponseModel GetStudentDetailById(long Id);
        ApiResponseModel InsertWithCourse(StudentWithCourseModel studentWithCourse);        
    }
}
