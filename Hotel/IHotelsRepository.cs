using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel
{
    public interface IHotelsRepository
    {
        void Seed();
        void Add(Area area);
        List<Area> GetAreas();
        void DeleteArea(Area area);
    }
}
