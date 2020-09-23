using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CRUDAPP.Models
{
    public class DataAccessLayer
    {
        // Data access layer class has all the methods that connect and communicate
        // with the database in order to retreive and send data


        // Connection string in order to achive the connection with the db server and the current application db
        string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=trade_interchange;Data Source=LAPTOP-TRHOLFTO\\Ben";


        public List<Employee> GetAllEmployees()
        {
            // Method that retreive all the employee data based on the Employee model
            List<Employee> employee_list = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {


                string sqlCommand = "Select  * from employee";
                SqlCommand cmd = new SqlCommand(sqlCommand, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.employee_id = Convert.ToInt32(rdr["employee_id"]);
                    employee.employee_number = Convert.ToInt32(rdr["employee_number"]);
                    employee.employee_forename = rdr["employee_forename"].ToString();
                    employee.employee_surname = rdr["employee_surname"].ToString();
                    employee.dob = (DateTime)rdr["dob"];
                    employee.department_id = Convert.ToInt32(rdr["department_id"]);

                    employee_list.Add(employee);
                }
                con.Close();

            }
            return employee_list;
        }


        public List<EmployeeViewModel> SelectDepartment()
        {
            // Method that retreives employee data based on the EmployeeViewModel model
            var employees = new List<EmployeeViewModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
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
            return employees;


        }

        public void AddEmployee(Employee employee)
        {
            // Method for adding an employee

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("add_employee", con);
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@employee_number", employee.employee_number);
                cmd.Parameters.AddWithValue("@employee_forename", employee.employee_forename);
                cmd.Parameters.AddWithValue("@employee_surname", employee.employee_surname);
                cmd.Parameters.AddWithValue("@dob", employee.dob);
                cmd.Parameters.AddWithValue("@department_id", employee.department_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            // Method for updating data of an existing employee

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update_employee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employee_id", employee.employee_id);
                cmd.Parameters.AddWithValue("@employee_number", employee.employee_number);
                cmd.Parameters.AddWithValue("@employee_forename", employee.employee_forename);
                cmd.Parameters.AddWithValue("@employee_surname", employee.employee_surname);
                cmd.Parameters.AddWithValue("@dob", employee.dob);
                cmd.Parameters.AddWithValue("@department_id", employee.department_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Employee retreiveEmployeeData(int? employee_id)
        {
            // Method for retreiving data for a specific employee by id

            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQueryForEmployee = "Select * from employee where employee_id = " + employee_id;
                SqlCommand cmd = new SqlCommand(sqlQueryForEmployee, con);

                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {

                    employee.employee_number = Convert.ToInt32(sqlDataReader["employee_number"]);
                    employee.employee_forename = sqlDataReader["employee_forename"].ToString();
                    employee.employee_surname = sqlDataReader["employee_surname"].ToString();
                    employee.dob = (DateTime)sqlDataReader["dob"];
                    employee.department_id = Convert.ToInt32(sqlDataReader["department_id"]);
                }
            }
            return employee;
        }

        public void deleteEmployee(int? employee_id)
        {
            // Method for deleting an employee by id

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete_employee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employee_id", employee_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


       

    }

}
