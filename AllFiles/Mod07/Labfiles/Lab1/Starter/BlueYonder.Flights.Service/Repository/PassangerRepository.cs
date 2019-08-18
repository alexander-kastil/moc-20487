using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Repository
{
    public class PassangerRepository : IPassangerRepository
    {
        public List<string> GetPassangerList()
        {
            return _passangers;
        }

        private List<string> _passangers = new List<string>
        {
            "Kevin   Liu",
            "Martin  Weber",
            "George  Li",
            "Lisa    Miller",
            "Run Liu",
            "Sean    Stewart",
            "Olinda  Turner",
            "Jon Orton",
            "Toby    Nixon",
            "Daniela Guinot",
            "Vijay   Sundaram",
            "Eric    Gruber",
            "Chris   Meyer",
            "Yuhong  Li",
            "Yan Li"
        };
    }
}
