using System;
using System.Linq;
using Hotel.Domain;
using Hotel.Infrastructure;

namespace Hotel
{
    public class Validate
    {
        public bool ValidateArea(Area area)
        {
            if (area.Id == 0)
            return false;
            else if (String.IsNullOrWhiteSpace(area.Name))
                return false;
            else
                return true;
        }

        public bool ValidateAreaId(int id)
        {
            var list = new HotelsRepository().GetAreas();
            if (list.Any(x => x.Id == id))
                return true;
            else
            {
                return false;
            }
        }
    }
}
