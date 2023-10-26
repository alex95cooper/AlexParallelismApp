using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Models;
using AlexParallelismApp.Models;
using AutoMapper;

namespace AlexParallelismApp.Mappings;

public class XEntityVmMappingProfile : Profile
{
    public XEntityVmMappingProfile()
    {
        CreateMap<XEntityDto, XEntityViewModel>().ReverseMap();
    }
}

public class XEntityDtoMappingProfile : Profile
{
    public XEntityDtoMappingProfile()
    {
        CreateMap<XEntity, XEntityDto>().ReverseMap();
    }
}