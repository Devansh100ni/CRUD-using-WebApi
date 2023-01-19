using CRUD_in_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CRUD_in_WebApi.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            List<Employee> empList = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.GetAsync("CrudApi");
            response.Wait();
            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                var dis = test.Content.ReadAsAsync<List<Employee>>();
                dis.Wait();
                empList= dis.Result;
            }

            return View(empList);
        }

        //this is the get request method
        public ActionResult Create()
        {

            return View();
        }
        //this is the post request method
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            List<Employee> empList = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.PostAsJsonAsync<Employee>("CrudApi", employee);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
               return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult Details(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var dis = test.Content.ReadAsAsync<Employee>();
                dis.Wait();
                e = dis.Result;
            }
            return View(e);
        }
        public ActionResult Edit(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var dis = test.Content.ReadAsAsync<Employee>();
                dis.Wait();
                e = dis.Result;
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.PutAsJsonAsync<Employee>("CrudApi", e);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Edit");
        }
        public ActionResult Delete(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var dis = test.Content.ReadAsAsync<Employee>();
                dis.Wait();
                e = dis.Result;
            }
            return View(e);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44384/api/CrudApi");
            var response = client.DeleteAsync("CrudApi/"+id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
