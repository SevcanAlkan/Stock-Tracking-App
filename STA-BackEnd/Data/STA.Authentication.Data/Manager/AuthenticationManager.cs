using Microsoft.IdentityModel.Tokens;
using STA.Authentication.Data.Contract;
using STA.EventBus.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STA.Authentication.Data.Manager
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly string secret;

        public AuthenticationManager(string secret)
        {
            this.secret = secret;
        }

        public TokenVM Register(UserRegistrationModel model)
        {
            return null;
        }

        public TokenVM Signin(AuthenticateModel model)
        {
            //RabbitMQ
            var user = new TokenVM(); // _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
