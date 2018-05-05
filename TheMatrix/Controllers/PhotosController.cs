using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheMatrix.Data;
using TheMatrix.DTOs;
using TheMatrix.Models;

namespace TheMatrix.Controllers
{
    [Produces("application/json")]
    [Route("api/Photos")]
    public class PhotosController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _conf;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public PhotosController(IUserRepository repo, IConfiguration conf, IMapper mapper)
        {
            _repo = repo;
            _conf = conf;
            _mapper = mapper;
            Account acct = new Account(
                _conf.GetSection("CloudinarySettings:CloudName").Value,
                _conf.GetSection("CloudinarySettings:ApiKey").Value,
                _conf.GetSection("CloudinarySettings:ApiSecret").Value
                );
            _cloudinary = new Cloudinary(acct);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, AddPhotoDTO photo)
        {
            var user = await _repo.GetUser(userId);
            if (user == null)
            {
                return BadRequest("No such user");
            }
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != user.ID)
                return Unauthorized();

            var file = photo.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photo.Url = uploadResult.Uri.ToString();
            photo.PublicId = uploadResult.PublicId;

            var newPhoto = _mapper.Map<Photo>(photo);

            newPhoto.User = user;

            if (!user.Photos.Any(p => p.IsProfilePic))
                newPhoto.IsProfilePic = true;

            user.Photos.Add(newPhoto);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoReturnedDTO>(newPhoto);
                return CreatedAtRoute("GetPhoto", new { id = newPhoto.ID }, photoToReturn);
            }
            return BadRequest("Could not save photo");
        }
    }
}