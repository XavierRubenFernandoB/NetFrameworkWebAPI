using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NetFrameworkWebAPI.Controllers
{
    [EnableCorsAttribute("https://localhost:44361", "*","*")]
    public class EmployeeController : ApiController
    {
        //Employee & Employees are auto-generated using EF
        //[HttpGet]
        //public IEnumerable<Employee> FetchEmployees()
        //{
        //    using (DevDBEntities entities = new DevDBEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage FetchEmployeeById(int id)
        {
            using (DevDBEntities entities = new DevDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee " + id.ToString() +  " does not exist");    
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage FetchEmployeeByGender(string gender = "All")
        {
            try
            {
                using (DevDBEntities entities = new DevDBEntities())
                {
                    switch (gender.ToLower())
                    {
                        case "all":
                            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                        case "male":
                            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                        case "female":
                            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Gender entered : " + gender.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmployee(Employee employee)
        {
            try
            {
                using (DevDBEntities entities = new DevDBEntities())
                {
                    var newemp_entity = entities.Employees.Add(employee);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, newemp_entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id, Employee employee)
        {
            try
            {
                using (DevDBEntities entities = new DevDBEntities())
                {
                    var newemp_entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (newemp_entity != null)
                    {
                        newemp_entity.FirstName = employee.FirstName;
                        newemp_entity.LastName = employee.LastName;
                        newemp_entity.Gender = employee.Gender;
                        newemp_entity.Salary = employee.Salary;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, newemp_entity);
                    }
                    else 
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Emp Id " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEmployee([FromBody] int id)
        {
            try
            {
                using (DevDBEntities entities = new DevDBEntities())
                {
                    var deleteEmp_entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (deleteEmp_entity != null)
                    {
                        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, deleteEmp_entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Emp ID " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
