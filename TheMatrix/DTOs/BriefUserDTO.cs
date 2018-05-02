using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMatrix.DTOs
{
    public class BriefUserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string ProfilePhotoURL { get; set; }
    }
}
