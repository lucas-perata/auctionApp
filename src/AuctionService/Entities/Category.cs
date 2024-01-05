using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionService.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // nav property
        public List<Item> Items { get; set; }

    }
}