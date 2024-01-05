using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // nav properties
        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }

        // Foreign key to Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
