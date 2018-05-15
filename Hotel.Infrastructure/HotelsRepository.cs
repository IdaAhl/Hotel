using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Hotel.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hotel.Infrastructure
{
    public class HotelsRepository : IHotelsRepository
    {
        private readonly DatabaseContext context = new DatabaseContext();
        public void Seed()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Area.Add(new Area { Name = "Göteborg Centrum", Id = 50 });
            context.Area.Add(new Area { Name = "Göteborg Hisingen", Id = 60 });
            context.Area.Add(new Area { Name = "Helsingborg", Id = 70 });

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

        

        public void ImportFile(List<Domain.Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                if (!context.Hotel.Any(x => x.Name == hotel.Name && x.AreaId == hotel.AreaId))
                    context.Hotel.Add(hotel);
                else
                {
                    var hotelFromDB = context.Hotel.First(x => x.Name == hotel.Name && x.AreaId == hotel.AreaId);
                    hotelFromDB.FreeRooms = hotel.FreeRooms;
                    context.Hotel.Update(hotel);
                }
            }
            context.SaveChanges();
        }

       

       


       

        
    }
}
