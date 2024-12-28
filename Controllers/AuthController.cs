using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTechShop.API.Models.Domain;
using StudentTechShop.API.Models.DTOs;
using StudentTechShop.API.Repositories;
using System.Web;

namespace StudentTechShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly Repositories.IEmailSender emailSender;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository,
            Repositories.IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailSender = emailSender;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            // Check if the username already exists
            var existingUserByUsername = await userManager.FindByNameAsync(registerRequestDto.Username);
            if (existingUserByUsername != null)
            {
                return BadRequest("Username is already taken.");
            }

            // Check if the email already exists
            var existingUserByEmail = await userManager.FindByEmailAsync(registerRequestDto.Email);
            if (existingUserByEmail != null)
            {
                return BadRequest("Email is already in use.");
            }


            var applicationUser = new ApplicationUser
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
                PhoneNumber = registerRequestDto.PhoneNumber,
             //   IBAN = registerRequestDto.IBAN
            };
            


            var identityResult = await userManager.CreateAsync(applicationUser, registerRequestDto.Password);
            
            if (identityResult.Succeeded)
            {
                // Add roles to this User
                var roles = new List<string>()
               {
                   "User"
               };
                    identityResult = await userManager.AddToRolesAsync(applicationUser, roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                
            }

            return BadRequest("Somthing went wrong");
        }


        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password incorrect");
        }



        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
        {

           


            var user = await userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
            if (user == null)
            {
                return BadRequest("User with the given email does not exist.");
            }

            // Generate Password Reset Token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Construct reset link
            var resetLink = $"{Request.Scheme}://{Request.Host}/api/Auth/ResetPassword?token={HttpUtility.UrlEncode(token)}&email={user.Email}";

            // Send email
            // (Replace this with your actual email sending logic)
            await emailSender.SendEmailAsync(user.Email, "Password Reset Request",
                $"Click the link to reset your password: {resetLink}");

            return Ok("Password reset link has been sent to your email.");
        }



        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var token = HttpUtility.UrlDecode(resetPasswordRequestDto.Token);

            var user = await userManager.FindByEmailAsync(resetPasswordRequestDto.Email);
            if (user == null)
            {
                return BadRequest("Invalid Email.");
            }

            var result = await userManager.ResetPasswordAsync(user, token, resetPasswordRequestDto.Password);

            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest("Failed to reset password. Please check the token and try again.");
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Retrieve the user by ID
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Map user details to a DTO (optional, for better control of exposed data)
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
               // IBAN = user.IBAN
            };

            return Ok(userDto);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            // Retrieve the user by ID
            var user = await userManager.Users.SingleOrDefaultAsync(x => x.Id == id.ToString());

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user fields
            user.FirstName = updateUserRequestDto.FirstName ?? user.FirstName;
            user.LastName = updateUserRequestDto.LastName ?? user.LastName;
            user.UserName = updateUserRequestDto.Username ?? user.UserName;
            user.Email = updateUserRequestDto.Email ?? user.Email;
            user.PhoneNumber = updateUserRequestDto.PhoneNumber ?? user.PhoneNumber;
          //  user.IBAN = updateUserRequestDto.IBAN ?? user.IBAN;


            // Update user in database
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("User updated successfully.");
            }

            return BadRequest("Failed to update user.");
        }


        /*[HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("User not logged in.");
            }

            var result = await userManager.ChangePasswordAsync(user, changePasswordRequestDto.OldPassword, changePasswordRequestDto.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password updated successfully.");
            }

            return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));
        }*/


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Retrieve the user by ID
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Delete the user
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }


    }
}
