using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetList(CancellationToken token)
        {
            try
            {
                var users = await _userService.GetUserList(token);
                return Ok(users);
            }
            catch(Exception ex)
            {
                return NoContent();
            }
        }

        [Authorize]
        [Route("/[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            try
            {
                var user = await _userService.GetUser(id, token);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]UserDTO request, CancellationToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = await _userService.UserSignUp(request, token);
                    return Ok(userId);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id,[FromBody]UserDTO request, CancellationToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.EditUser(id ,request, token);
                    return Ok(user);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                var result = await _userService.DeleteUser(id, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDTO request, CancellationToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var identity = await _userService.UserSignIn(request, token);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));
                    var UserId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return Ok(UserId);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
