using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Services.Context;
using Humanizer;
using Microsoft.AspNetCore.Http.Features;

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

        public bool DeleteBlogItem(BlogItemModel blogItemToDelete)
        {
            _context.Update(blogItemToDelete);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _context.BlogInfo;
        }

        public IEnumerable<BlogItemModel> GetBlogItemsByCategory(string category)
        {
            return _context.BlogInfo.Where(item => item.Category == category);
            //.Where filters and returns a list of items based on the condition passed in! // ? Return an Iqueriable??
        }

        public IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _context.BlogInfo.Where(item => item.Date == date );
        }

        public List<BlogItemModel> GetItemsByTag(string tag)
        {
            List<BlogItemModel> AllBlogsWithTag = new List<BlogItemModel>();
            var allItems = GetAllBlogItems().ToList();

            for(int i = 0; i < allItems.Count; i++)
            {
                BlogItemModel Item = allItems[i];
                var itemArr = Item.Tags.Split(",");
                for(int j = 0; j < itemArr.Length; j++)
                {
                    if (itemArr[j].Contains(tag))
                    {
                        AllBlogsWithTag.Add(Item);
                        break;
                    }
                }
            }
            return AllBlogsWithTag;

        }

        public bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            _context.Update(blogUpdate);
            return _context.SaveChanges() != 0;

        }

        public IEnumerable<BlogItemModel> GetPublishedItems()
        {
            return _context.BlogInfo.Where(item => item.IsPublished);
        }
    }
}