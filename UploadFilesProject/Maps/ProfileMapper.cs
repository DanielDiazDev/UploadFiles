using AutoMapper;
using UploadFilesProject.Models;
using UploadFilesProject.Models.ViewModels;

namespace UploadFilesProject.Maps
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<AppUser, RegisterViewModel>().ReverseMap();
        }
    }
}
