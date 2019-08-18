using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    public class Teacher : Person
    {
        public decimal Salary { get; set; }
    }
}