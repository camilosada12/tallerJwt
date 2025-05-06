using Bussines.serviceRepository;
using Entity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        private readonly ILogger<UserServices> _logger;

        public  UserController(UserServices userServices, ILogger<UserServices> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }

        [HttpGet]
        

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var user = await _userServices.GetAll();
                return Ok(user);
            }
            catch (Exception ex) 
            {
                _logger.LogError("error al obtener todos las personas");
                return StatusCode(500, new {message = ex.Message });
            }
        }

        [HttpGet("{id}")]


        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userServices.GetAllById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("error al obtener todos las personas");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]


        public async Task<IActionResult> create([FromBody] UserDto dto)
        {
            try
            {
                var created = await _userServices.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError("error al obtener todos las personas");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]


        public async Task<IActionResult> update(UserDto dto)
        {
            try
            {
                var created = await _userServices.updateAsync(dto);
                return Ok(new {message = "actualizacion correcta "});
            }
            catch (Exception ex)
            {
                _logger.LogError("error al obtener todos las personas");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await _userServices.delete(id);
                return Ok(new { message = "dato eliminado correcta " });
            }
            catch (Exception ex)
            {
                _logger.LogError("error al obtener todos las personas");
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
