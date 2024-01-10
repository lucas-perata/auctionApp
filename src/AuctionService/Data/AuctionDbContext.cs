using System;
using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
            
        }    

        public DbSet<Auction> Auctions {get; set;}
        public DbSet<Category> Category {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity(); 
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
