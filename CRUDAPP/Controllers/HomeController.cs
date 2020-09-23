using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDAPP.Models;

namespace CRUDAPP.Controllers
{
   
    public class HomeController : Controller
    {
        // Home Controller class, this class handles the application routes
        // along with the request that each route receive from the client side.


        // Access Data Access Layer 
        DataAccessLayer data = new DataAccessLayer();
        
        
        public ActionResult Index(string search)
        {

            List<EmployeeViewModel> empData = data.SelectDepartment();

            if (search == null)
            {
                return View(empData);
            }
            else
            {
                return View(empData.Where(x => search.ToUpper().Contains(x.employee_forename.ToUpper()) || 
                                               search.ToUpper().Contains(x.employee_surname.ToUpper())  && 
                                               search.ToUpper().Contains(x.department.ToUpper())));
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "employee_id")] Employee createEmployee)
        {

            if (ModelState.IsValid)
            {
                data.AddEmployee(createEmployee);
                return RedirectToAction("Index");
            }
            return View(createEmployee);

        }

        [HttpGet]
        public ActionResult Edit(int? employee_id)
        {
            if (employee_id == null)
            {
                throw new HttpException(404, "Employee Not Found");
            }

            Employee emp = data.retreiveEmployeeData(employee_id);

            if (employee_id == null)
            {
                throw new HttpException(404, "Employee Not Found");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int employee_id, [Bind] Employee employee)
        {
            if (employee_id != employee.employee_id)
            {
                throw new HttpException(404, "Employee Not Found");
            }

            if (ModelState.IsValid)
            {
                data.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int? employee_id)
        {
            if (employee_id == null)
            {
                throw new HttpException(404, "Employee Not Found");
            }

            Employee emp = data.retreiveEmployeeData(employee_id);

            if (emp == null)
            {
                throw new HttpException(404, "Employee Not Found");
            }
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? employee_id)
        {
            data.deleteEmployee(employee_id);
            return RedirectToAction("Index");
        }
    }
}