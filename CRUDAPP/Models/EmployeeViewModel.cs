using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPP.Models
{
    public class EmployeeViewModel
    {
        // Employee view model, in order to have a specific model 
        // for representing employee data on the client side

        public int employee_id { get; set; }

        [Display(Name = "Employee Number")]
        public int employee_number { get; set; }

        [Display(Name = "Employee Forename")]
        public string employee_forename { get; set; }

        [Display(Name = "Employee Surname")]
        public string employee_surname { get; set; }

        [Display(Name = "Employee Date of Birth")]
        public DateTime dob { get; set; }

        [Display(Name = "Employee Department")]
        public string department { get; set; }
    }
}