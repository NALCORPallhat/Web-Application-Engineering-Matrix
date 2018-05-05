using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMatrix.DTOs
{
    public class UpdateUserDTO
    {
        public string City { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
    }
}
