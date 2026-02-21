using AutoMapper;
using ReactCRUDSupport_v1.Models.Domain;
using ReactCRUDSupport_v1.Models.DTOs.User;
using ReactCRUDSupport_v1.Models.DTOs.Values;

namespace ReactCRUDSupport_v1.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddValuesRequestDto, AddDomain>().ReverseMap();
            CreateMap<UpdateTotalRequestDto, AddDomain>().ReverseMap();
        }
    }
}
