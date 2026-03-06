using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Services.Context;

namespace blogAPI.Services
{
    public class BlogItemService
    {
        private readonly DataContext _context;
        public BlogItemService(DataContext context)
        {
            _context = context;
        }
        public bool AddBlogItem(BlogItemModel newBlogItem)
        {
            _context.Add(newBlogItem);
            bool result = _context.SaveChanges() != 0;
            return result;
        }

        internal bool DeleteBlogItem(BlogItemModel blogItemToDelete)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetBlogItemsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            throw new NotImplementedException();
        }

        internal List<BlogItemModel> GetItemsByTag(string tag)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            throw new NotImplementedException();
        }
    }
}