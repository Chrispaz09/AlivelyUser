using Alively.Core.Entities;
using Alively.Core.Services;
using Alivley.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alivley.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IManageUserService _manageUserService;

        private readonly IManageAccountService _manageAccountService;

        public UserController(IMapper mapper, IManageUserService manageUserService, IManageAccountService manageAccountService)
        {
            _mapper = mapper;

            _manageUserService = manageUserService;

            _manageAccountService = manageAccountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            try
            {
                userDto.Password = _manageAccountService.SecurePassword(userDto.Password);

                var userAdded = await _manageUserService.CreateUserAsync(_mapper.Map<UserDTO, User>(userDto)).ConfigureAwait(false);

                userAdded.Password = "";

                return Ok(_mapper.Map<UserDTO>(userAdded));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid uuid)
        {
            try
            {
                var userGet = await _manageUserService.GetUserAsync(uuid).ConfigureAwait(false);

                userGet.Password = "";

                return Ok(_mapper.Map<UserDTO>(userGet));
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{uuid}")]
        public async Task<IActionResult> Put(Guid uuid, [FromBody] UserDTO userDto)
        {
            try
            {
                if(uuid ==  Guid.Empty || userDto == null)
                {
                    return BadRequest();
                }

                var user = new User();

                user = _mapper.Map<UserDTO, User>(userDto);

                user.Uuid = uuid;
               
                var userUpdate = await _manageUserService.UpdateUserAsync(user).ConfigureAwait(false);

                userUpdate.Password = "";

                return Ok(_mapper.Map<UserDTO>(userUpdate));
            }
            catch 
            {

                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            try
            {
                if(uuid == Guid.Empty)
                {
                    return BadRequest();
                }

                var result = await _manageUserService.DeleteUserAsync(uuid).ConfigureAwait(false);

                return Ok(result);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("DoesUsernameExists/{username}")]
        public async Task<IActionResult> DoesUsernameAlreadyExists(string username)
        {
            try
            {
                var result = await _manageAccountService.DoesUsernameAlreadyExist(username).ConfigureAwait(false);

                return Ok(result);
            }
            catch 
            {

                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("IsEmailRegistered/{email}")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            try
            {
                var result = await _manageAccountService.IsEmailAlreadyRegistered(email).ConfigureAwait(false);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var userUuid = await _manageAccountService.GetUserUuidByUsername(username).ConfigureAwait(false);

                if(userUuid == Guid.Empty)
                {
                    return NotFound();
                }

                var isVerified = await _manageAccountService.VerifyHashPassword(userUuid, password);

                if(isVerified)
                {
                    var userGet = await _manageUserService.GetUserAsync(userUuid).ConfigureAwait(false);

                    userGet.Password = "";

                    return Ok(_mapper.Map<UserDTO>(userGet));
                }

                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("ChangePassword/{uuid}")]
        public async Task<IActionResult> ChangeUserPassword(Guid uuid, string newPassword)
        {
            try
            {
                if(uuid == Guid.Empty || string.IsNullOrEmpty(newPassword))
                {
                    return BadRequest();
                }

                var doesUuidExist = await _manageAccountService.DoesUuidExist(uuid).ConfigureAwait(false);

                if(doesUuidExist)
                {
                    await _manageAccountService.ChangePassword(newPassword, uuid).ConfigureAwait(false);

                    return Ok();
                }

                return NotFound();
            }
            catch 
            {

                return StatusCode(500);
            }
        }
    }
}
