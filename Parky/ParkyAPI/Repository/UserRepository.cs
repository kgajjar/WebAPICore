using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        //Add our dependancy Injection
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            //User not found
            if (user == null)
            {
                return null;
            }
            //if user found. generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //Has all details regarding token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),//Claim type for Name
                    new Claim(ClaimTypes.Role, user.Role.ToString())//Claim type for Role

                }),
                //Set when token expires
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Add token to user object
            user.Token = tokenHandler.WriteToken(token);
            //Blank out password for response
            user.Password = "";

            return user;
        }

        public bool IsUniqueUser(string username)
        {
            //Retrieve user based on UserName
            var user = _db.Users.SingleOrDefault(x => x.Username == username);

            //return null if user not found
            if (user == null)
                return true;//This means that user is unique

            return false;


        }

        public User Register(string username, string password)
        {
            User userObj = new User()
            {
                Username = username,
                Password = password,
                Role = "Admin"
            };
            _db.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";//Set password to "" for response.

            return userObj;
        }
    }
}
