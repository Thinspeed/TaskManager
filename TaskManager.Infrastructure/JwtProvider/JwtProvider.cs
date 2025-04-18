﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.JwtProvider;

public class JwtProvider(JwtOptions options) : IJwtProvider
{
    public string GenerateToken(User user)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
            new (ClaimTypes.Name, user.Name),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        ];
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddHours(options.ExpireInHours));

        string result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }

    public object GetUserInfo(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        
        return new { userId, userName };
    }
}