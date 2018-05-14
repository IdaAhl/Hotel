using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel
{
    public class Area
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}
