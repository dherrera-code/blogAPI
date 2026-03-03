
using blogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services.Context
{
    //first inherit our DbContext from ef core!
    public class DataContext : DbContext
    {
        // from our ef core 
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        // These are now our tables we created for user's info and Blog Items info
        // This is creating tables based on the models and its variables!
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<BlogItemModel> BlogInfo { get; set; }

    }
}