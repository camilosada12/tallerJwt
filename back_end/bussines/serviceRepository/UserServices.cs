using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repository;
using Entity.DTO;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Bussines.serviceRepository;
public class UserServices : servicesBase<UserDto,User>
{
    private readonly DataGeneric<User> _data;

    public UserServices(DataGeneric<User> data, ILogger<UserServices> logger, IMapper mapper) : base(data, logger, mapper)
    {
        _data = data;

    }
}
