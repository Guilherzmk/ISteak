using ISteak.Commons.Results;
using ISteak.Core.SignIns;
using ISteak.Core.User;
using ISteak.Repositories.Criptographys;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Services.Users
{
    public class UserService : ResultService, IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User @params)
        {
            try
            {
                var user = await this.userRepository.InsertAsync(@params);

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> LoginAsync(SignInParams @params)
        {
            try
            {
                var user = await this.userRepository.Get(@params.AccessKey);

                var encode = Cryptography.CreateHash(@params.Password);
                string passwordEncoded = Convert.ToBase64String(encode);

                if (user is null)
                    return null;

                user.Token = GenerateToken(user);

                if (user.Password == passwordEncoded)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Hash, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
