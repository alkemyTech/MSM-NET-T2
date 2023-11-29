using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;

namespace VirtualWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        private readonly VirtualWalletDbContext _context;
        public AutenticacionController(IConfiguration config, VirtualWalletDbContext context)
        {
            secretKey = config.GetSection("settings:secretkey").Value;

            _context = context;
        }

        [HttpPost]
        [Route("validar")]
        public async Task<IActionResult> Validar([FromBody] AuthModel request)
        {

            var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            var account = await _context.Accounts.FindAsync(users.Id);

            if (users == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "inexistente o inhabilitado" });
            }

            //En caso de tratarse de una cuenta bloqueada se le niega el acceso
            if (account.IsBlocked)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "La cuenta se encuentra bloqueada" });
            }

            if (EncryptPass.GetSHA256(request.Password) == users.Password && request.Email == users.Email)
            {
                var keyBytes = Encoding.UTF8.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Password.ToString()));
                claims.AddClaim(new Claim("Id", users.Id.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Role, users.Role_Id == 1 ? "Admin" : "Regular"));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado, id = users.Id });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "inexistente o inhabilitado" });
            }
        }
    }
}