using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMatrix.Models;

namespace TheMatrix.DTOs
{
    public class AddPhotoDTO
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public User User { get; set; }
        public AddPhotoDTO()
        {
            DateAdded = DateTime.Now;
        }
    }
}
