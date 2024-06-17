using AutoMapper;
using VietAgrisell.Data;
using VietAgrisell.ViewModels;

namespace VietAgrisell.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterViewModel, User>();
                //.ForMember(u => u.Name, option => option.MapFrom(RegisterViewModel =>
                //RegisterViewModel.Name)).ReverseMap();
        }
    }
}
