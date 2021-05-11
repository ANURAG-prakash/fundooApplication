using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using EmailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly UserContext _user;
        private readonly IEmailSender emailSender;
        public UserController(IUserBL dataRepository, IConfiguration config, UserContext userContext , IEmailSender emailSender)
        {
            _userBL = dataRepository;
            _secret = config.GetSection("Jwt").GetSection("Key").Value;
            _issuer = config.GetSection("Jwt").GetSection("Issuer").Value;
            _user = userContext;
            this.emailSender = emailSender;
        }




        // POST: api/User
        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult UserRegister([FromBody] RegisterModel user)
        {
            try
            {
                var model = _user.Users.Any(x => x.Email == user.Email);

                if (user == null)
                {
                    return BadRequest("User is null.");
                }

                bool result = _userBL.UserRegister(user);
                if (model == true)
                {
                    return this.BadRequest(new { success = false, message = "User Registration Fail User Already Exist" });
                }
                else
                {
                    return this.Ok(new { success = true, message = "User Registration Done" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            try
            {

                 

                var user = _userBL.Authenticate(model.Email, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _issuer,
                    Audience = _issuer,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Id", Convert.ToString(user.UserId)),

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1440),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info and authentication token
                return Ok(new
                {
                    Id = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = "*******",
                    Token = tokenString
                }); 
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }



        [AllowAnonymous]
        [HttpPost("Resetpassword")]
        public IActionResult ForgetPassWordModel([FromBody] ForgetPasswordModel model)
        {
            try
            {



                var user = _userBL.ForgetPassWordModel(model.Email);

                if (user == null)
                    return BadRequest(new { message = "InValid Email" });
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _issuer,
                    Audience = _issuer,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Id", Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Email, user.Email)


                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1440),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
               /* var message = new Message(new string[] { "codemazetest@mailinator.com" }, "Test email", "This is the content from our email.");
                emailSender.SendEmail(message);
               */ 

                return Ok(new
                {
                    tokenString
                    
            }
                ) ;

                
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
            




