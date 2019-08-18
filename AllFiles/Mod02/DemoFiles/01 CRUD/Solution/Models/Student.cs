using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    public class Student : Person
    {
        public int Grade { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}