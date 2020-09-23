using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUDAPP;
using CRUDAPP.Controllers;
using CRUDAPP.Models;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;

namespace CRUDAPP.Tests.Controllers
{
   
    [TestClass]
    public class ForDataAceess
    {
        DataAccessLayer data = new DataAccessLayer();

        [TestMethod]
        public void test_check_db_connection_and_retreive_data()
        {
            // This test is implemnted in order to check if the application have a succesfully
            // connection with the db and if we can retreive data

            string correctString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=trade_interchange;Data Source=LAPTOP-TRHOLFTO\\Ben";

            var employees = new List<EmployeeViewModel>();

            using (SqlConnection con = new SqlConnection(correctString))
            {


                string sqlCommand = "select e.employee_id, e.employee_number, e.employee_forename, e.employee_surname, e.dob, d.department_name  from employee as e Join department as d on e.department_id = d.department_id";
                SqlCommand cmd = new SqlCommand(sqlCommand, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeViewModel empModel = new EmployeeViewModel();

                    empModel.employee_id = Convert.ToInt32(rdr["employee_id"]);
                    empModel.employee_number = Convert.ToInt32(rdr["employee_number"]);
                    empModel.employee_forename = rdr["employee_forename"].ToString();
                    empModel.employee_surname = rdr["employee_surname"].ToString();
                    empModel.dob = (DateTime)rdr["dob"];
                    empModel.department = rdr["department_name"].ToString();

                    employees.Add(empModel);
                }
                con.Close();
            }

            Assert.IsNotNull(employees);
        }

        [TestMethod]
        public void test_retreive_correct_userById()
        {
            // Test for checking if the datase send the correct requested data 
            // to the application

            var employeeDataDb = data.retreiveEmployeeData(18);
            
            Employee hardcodedEmp = new Employee();
            hardcodedEmp.employee_id = 18;
            hardcodedEmp.employee_number = 102;
            hardcodedEmp.employee_forename = "Chris";
            hardcodedEmp.employee_surname = "Athan";
            hardcodedEmp.dob = Convert.ToDateTime("11/09/1994");
            hardcodedEmp.department_id = 3;

            Assert.AreEqual(hardcodedEmp.employee_surname, employeeDataDb.employee_surname);

        }

        [TestMethod]
        public void test_retreive_correct_employee_data()
        {
            // Test for checking if all the requested data 
            // are correct based on the employees data that are stored
            
            List<Employee> empListdb = data.GetAllEmployees();
            List<Employee> hardcoded = new List<Employee>();

            Employee empmodel = new Employee();
            empmodel.employee_id = 14;
            empmodel.employee_number = 555;
            empmodel.employee_forename = "ccc";
            empmodel.employee_surname = "cc";
            empmodel.dob = Convert.ToDateTime("11/09/1994 00:00:00");
            empmodel.department_id = 2;

            Employee empmodel2 = new Employee();
            empmodel.employee_id = 15;
            empmodel.employee_number = 667;
            empmodel.employee_forename = "bbb";
            empmodel.employee_surname = "bb";
            empmodel.dob = Convert.ToDateTime("11/09/1990 00:00:00");
            empmodel.department_id = 3;

            hardcoded.Add(empmodel2);
            hardcoded.Add(empmodel);

            Assert.AreEqual(empListdb[1].department_id, hardcoded[1].department_id);

        }

    }

}
