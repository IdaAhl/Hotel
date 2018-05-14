using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var listAreas = context.Area.Include(x => x.Hotels).ToList();
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
            var text = File.ReadAllLines(GetLastScandicFile()).ToList();
            foreach (var t in text)
            {
                var temp = t.Split(',');

                if (!context.Hotel.Any(x => x.Name == temp[1] && x.AreaId == Convert.ToInt32(temp[0])))
                {
                    listOfHotels.Add(new Hotel()
                    {
                        AreaId = Convert.ToInt32(temp[0]),
                        Name = temp[1],
                        FreeRooms = Convert.ToInt32(temp[2])
                    });
                }

                else
                {
                    var hotel = context.Hotel.First(x => x.Name == temp[1] && x.AreaId == Convert.ToInt32(temp[0]));

                    hotel.FreeRooms = Convert.ToInt32(temp[2]);
                    context.Hotel.Update(hotel);
                }
            }
            context.Hotel.AddRange(listOfHotels);
            context.SaveChanges();
        }

        public string GetLastScandicFile()
        {
            var files = Directory.GetFiles("wwwroot");

            

            DateTime latestFile = DateTime.MinValue;

            foreach (var file in files)
            {
                string tempFile = file.Replace(".txt", "");
                var tempFile2 = tempFile.Replace(@"wwwroot\Scandic-", "");

                var tempDate = DateTime.Parse(tempFile2);

                if (tempDate > latestFile)
                    latestFile = tempDate;
            }

            return $"wwwroot/Scandic-{latestFile.ToShortDateString()}.txt";
        }
    }
}
