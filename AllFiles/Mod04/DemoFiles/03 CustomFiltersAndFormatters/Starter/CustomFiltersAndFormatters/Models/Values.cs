using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CustomFiltersAndFormatters.Models
{
    public class Value
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [IgnoreDataMember]
        public string Thumbnail { get; set; }
    }
}
