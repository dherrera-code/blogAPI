using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Models.DTO;
using blogAPI.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services
{
        //now we want to inherit from our DBcontext
    public class UserService
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
            //create a hash versed argument
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

    }
}