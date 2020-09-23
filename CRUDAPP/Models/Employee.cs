using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CRUDAPP.Models
{
    public class Employee
    {
        // Employee model

        public int employee_id { get; set; }

        [Required]
        public int employee_number { get; set; }

        [Required]
        public string employee_forename { get; set; }

        [Required]
        public string employee_surname { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2004","1/1/2020",
        ErrorMessage = "You should be above 16 years old")]
        public DateTime? dob { get; set; }

        [Range (1,3)]
        public int department_id { get; set; }
    }
}