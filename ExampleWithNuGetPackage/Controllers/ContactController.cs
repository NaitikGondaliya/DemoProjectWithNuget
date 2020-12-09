using ENP.DA;
using ENP.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWithNuGetPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        public ContactController(IContacts contact)
        {
            Contact = contact;
        }

        [HttpPost]
        public IActionResult Add(ContactsModel contactsModel)
        {
            return Ok(Contact.Add(contactsModel));
        }


        [HttpPut]
        public IActionResult Update(ContactsModel contacts)
        {
            return Ok(Contact.Update(contacts));
        }


        [HttpGet("{pageNo:int}/{pageSize:int}")]
        [HttpGet("{pageNo:int}/{pageSize:int}/orderBy")]
        public IActionResult GetAll(int pageNo, int pageSize, string orderBy)
        {
            return Ok(Contact.GetAllPagination(pageNo, pageSize, orderBy));
        }

        [HttpGet("/Query/{pageNo:int}/{pageSize:int}/{orderBy}")]
        public IActionResult GetAllQuery(int pageNo, int pageSize, string orderBy)
        {
            return Ok(Contact.GetAllPaginationByQuery(pageNo, pageSize, orderBy));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {            
            return Ok(Contact.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(Contact.Delete(id));
        }

        [HttpGet("ExecuteMethod")]
        public IActionResult ExecuteMethod()
        {
            return Ok(Contact.GetCount());
        }

        [HttpGet("Exist")]
        public IActionResult Exist()
        {
            return Ok(Contact.Exist());
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(Contact.Count());
        }
    }
}