using Azure;
using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Repository.AuthRepository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ResponseDto<string>> Register(RegisterRequest request, string role)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists != null)
                {
                    _response.Status = 404;
                    _response.Message = "User already exists";
                    _response.Success = false;
                    return _response;
                }
                ApplicationUser user = new()
                {
                    Email = request.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = request.UserName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                };
                var createUserResult = await _userManager.CreateAsync(user, request.Password);
                if (!createUserResult.Succeeded)
                {
                    _response.Status = 404;
                    _response.Message = "User creation failed due to errors:";
                    _response.Success = false;
                    foreach (var error in createUserResult.Errors)
                    {
                        _response.Message += $"\n- {error.Description}";
                    }
                    return _response;
                }
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                _response.Message = "Successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Status = 500;
                _response.Message = ex.Message;
                return _response;
            }
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var _response = new LoginResponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null)
                {
                    _response.Status = 404;
                    _response.Message = "Invalid Email";
                    _response.Success = false;
                    return _response;
                }
                if(!await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    _response.Status = 404;
                    _response.Message = "Invalid Password";
                    _response.Success = false;
                    return _response;
                }
                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email!),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                string accessToken = GenerateToken(authClaims);
                string refreshToken = GenerateRefreshToken(authClaims);
                _response.AccessToken = accessToken;
                _response.RefreshToken = refreshToken;
                _response.Message = "Login Successfully";
                return _response;
            } catch(Exception ex)
            {
                _response.Success = false;
                _response.Status = 500;
                _response.Message = ex.Message;
                return _response;
            }


        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]!));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]!));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:RefreshTokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
