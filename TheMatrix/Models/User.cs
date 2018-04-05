using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMatrix.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] Salt { get; set; }
    }
}