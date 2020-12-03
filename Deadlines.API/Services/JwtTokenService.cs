using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Deadlines.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Deadlines.API.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(DbUser dbUser);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly EfContext _context;
        private readonly IConfiguration _configuration;
        public JwtTokenService(UserManager<DbUser> userManager, EfContext context,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }
        public string CreateToken(DbUser dbUser)
        {
            var roles = _userManager.GetRolesAsync(dbUser).Result;
            roles = roles.OrderBy(x => x).ToList();
            var query = _context.Users.AsQueryable();
            //var image = $"/files/{user.Image}";

            //if (image == null)
            //{
            //    image = _configuration.GetValue<string>("DefaultImage");
            //}


            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",dbUser.Id.ToString()),
                new Claim("name",dbUser.UserName),
                //new Claim("image", image)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            //var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("JwtKey")));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(1000),
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}