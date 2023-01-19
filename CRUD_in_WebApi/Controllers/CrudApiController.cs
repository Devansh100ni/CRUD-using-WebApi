using CRUD_in_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace CRUD_in_WebApi.Controllers
{
    public class CrudApiController : ApiController
    {
        CRUD_DBEntities1 db = new CRUD_DBEntities1();
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            List<Employee> empList = db.Employees.ToList();
            return Ok(empList);
        }
        [HttpPost]
        public IHttpActionResult PostEmployees(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            var data = db.Employees.Where(x => x.id == id).FirstOrDefault();
            return Ok(data);
        }
        [HttpPut]
        public IHttpActionResult Edit(Employee e)
        {
            try
            {
                {
                    //var emp = db.Employees.Where(x => x.id == e.id).FirstOrDefault();
                    //if (emp != null)
                    //{
                    //    emp.id = e.id;
                    //    emp.name = e.name;
                    //    emp.gender = e.gender;
                    //    emp.designation = e.designation;
                    //    emp.age = e.age;
                    //    emp.salary = e.salary;
                    //    db.SaveChanges();
                    //}
                    //else
                    //{
                    //    return NotFound();
                    //}
                }
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var data = db.Employees.Where(x => x.id == id).FirstOrDefault();
                if (data != null)
                {
                    db.Entry(data).State = EntityState.Deleted; 
                    db.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }
    }
}
