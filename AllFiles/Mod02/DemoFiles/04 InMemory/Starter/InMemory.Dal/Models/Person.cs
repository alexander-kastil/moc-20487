using System.ComponentModel.DataAnnotations.Schema;

namespace InMemory.Dal.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}