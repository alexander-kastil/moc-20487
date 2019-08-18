using System.ComponentModel.DataAnnotations.Schema;

namespace UsingLINQtoEntity.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}