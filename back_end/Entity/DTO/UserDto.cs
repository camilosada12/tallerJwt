using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enum;

namespace Entity.DTO;

    public class UserDto
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string username { get; set; } 
        public string password { get; set; }

        public string Rol { get; set; }
    }


