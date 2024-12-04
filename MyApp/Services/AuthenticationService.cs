using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApp.Data;
using MyApp.Data.Models;
using MyApp.Views.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<AplicationUser> _userManager;
        public AuthenticationService(UserManager<AplicationUser> userManager) {
            _userManager = userManager;

        }
        public async Task<List<Claim>> GenerateClaims(AplicationUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            //Add User Roles
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}