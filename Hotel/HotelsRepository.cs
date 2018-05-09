using System;
using System.Collections.Generic;
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
    }
}
