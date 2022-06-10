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
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                user.Password = _manageAccountService.SecurePassword(user.Password);

                var userAdded = await _manageUserService.CreateUserAsync(user).ConfigureAwait(false);

                return Ok(_mapper.Map<UserDTO>(userAdded));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var userGet = await _manageUserService.GetUserAsync(id).ConfigureAwait(false);

                return Ok(_mapper.Map<UserDTO>(userGet));
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                var userUpdate = await _manageUserService.UpdateUserAsync(user).ConfigureAwait(false);

                return Ok(_mapper.Map<UserDTO>(userUpdate));
            }
            catch 
            {

                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _manageUserService.DeleteUserAsync(id).ConfigureAwait(false);

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
    }
}
