using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsingLINQtoEntity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}