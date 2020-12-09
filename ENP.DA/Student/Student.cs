using ENP.Model;
using Microsoft.AspNetCore.Http;
using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENP.DA
{
    public class Student : IStudent
    {
        GenericDbContext DbContext;
        public Student(GenericDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public ApiResponseModel Add(StudentModel studentModel)
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                repository.Add(studentModel);
                apiResponse = new ApiResponseModel() { Data = studentModel, Message = "Record Added Successfully", StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }

        public ApiResponseModel Delete(long Id)
        {
            ApiResponseModel apiResponse = null;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                if (repository.Count("Select count(*) from Tbl_StudentCourseMapping where StudentId=@StudentId", new Dictionary<string, object> { { "StudentId", Id } }) < 1)
                {
                    repository.Delete(Id);
                    apiResponse = new ApiResponseModel() { Data = new { Result = true }, Message = "Record Delete Successfully", StatusCodes = StatusCodes.Status200OK };
                }
                else
                    apiResponse = new ApiResponseModel() { Message = "Record is mapped, Can not delete it", StatusCodes = StatusCodes.Status304NotModified };
            }
            return apiResponse;
        }

        public ApiResponseModel GetAll()
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                List<StudentModel> lstStudent = repository.ReadAll();
                apiResponse = new ApiResponseModel() { Data = lstStudent, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }

        public ApiResponseModel GetAll(int PageNo, int PageSize, string OrderBy, string SearchBy)
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                string sqlQuery = "Select * from Tbl_Student";
                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    sqlQuery = sqlQuery + $" Where (Name like Concat('%',@SearchBy,'%') OR RollNo like Concat('%',@SearchBy,'%') OR MiddleName like Concat('%',@SearchBy,'%') OR MobileNo like Concat('%',@SearchBy,'%') OR Address like Concat('%',@SearchBy,'%'))";
                }
                PaginationModel<StudentModel> lstStudent = repository.Query<StudentModel>(sqlQuery, PageNo, PageSize, OrderBy, new Dictionary<string, object> { { "@SearchBy", SearchBy } });
                apiResponse = new ApiResponseModel() { Data = lstStudent, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }

        public ApiResponseModel GetById(long Id)
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                StudentModel Student = repository.ReadOne(Id);
                apiResponse = new ApiResponseModel() { Data = Student, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }

        public ApiResponseModel GetStudentDetailById(long Id)
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                StudentModel student = repository.Query<StudentModel>(@"SELECT st.*,c.[Name] as courseName,c.DurationInYear  FROM Tbl_StudentCourseMapping scm 
                inner join Tbl_Student St on St.Id=scm.StudentId
                inner join Tbl_Course c on c.Id=scm.CourseId where st.Id=@Id", new Dictionary<string, object> { { "@Id", Id } }).FirstOrDefault();
                apiResponse = new ApiResponseModel() { Data = student, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }

        public ApiResponseModel InsertWithCourse(StudentWithCourseModel studentWithCourse)
        {
            ApiResponseModel apiResponse = null;
            try
            {
                using (var connection = DbContext)
                {
                    DbContext.Database.BeginTransaction();
                    Repository<StudentModel> repository = new Repository<StudentModel>(DbContext);
                    Repository<StudentCourseMapping> repositoryMapping = new Repository<StudentCourseMapping>(DbContext);
                    repository.Add(studentWithCourse.student);
                    studentWithCourse.courseMapping.StudentId = studentWithCourse.student.Id;
                    repositoryMapping.Add(studentWithCourse.courseMapping);
                    apiResponse = new ApiResponseModel() { Data = studentWithCourse, Message = "Record Added Successfully", StatusCodes = StatusCodes.Status200OK };
                    DbContext.Database.CommitTransaction();
                }
                return apiResponse;
            }
            catch (Exception)
            {
                DbContext.Database.RollbackTransaction();
                return apiResponse;
            }
        }

        public ApiResponseModel Update(StudentModel studentModel)
        {
            ApiResponseModel apiResponse;
            using (var connection = DbContext)
            {
                Repository<StudentModel> repository = new Repository<StudentModel>(connection);
                repository.Update(studentModel);
                apiResponse = new ApiResponseModel() { Data = studentModel, Message = "Record Updated Successfully", StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponse;
        }
    }
}
