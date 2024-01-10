using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public class AuctionUpdated
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}