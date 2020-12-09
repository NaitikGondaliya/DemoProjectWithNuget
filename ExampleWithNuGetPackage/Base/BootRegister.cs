using ENP.DA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWithNuGetPackage.Controllers
{
    public static class BootRegister
    {

        public static void AddMyServices(
               this IServiceCollection services)
        {
            services.AddScoped<IContacts, Contacts>();
            services.AddScoped<IEmployee, Employee>();
            services.AddScoped<IStudent, Student>();
        }

    }
}
