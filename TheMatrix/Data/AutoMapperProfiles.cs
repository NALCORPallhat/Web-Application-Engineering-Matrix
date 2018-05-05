using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMatrix.DTOs;
using TheMatrix.Helpers;
using TheMatrix.Models;

namespace TheMatrix.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, BriefUserDTO>().ForMember(dest => dest.ProfilePhotoURL, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsProfilePic).Url);
            }).ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.BirthDate.CalculateAge());
            });

            CreateMap<User, DetailUserDTO>().ForMember(dest => dest.ProfilePhotoURL, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsProfilePic).Url);
            }).ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.BirthDate.CalculateAge());
            });

            CreateMap<Photo, PhotoDTO>();

            CreateMap<RegisterUserDTO, User>();

            CreateMap<User, UpdateUserDTO>();
        }
    }
}
