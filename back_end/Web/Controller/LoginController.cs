using Bussines.Token;
using Entity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
   private readonly CreateToken _token;
   private readonly ILogger<LoginController> _logger;

    public LoginController(CreateToken token, ILogger<LoginController> logger)
    {
        this._token = token;
        this._logger = logger;
    }

    [HttpPost]

    public async Task<IActionResult> login([FromBody] loginDto dto)
    {
        try
        {
            var token = await _token.crearToken(dto);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = true, token });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "validacion fallida ,error al crear el token");
            return BadRequest(new { message = ex.Message });
        }
    }
}

