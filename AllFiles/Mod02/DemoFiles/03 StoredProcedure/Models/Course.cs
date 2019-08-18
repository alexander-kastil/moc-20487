using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UsingLINQtoEntity.Models
{
    public class Course
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CourseId { get; set; }
        public virtual string Name { get; set; }
        public virtual Teacher CourseTeacher { get; set; }
        public virtual List<Student> Students { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Course Id: {CourseId}, Name: {Name}");
            sb.AppendLine($"Teacher name: {CourseTeacher.Name}, Salary: {CourseTeacher.Salary}");
            sb.AppendLine("Students:");

            foreach (var item in Students)
            {
                sb.AppendLine($"\tStudent name: {item.Name}");
            }

            return sb.ToString();
        }
    }
}