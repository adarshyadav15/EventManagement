using AutoMapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using EventManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public UsersController(IMapper mapper,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this.mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            var user = mapper.Map<User>(newUser);
            var result = await _userRepository.CreateUserAsync(user);


            return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO updatedUser)
        {
            if (updatedUser == null || updatedUser.UserId != id)
            {
                return BadRequest();
            }
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            var result = await _userRepository.UpdateUserAsync(user);
            var userDTO = mapper.Map<UserDTO>(result);
            return Ok(userDTO);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
        {
            

            var result = await _userRepository.DeleteUserAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok($"User with id : {id} was deleted successfully");
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }
    }
}