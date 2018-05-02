using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMatrix.Helpers
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            // adjust age if current year birthday hasn't happened yet
            return age;
        }
    }
}
