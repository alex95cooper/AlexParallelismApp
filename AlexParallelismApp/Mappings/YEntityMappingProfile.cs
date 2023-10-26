using AlexParallelismApp.DAL.Models;
using AlexParallelismApp.Domain.Models;
using AlexParallelismApp.Models;
using AutoMapper;

namespace AlexParallelismApp.Mappings;

public class YEntityVmMappingProfile : Profile
{
    public YEntityVmMappingProfile()
    {
        CreateMap<YEntityDto, YEntityViewModel>().ReverseMap();
    }
}

public class YEntityDtoMappingProfile : Profile
{
    public YEntityDtoMappingProfile()
    {
        CreateMap<YEntity, YEntityDto>().ReverseMap();
    }
}