using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Hotel.Domain
{
    public class Parse
    {
        private readonly string _path;
        
        public Parse(string path)
        {
            _path = path;
        }

        public Parse()
        {
            _path = "wwwroot";
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

        public List<Domain.Hotel> ParseBestWesternfile()
        {
            using (StreamReader r = new StreamReader(GetLastFile("BestWestern")))
            {
                string json = r.ReadToEnd();
                var array = JsonConvert.DeserializeObject<List<HotelBestWesternJson>>(json);

                var listOfHotels = new List<Domain.Hotel>();

                foreach (var hotel in array)
                {
                    listOfHotels.Add(new Domain.Hotel()
                    {
                        AreaId = hotel.Reg,
                        Name = hotel.Name,
                        FreeRooms = hotel.LedigaRum
                    });
                }
                return listOfHotels;
            }
        }

        public List<string> GetFilePaths(string hotelCompany)
        {
            var files = Directory.GetFiles(_path);

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

            if (hotelCompany == "Scandic")
                return $"{_path}/Scandic-{latestFile.ToString("yyyy-MM-dd")}.txt";
            else
                return $"{_path}/BestWestern-{latestFile.ToString("yyyy-MM-dd")}.json";
        }

    }
}
