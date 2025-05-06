using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTO;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : DataGeneric<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> validacionUser(loginDto dto)
        {
            bool sucess = false;

            var user = await _context.Set<User>().FirstOrDefaultAsync(u =>
                u.Username == dto.username &&
                u.Password == dto.password &&
                u.Rol == dto.Role
            );

            sucess = (user != null) ? true :throw new UnauthorizedAccessException("credenciales Incorrectas");

            return user;
        }
    }
}
