﻿using CashFlow.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CashFlow.Application.Middlewares
{
    public class JwtMiddlewareOLD
    {
        private readonly RequestDelegate _next;

        public JwtMiddlewareOLD(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var (userId, roleId) = ValidateAccessToken(token);
                context.Items["UserId"] = userId;
                context.Items["RoleId"] = roleId;
            }

            await _next(context);
        }

        private (string? userId, string? roleId) ValidateAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.Secret));

                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = JwtConst.Issuer,
                    ValidAudience = JwtConst.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                var userId = jwtToken?.Claims.FirstOrDefault(x => x.Type == JwtConst.UserId)?.Value;
                var roleId = jwtToken?.Claims.FirstOrDefault(x => x.Type == JwtConst.RoleId)?.Value;
                return (userId, roleId);
            }
            catch
            {
                return (null, null);
            }
        }
    }
}
