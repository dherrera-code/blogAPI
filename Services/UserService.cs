using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Models.DTO;
using blogAPI.Services.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace blogAPI.Services
{
        //now we want to inherit from our DBcontext
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        //gives us access to our database!!
        public UserService(DataContext context)
        {
            _context = context;
        }

        //! Helper Method :
        //* We need a helper method to check if our user exist in our database!
        public bool DoesUserExist(string username)
        {
            //check our tables to see if the user name exist!
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
            //this function checks our tables if username passed in exist or not! It will return a bool!
        }

        // ! Helper method end!
        //create helper functions to create our users A bit into salt and hash!
        public bool AddUser(CreateAccountDTO userToAdd)
        {
            bool result = false;

            if(userToAdd.Username != null && !DoesUserExist(userToAdd.Username)) {
                UserModel newUser = new UserModel();

                var HashedPassword = HashPassword(userToAdd.Password);

                newUser.Id = userToAdd.Id;
                newUser.Username = userToAdd.Username;
                newUser.Salt = HashedPassword.Salt;
                newUser.Hash = HashedPassword.Hash;

                _context.Add(newUser);
                result = _context.SaveChanges() != 0; //Checks if something is in our database
                // When we write into our database, it should 
            }
            return result;
            //we are going to need Hash helper function to help us hash our password
            // we are going to need to set our newUser.Id = UserToAdd.Id
            //Username
            //Salt
            //Hash

            //then we add it to our DataContext
            // Save our changes
            // return a bool to return true or false;
        }

        //Function that will help hash our password
        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();

            byte[] SaltBytes = new byte[64];

            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(SaltBytes); 

            var Salt = Convert.ToBase64String(SaltBytes);
            //create a hash versed argument: Created our salt string!
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000,HashAlgorithmName.SHA256);
                                                                        //The higher the bytes the better the encryption!
            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        //Helper function to verify Password!

        public bool VerifyUserPassword(string? password, string? StoredHash, string? StoredSalt)
        {
            if(StoredSalt == null) {
                return false;
            }

            var SaltBytes = Convert.FromBase64String(StoredSalt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == StoredHash;

        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }
// what is the internal! mean!
        public IActionResult Login(LoginDTO user)
        {
            //Check if the user exist! 
            IActionResult result = Unauthorized();
            if (DoesUserExist(user.Username))
            {
                //create a secret key used to sign the JWT Token
                //This key should be stored securely (not hard coded in production) //Once we hit our 256 range for our passed string we are good
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersupersuperduppersecurekey@34456789"));
                //Create signing credentials using the secretkey and an algorithm: HMACSHA256
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); //This ensures the token can't be tampered with

                //Build the JWT toke with metadata
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(), 
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );

                //Convert the token object into string that can be sent to the client!
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                //return the token as JSON to the client
                result = Ok(new {Token = tokenString});
            }
            //return either the token (if user exist) or unauthorized (if user does not exist)!
            return result;
        }

        internal UserIdDTO GetUserIdDTOByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}