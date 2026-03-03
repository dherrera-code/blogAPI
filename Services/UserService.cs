using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogAPI.Models.DTO;
using blogAPI.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services
{
        //now we want to inherit from our DBcontext
    public class UserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        //create helper functions to create our users A bit into salt and hash!
        internal bool AddUser(CreateAccountDTO userToAdd)
        {
            //internal bool???
            throw new NotImplementedException();
        }

    }
}