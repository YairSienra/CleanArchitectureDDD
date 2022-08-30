
using CleanArchitecture.Application.Constace;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe.");
            }

         var retultado =  await _signInManager.PasswordSignInAsync(user.Email, request.Password, false, lockoutOnFailure: false);
            if (retultado.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await ObtenerToken(user);
            var authResponse = new AuthResponse()
            {
                Id = user.Id,
                Name = user.Nombre,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Registro(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if(existingUser != null)
            {
                throw new Exception($"Ya existe un usuario con este dato");
            }
            var ExistingEmail = await _userManager.FindByEmailAsync(request.Email);
            if(ExistingEmail != null)
            {
                throw new Exception($"Ya existe un usuario con ese email");
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                Nombre = request.Name,
                Apellido = request.Apellido,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(user, request.Password);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operador");
                var token = await ObtenerToken(user);
                return new RegistrationResponse()
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Id = user.Id,
                    Name = user.UserName,
                };
            }

            throw new Exception($"{resultado.Errors}");
        }

        private async Task<JwtSecurityToken> ObtenerToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaim = new List<Claim>();

            foreach (var ro in roles)
            {
                roleClaim.Add(new Claim(ClaimTypes.Role, ro));
            }

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Nombre),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimsTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signIngCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims : Claims,
                expires : DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials : signIngCredentials);

            return jwtSecurityToken;
        }
    }
}
