using LinkCraft.Data;
using LinkCraft.Models;
using LinkCraft.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JADA.API.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly LinkCraftContext _context;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            LinkCraftContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize]
        [HttpGet]
        public async Task<ResultBaseModel<IUserProfile>> UserProfile()
        {
            try
            {
                var user = await _context.FindAsync<User>(User.Identity.Name);
                if(user == null)
                {
                    return new ResultBaseModel<IUserProfile>("User not found");
                }

                var artcodes = _context.Set<Experience>().Count(x => x.UserId == user.Id);
                var profile = new UserProfile
                {
                    Name = user.Name,
                    City = user.City,
                    Country = user.Country,
                    Username = user.UserName,
                    CreatedArtCodes = artcodes
                };

                return new ResultBaseModel<IUserProfile>(profile);
            }
            catch (Exception)
            {
                return new ResultBaseModel<IUserProfile>("Database error");
            }
        }

        [HttpPost]
        public async Task<ResultBaseModel<string>> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return new ResultBaseModel<string>(GenerateJwtToken(model.Email, appUser), true);
                }

                return new ResultBaseModel<string>("Wrong username and password combination");
            }
            catch (Exception)
            {
                return new ResultBaseModel<string>("Database error");
            }
        }

        [HttpPost]
        public async Task<ResultBaseModel<string>> Register([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ResultBaseModel<string>("All fields must be completed");
            }
            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    City = model.City,
                    Country = model.Country
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return new ResultBaseModel<string>(GenerateJwtToken(model.Email, user), true);
                }

                return new ResultBaseModel<string>("User with this email already exists");
            }
            catch (Exception)
            {
                return new ResultBaseModel<string>("Database error");
            }

        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                audience: _configuration["JwtIssuer"],
                issuer: _configuration["JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}