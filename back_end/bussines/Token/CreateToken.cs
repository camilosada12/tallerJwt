using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using Entity.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bussines.Token;
public class CreateToken
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _user;

    public CreateToken(IConfiguration configuration, UserRepository user)
    {
        this._configuration = configuration;
        this._user = user;
    }
    public async Task<string> crearToken(loginDto dto)
    {
        var user = await _user.validacionUser(dto);
        if (user == null) 
        {
            throw new UnauthorizedAccessException("credenciales incorrectas");
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Name, user.Password),
                new Claim(ClaimTypes.Role, user.Rol)  // Ahora no da error porque Rol ya es string

            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtConfig = new JwtSecurityToken
        (
            claims : claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration"]))
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
    }
}

