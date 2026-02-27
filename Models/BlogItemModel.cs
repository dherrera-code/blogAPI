using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogAPI.Models
{
    public class BlogItemModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PublisherName { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        //* For date's create a dateTime object to hold the date!
        public string? Date { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}