using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InMemory.Dal.Models
{
    public class Teacher : Person
    {
        public decimal Salary { get; set; }
    }
}