using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        //Dependency injection to allow us to have access towards our service functions!
        private readonly BlogItemService _data;
        public BlogController(BlogItemService data)
        {
            _data = data;
        }

        // Add blog Itmes
        [HttpPost("AddBlogItems")]
        public bool AddBlogItems(BlogItemModel newBlogItem)
        {
            return _data.AddBlogItem(newBlogItem);
        }
        //GetBlogItems
        [HttpGet("GetBlogItems")]
        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _data.GetAllBlogItems();
        }
        //GetBlogItemsByCategory
        [HttpGet("GetItemsByCategory/{category}")]
        public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
        {
            return _data.GetBlogItemsByCategory(category);
        }

        //GetItemsByTags
        [HttpGet("GetItemsByTag/{tag}")]
        public List<BlogItemModel> GetItemsByTag(string tag)
        {
            return _data.GetItemsByTag(tag);
        }
        
        [HttpGet("GetItemsByDate/{date}")]
        public IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _data.GetItemsByDate(date);
        }

        [HttpPut("blogUpdate")]
        public bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            return _data.UpdateBlogItems(blogUpdate);
        }
        
        //DeleteBlogItems
        [HttpPut("DeleteBlogItem/{blogToDelete}")]
        public bool DeleteBlogItem(BlogItemModel blogItemToDelete)
        {
            return _data.DeleteBlogItem(blogItemToDelete);
        }

        //GetPublishedBlogItems
        [HttpGet("GetPublishedItems")]
        public IEnumerable<BlogItemModel> GetPublishedItems()
        {
            return _data.GetPublishedItems();
        }
        //Get
    }
}