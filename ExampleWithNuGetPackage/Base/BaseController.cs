using ENP.DA;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWithNuGetPackage.Controllers
{
    public class BaseController: ControllerBase
    {
        public IContacts Contact;
        public IEmployee Employee;
        public IStudent Student;
    }
}
