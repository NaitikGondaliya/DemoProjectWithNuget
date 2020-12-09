using ENP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENP.DA
{
    public interface IEmployee
    {
        EmployeeModel Add(EmployeeModel employeeModel);
        EmployeeModel Update(EmployeeModel employeeModel);
        bool Delete(Guid employeeId);
        EmployeeModel GetById(object employeeId);
        List<EmployeeModel> GetAll();
        ApiResponseModel Add(EmployeeRequestModel employeeRequest);
        ApiResponseModel Update(EmployeeRequestModel employeeRequest);
        List<EmployeeModel> GetAllWithFilter();
        EmployeeModel GetByColoumn(string Name);
        PaginationModel<EmployeeModel> GetPaginatedData(int page, int pageSize);
        List<EmployeePersonalInfoModel> GetByQuery();
        List<KeyValueControlBinder<Guid, string>> FillEmployee();
        ApiResponseModel AddUsingQuery();
        List<EmployeeModel> checkReadIgnore();
    }
    public class Employee : IEmployee
    {
        private readonly GenericDbContext _dbContext;
        //  IConfiguration _configuration;
        public Employee(GenericDbContext dbContext)//, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //_configuration = configuration;
            // ConnectionTools.ChangeDbConnection(_dbContext, _configuration.GetConnectionString("TestDefaultConnection"));
        }
        public EmployeeModel Add(EmployeeModel employeeModel)
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            repository.Add(employeeModel);
            return employeeModel;
        }

        public ApiResponseModel Add(EmployeeRequestModel employeeRequest)
        {
            ApiResponseModel apiResponse = null;
            using (var connection = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Repository<EmployeeModel> repoEmp = new Repository<EmployeeModel>(_dbContext);
                    Repository<EmployeeDetailModel> repoEmpDetail = new Repository<EmployeeDetailModel>(_dbContext);
                    repoEmp.Add(employeeRequest.Employee);
                    repoEmpDetail.Add(employeeRequest.EmployeeDetailModel);
                    connection.Commit();

                    apiResponse = new ApiResponseModel() { Data = employeeRequest, Message = "Record Added Succesfully", StatusCodes = StatusCodes.Status200OK };
                }
                catch (Exception Ex)
                {
                    connection.Rollback();
                    apiResponse = new ApiResponseModel() { Data = employeeRequest, Message = Ex.Message, StatusCodes = StatusCodes.Status400BadRequest };
                }
            }
            return apiResponse;
        }

        public bool Delete(Guid employeeId)
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            return repository.Delete(employeeId);
        }

        public List<EmployeeModel> GetAll()
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            return repository.ReadAll().ToList();
        }

        public EmployeeModel GetById(object Id)
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            return repository.ReadOne(Id);
        }

        public EmployeeModel Update(EmployeeModel employeeModel)
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
           // repository.Update(employeeModel, columns: "Name,Designation");
            repository.Update(employeeModel);
            return employeeModel;
        }

        public ApiResponseModel Update(EmployeeRequestModel employeeRequest)
        {
            ApiResponseModel apiResponse = null;
            using (var connection = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Repository<EmployeeModel> repoEmp = new Repository<EmployeeModel>(_dbContext);
                    Repository<EmployeeDetailModel> repoEmpDetail = new Repository<EmployeeDetailModel>(_dbContext);
                    repoEmp.Update(employeeRequest.Employee, columns: "Name");
                    repoEmpDetail.Update(employeeRequest.EmployeeDetailModel, columns: "ProjectEndDate");
                    connection.Commit();
                    apiResponse = new ApiResponseModel() { Data = employeeRequest, Message = "Record Updated Succesfully", StatusCodes = StatusCodes.Status200OK };
                }
                catch (Exception Ex)
                {
                    connection.Rollback();
                    apiResponse = new ApiResponseModel() { Data = employeeRequest, Message = Ex.Message, StatusCodes = StatusCodes.Status400BadRequest };
                }
            }
            return apiResponse;
        }

        public List<EmployeeModel> GetAllWithFilter()
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            //List<EmployeeModel> employees = repository.Get(x => x.EmpId == new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789"));
            List<EmployeeModel> employees = repository.ReadAll(orderBy: (x => x.OrderByDescending(xo => xo.Name)));
            return employees;
        }

        public EmployeeModel GetByColoumn(string Name)
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            //return repository.ReadOne(x => x.Name == Name, x => x.EmpId, x => x.Name);
            return repository.ReadOne(x => x.Name == Name);
        }

        public PaginationModel<EmployeeModel> GetPaginatedData(int page, int pageSize)
        {
            PaginationModel<EmployeeModel> res = null;
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            res = repository.ReadAll(1, 10, orderBy: x => x.OrderBy(xo => xo.EmpId));
            return res;
        }

        public List<EmployeePersonalInfoModel> GetByQuery()
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            //return repository.ReadAllQuery("Select Name as EmployeeName ,ProjectName from Tbl_Employee E inner join Tbl_EmployeeDetail Ed on ed.Empid=E.Empid WHERE Ed.EmpId =@UserID ",
            //new Dictionary<string, object> { { "@UserID", new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789") } }).ToList();

            return repository.Query<EmployeePersonalInfoModel>("Select Name as EmployeeName ,ProjectName from Tbl_Employee E left join Tbl_EmployeeDetail Ed on ed.Empid=E.Empid Order By Name ",
            new Dictionary<string, object> { { "@UserID", new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789") } }).ToList();
        }

        public List<KeyValueControlBinder<Guid, string>> FillEmployee()
        {
            List<KeyValueControlBinder<Guid, string>> lstEmployee;
            using (var connection = _dbContext)
            {
                Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
                lstEmployee = repository.Query<KeyValueControlBinder<Guid, string>>("Select EmpId as Id,Name from Tbl_Employee");
                return lstEmployee;
            }
        }

        public ApiResponseModel AddUsingQuery()
        {
            ApiResponseModel apiResponseModel;
            using (var connection = _dbContext)
            {
                Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
                int affectedRow = repository.ExecuteNonQuery("insert into Tbl_Employee values(NEWID(),'Added by Method 2','D2')");
                apiResponseModel = new ApiResponseModel() { Data = affectedRow, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponseModel;
        }

        public List<EmployeeModel> checkReadIgnore()
        {
            Repository<EmployeeModel> repository = new Repository<EmployeeModel>(_dbContext);
            //return repository.ReadAllQuery("Select Name as EmployeeName ,ProjectName from Tbl_Employee E inner join Tbl_EmployeeDetail Ed on ed.Empid=E.Empid WHERE Ed.EmpId =@UserID ",
            //new Dictionary<string, object> { { "@UserID", new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789") } }).ToList();

            return repository.ReadAll();
        }
    }
}
