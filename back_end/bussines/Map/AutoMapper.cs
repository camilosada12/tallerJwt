using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entity.DTO;
using Entity.Model;


namespace Bussines.Map;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}
