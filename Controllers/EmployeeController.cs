using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFrameworkWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        //Employee & Employees are auto-generated using EF
        public IEnumerable<Employee> Get()
        {
            using (DevDBEntities entities = new DevDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public Employee Get(int id)
        {
            using (DevDBEntities entities = new DevDBEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
