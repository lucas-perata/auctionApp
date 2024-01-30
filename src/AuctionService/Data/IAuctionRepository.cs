using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionService.DTOs;
using AuctionService.Entities;

namespace AuctionService.Data
{
    public interface IAuctionRepository
    {
        Task<List<AuctionDto>> GetAuctionsAsync(string date);
        Task<AuctionDto> GetAuctionByIdAsync(Guid id);
        Task<Auction> GetAuctionEntityById(Guid id);
        Task<Auction> CheckCategoryExists(string categoryName);
        void AddAuction(Auction auction);   
        void RemoveAuction(Auction auction);
        Task<bool> SaveChangesAsync();
    }
}