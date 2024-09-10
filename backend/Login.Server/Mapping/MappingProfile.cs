using AutoMapper;
using Login.Server.DTOs;
using Login.Server.Models;

namespace Login.Server.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}
