using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Expressions;

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
    }
}
