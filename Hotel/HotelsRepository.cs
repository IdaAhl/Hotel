using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel
{
    public class HotelsRepository : IHotelsRepository
    {
        private readonly DatabaseContext context = new DatabaseContext();
        public void Seed()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Area.Add(new Area {Name = "Göteborg Centrum", Id = 50});
            context.Area.Add(new Area {Name = "Göteborg Hisingen", Id = 60});
            context.Area.Add(new Area {Name = "Helsingborg", Id = 70});

            context.SaveChanges();
        }

        public void Add(Area area)
        {
            context.Area.Add(area);
            context.SaveChanges();
        }

        public List<Area> GetAreas()
        {
            var listAreas = context.Area.ToList();
            return listAreas;
        }

        public void DeleteArea(int id)
        {
            var area = context.Area.First(n => n.Id == id);
            context.Area.Remove(area);
            context.SaveChanges();
        }

        public void UpdateArea(Area area)
        {
            context.Area.Update(area);
            context.SaveChanges();
        }

        public void ImportScandicFile()
        {
            var listOfHotels = new List<Hotel>();
            var text = File.ReadAllLines("Scandic-2018-03-20.txt").ToList();
            foreach (var t in text)
            {
                var temp = t.Split(',');

                listOfHotels.Add(new Hotel()
                {
                    AreaId = Convert.ToInt32(temp[0]),
                    Name = temp[1],
                    FreeRooms = Convert.ToInt32(temp[2])
                });
            }
            context.Hotel.AddRange(listOfHotels);
            context.SaveChanges();
        }
    }
}
