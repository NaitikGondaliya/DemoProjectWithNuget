using ENP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShivOhm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ENP.DA
{
    public interface IContacts
    {
        ApiResponseModel Add(ContactsModel contacts);
        ApiResponseModel Update(ContactsModel contacts);
        ApiResponseModel Delete(int contactId);
        ApiResponseModel GetById(int contactId);
        ApiResponseModel GetAllPagination(int pageNo, int pageSize, string orderBy);
        ApiResponseModel GetAllPaginationByQuery(int pageNo, int pageSize, string orderBy);
        ApiResponseModel GetCount();
        ApiResponseModel Exist();
        ApiResponseModel Count();
    }

    public class Contacts : IContacts
    {
        public readonly GenericDbContext _context;
        public Contacts(GenericDbContext context)
        {
            _context = context;

        }

        public ApiResponseModel Add(ContactsModel contacts)
        {
            try
            {
                using (var connection = _context)
                {
                    Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                    repository.Add(contacts);
                }
                return new ApiResponseModel() { Data = contacts, Message = "Record Added Succesfully", StatusCodes = StatusCodes.Status200OK };

            }
            catch (Exception ex)
            {
                return new ApiResponseModel() { Data = contacts, Message = ex.InnerException.Message, StatusCodes = StatusCodes.Status400BadRequest };
            }

        }

        public ApiResponseModel Delete(int contactId)
        {
            try
            {
                using (var connection = _context)
                {
                    Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                    repository.Delete(contactId);
                }
                return new ApiResponseModel() { Data = true, Message = "Record Deleted Succesfully", StatusCodes = StatusCodes.Status200OK };

            }
            catch (Exception ex)
            {
                return new ApiResponseModel() { Data = false, Message = ex.InnerException.Message, StatusCodes = StatusCodes.Status400BadRequest };
            }
        }

        public ApiResponseModel GetAllPagination(int pageNo, int pageSize, string orderBy)
        {
            List<ContactsModel> lstContact = null;
            PaginationModel<ContactsModel> s = new PaginationModel<ContactsModel>();
            try
            {
                using (var connection = _context)
                {
                    Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                    //lstContact = repository.GetAllByEntity(filter: (x => x.ID == 1), orderBy: (x => x.OrderByDescending(xo => xo.ID)));
                    s = repository.ReadAll(page: pageNo, pageSize: pageSize, orderBy: (x => x.OrderByDescending(xo => xo.ID)));
                    //lstContact = repository.ReadAllQuery<ContactsModel>($"Select top 10 Id,Name from Contacts", Parameters: new Dictionary<string, object> { { "@UserID", new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789") } });
                }
                return new ApiResponseModel() { Data = s, StatusCodes = StatusCodes.Status200OK };

            }
            catch (Exception ex)
            {
                return new ApiResponseModel() { Data = null, Message = ex.Message, StatusCodes = StatusCodes.Status400BadRequest };
            }
        }

        public ApiResponseModel GetAllPaginationByQuery(int pageNo, int pageSize, string orderBy)
        {
            try
            {
                using (var connection = _context)
                {
                    Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                    //List<ContactsModel> contacts = new List<ContactsModel>();
                    //EntityList<object> entityList = repository.ReadAllPaginationQuery(Query: "Select * from Contacts", orderby: "Id desc", pageNo: 1, pageSize: 10, Parameters: new Dictionary<string, object> { { "@UserID", new Guid("3FA85F64-5717-4562-B3FC-2C963F66A789") } });
                    //if (entityList.PageData !=null)
                    //{
                    //    foreach (var item in entityList.PageData)
                    //    {
                    //        ContactsModel contactsModel = new ContactsModel();
                    //        Mapper<ContactsModel>.Map((ExpandoObject)item, contactsModel);
                    //        contacts.Add(contactsModel);
                    //    }
                    //}
                    //entityList.PageData = contacts;
                    //return new ApiResponseModel() { Data = entityList, StatusCodes = StatusCodes.Status200OK };

                    PaginationModel<ContactsModel> entityList = repository.Query<ContactsModel>(Query: "select top 100 * from Contacts where FirstName like concat('%',@name,'%')", orderby: "Id desc", pageNo: pageNo, pageSize: pageSize, Parameters: new Dictionary<string, object> { { "@name", "naitik" } });
                    return new ApiResponseModel() { Data = entityList, StatusCodes = StatusCodes.Status200OK };

                }
            }
            catch (Exception ex)
            {
                return new ApiResponseModel() { Data = null, Message = ex.Message, StatusCodes = StatusCodes.Status400BadRequest };
            }
        }

        public ApiResponseModel GetById(int contactId)
        {
            ContactsModel contactsModel = new ContactsModel();

            //try
            //{
            using (var connection = _context)
            {
                //Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                //  ConnectionTools.ChangeDbConnection(_context, _configuration.GetConnectionString("TestDefaultConnection"));
                Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                contactsModel= repository.ReadOne(contactId);

            }
            return new ApiResponseModel() { Data = contactsModel, StatusCodes = StatusCodes.Status200OK };

            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponseModel() { Data = contactsModel, Message = ex.Message, StatusCodes = StatusCodes.Status400BadRequest };
            //}
        }

        public ApiResponseModel Update(ContactsModel contacts)
        {
            try
            {
                using (var connection = _context)
                {
                    Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                    repository.Update(contacts, e => e.Name, e => e.FirstName, e => e.LastName);
                }
                return new ApiResponseModel() { Data = contacts, Message = "Record Updated Succesfully", StatusCodes = StatusCodes.Status200OK };

            }
            catch (Exception ex)
            {
                return new ApiResponseModel() { Data = contacts, Message = ex.InnerException.Message, StatusCodes = StatusCodes.Status400BadRequest };
            }
        }

        public ApiResponseModel GetCount()
        {
            Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
            int id = Convert.ToInt32(repository.ExecuteScalar("select * from contacts where Id=1"));
            int count = Convert.ToInt32(repository.ExecuteScalar("select count(*) from contacts"));
            string name = Convert.ToString(repository.ExecuteScalar("select FirstName from contacts where Id=1"));
            return new ApiResponseModel() { Data = id, StatusCodes = StatusCodes.Status200OK };
        }

        public ApiResponseModel Exist()
        {
            Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
            if (repository.Exist("select * from contacts where Id=99999"))
            {
                return new ApiResponseModel() { Data = true };
            }
            return new ApiResponseModel() { Data = false };

        }

        public ApiResponseModel Count()
        {
            ApiResponseModel apiResponseModel=null;
            using (var connection = _context)
            {
                Repository<ContactsModel> repository = new Repository<ContactsModel>(_context);
                long total = repository.Count("Select count(*) from Contacts");
                apiResponseModel = new ApiResponseModel() { Data = total, StatusCodes = StatusCodes.Status200OK };
            }
            return apiResponseModel;
        }
    }
}
