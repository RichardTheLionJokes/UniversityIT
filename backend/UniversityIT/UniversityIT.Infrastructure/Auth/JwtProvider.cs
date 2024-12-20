﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityIT.Application.Abstractions.Auth;
using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Infrastructure.Auth
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(User user)
        {
            Claim[] claims = [
                new(CustomClaims.UserId, user.Id.ToString())
            ];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
                signingCredentials: signingCredentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid GetUserIdByToken(string token)
        {
            JwtSecurityToken tokenValue = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var claimUserId = tokenValue.Claims.FirstOrDefault(
                c => c.Type == CustomClaims.UserId);

            if (claimUserId is null || !Guid.TryParse(claimUserId.Value, out Guid userId))
            {
                return Guid.Empty;
            }

            return userId;
        }
    }
}