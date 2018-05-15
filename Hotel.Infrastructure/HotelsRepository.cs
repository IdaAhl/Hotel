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

        public List<Domain.Hotel> ParseScandicfile()
        {
            var listOfHotels = new List<Domain.Hotel>();
            var text = File.ReadAllLines(GetLastFile("Scandic")).ToList();

            foreach (var t in text)
            {
                var temp = t.Split(',');

                listOfHotels.Add(new Domain.Hotel()
                {
                    AreaId = Convert.ToInt32(temp[0]),
                    Name = temp[1],
                    FreeRooms = Convert.ToInt32(temp[2])
                });
            }
            return listOfHotels;
        }

        public void ImportScandicFile2(List<Domain.Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                if (!context.Hotel.Any(x => x.Name == hotel.Name && x.AreaId == hotel.AreaId))
                {
                    context.Hotel.Add(hotel);
                }
                else
                {
                    var hotelFromDB = context.Hotel.First(x => x.Name == hotel.Name && x.AreaId == hotel.AreaId);

                    hotelFromDB.FreeRooms = hotel.FreeRooms;
                    context.Hotel.Update(hotel);
                }
            }
            context.SaveChanges();
        }

        public void ImportScandicFile()
        {
            var listOfHotels = new List<Domain.Hotel>();
            var text = File.ReadAllLines(GetLastFile("Scandic")).ToList();
            foreach (var t in text)
            {
                var temp = t.Split(',');

                if (!context.Hotel.Any(x => x.Name == temp[1] && x.AreaId == Convert.ToInt32(temp[0])))
                {
                    listOfHotels.Add(new Domain.Hotel()
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

        public void ImportBestWesternFile()
        {
            using (StreamReader r = new StreamReader(GetLastFile("BestWestern")))
            {
                string json = r.ReadToEnd();

                var array = JsonConvert.DeserializeObject<List<HotelBestWesternJson>>(json);

                var listOfHotels = new List<Domain.Hotel>();

                foreach (var hotel in array)
                {

                    if (!context.Hotel.Any(x => x.Name == hotel.Name && x.AreaId == hotel.Reg))
                    {
                        listOfHotels.Add(new Domain.Hotel()
                        {
                            AreaId = hotel.Reg,
                            Name = hotel.Name,
                            FreeRooms = hotel.LedigaRum
                        });
                    }
                    else
                    {
                        var hotelToUpdate = context.Hotel.First(x => x.Name == hotel.Name && x.AreaId == hotel.Reg);

                        hotelToUpdate.FreeRooms = hotel.LedigaRum;
                        context.Hotel.Update(hotelToUpdate);
                    }
                }

                context.Hotel.AddRange(listOfHotels);
                context.SaveChanges();
            }
        }

        public List<string> GetFilePaths(string hotelCompany)
        {
            var files = Directory.GetFiles("wwwroot");

            var scandicList = new List<string>();
            var bestWesternList = new List<string>();

            foreach (var file in files)
            {
                if (file.Contains("Scandic"))
                    scandicList.Add(file);
                if (file.Contains("BestWestern"))
                    bestWesternList.Add(file);
            }

            if (hotelCompany == "Scandic")
                return scandicList;
            else //(hotel == "BestWestern")
                return bestWesternList;
        }

        public string GetLastFile(string hotelCompany)
        {
            DateTime latestFile = DateTime.MinValue;

            foreach (var file in GetFilePaths(hotelCompany))
            {
                string tempFile = Regex.Match(file, @"\d{4}-\d{2}-\d{2}").Value;

                var tempDate = DateTime.Parse(tempFile);

                if (tempDate > latestFile)
                    latestFile = tempDate;
            }

<<<<<<< HEAD
            if (hotelCompany == "Scandic")
                return $"wwwroot/Scandic-{latestFile.ToShortDateString()}.txt";
            else
                return $"wwwroot/BestWestern-{latestFile.ToShortDateString()}.json";
=======
            if (hotelCompany=="Scandic")
                return $"wwwroot/Scandic-{latestFile.ToString("yyyy-MM-dd")}.txt";
            else 
                return $"wwwroot/BestWestern-{latestFile.ToString("yyyy-MM-dd")}.json";
>>>>>>> 9bc1cc8cc1ed53279bfec74a6242bd2f9c9dadfd
        }
    }
}
