using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsingLINQtoEntity.Models
{
    public class Teacher : Person
    {
        public decimal Salary { get; set; }
    }
}