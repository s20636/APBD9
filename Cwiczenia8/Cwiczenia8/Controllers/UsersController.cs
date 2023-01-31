using Cwiczenia8.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cwiczenia8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO request)
        {
            var hasher = new PasswordHasher<LoginDTO>();
            var hashedPassword = hasher.HashPassword(request, request.Password);
            var claims = new Claim[]
                {
                    new("Custom", "SomeData")
                };
            var secret = "asdhfgop;dsiahngadfo[rwe40gfegjnm02493jg02ngsdcl;fngvfj-0923jf230-jsd0-fjcmdl;'fdjksaf0-23jjfdpsfjkdsp[ojfj[foipsdj";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var options = new JwtSecurityToken("http://localhost:5262", "http://localhost:5262",
                claims, expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds);

            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

                return Ok(new 
                { 
                    Token = new JwtSecurityTokenHandler().WriteToken(options),
                    refreshToken
                });
        }
    }
}
