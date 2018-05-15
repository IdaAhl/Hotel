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
        void ImportScandicFile();
        void ImportBestWesternFile();

        List<Domain.Hotel> ParseScandicfile();
        void ImportScandicFile2(List<Domain.Hotel> hotels);
    }
}
