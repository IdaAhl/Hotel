using System.Collections.Generic;
using Hotel.Domain;

namespace Hotel.Infrastructure
{
    public interface IHotelsRepository
    {
        void Seed();
        void Add(Area area);
        List<Area> GetAreas();
        void DeleteArea(int id);
        void UpdateArea(Area area);
       
        void ImportFile(List<Domain.Hotel> hotels);
    }
}
