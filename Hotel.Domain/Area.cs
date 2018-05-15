using System.Collections.Generic;

namespace Hotel.Domain
{
    public class Area
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Domain.Hotel> Hotels { get; set; } = new List<Domain.Hotel>();

    }
}
