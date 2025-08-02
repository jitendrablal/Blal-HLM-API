using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BlalApi.Authentication
{
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        private static readonly string Secret = ConfigurationManager.AppSettings["JwtSettings:Key"];
        private static readonly string Issuer = ConfigurationManager.AppSettings["JwtSettings:Issuer"];
        private static readonly string Audience = ConfigurationManager.AppSettings["JwtSettings:Audience"];
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
                return false;

            var token = authHeader.Parameter;
            if (string.IsNullOrEmpty(token))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                //ValidateIssuer = true,
                //ValidateAudience = true,
                //ValidateLifetime = true,
                //ValidateIssuerSigningKey = true,
                //ValidIssuer = Issuer,
                //ValidAudience = Audience,
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                //ClockSkew = TimeSpan.Zero

                ValidateIssuer = false,
                ValidateAudience = false, 
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                    HttpContext.Current.User = principal;

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
            {
                message = "Unauthorized access - invalid or missing token"
            });
        }
    }
}