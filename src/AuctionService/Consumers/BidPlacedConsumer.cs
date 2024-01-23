using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDbContext _dbContext;

        public BidPlacedConsumer(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<BidPlaced> dbContext)
        {
            Console.WriteLine("--> Consuming bid placed event"); 

           var auction = await _dbContext.Auctions.FindAsync(dbContext.Message.AuctionId);

           if(auction.CurrentHighBid == null || dbContext.Message.BidStatus.Contains("Accepted") && dbContext.Message.Amount > auction.CurrentHighBid)
           {
            auction.CurrentHighBid = dbContext.Message.Amount;
            await _dbContext.SaveChangesAsync();
           }

        }
    }
}