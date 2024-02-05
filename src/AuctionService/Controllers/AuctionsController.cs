using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionsController(IAuctionRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
    {
        return await _repo.GetAuctionsAsync(date);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _repo.GetAuctionByIdAsync(id);

        if (auction == null) return NotFound(); 
        
        return auction; 
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        // var checkCategory = await _repo.CheckCategoryExists(auctionDto.CategoryName);
        var category = await _repo.GetCategoryByName(auctionDto.CategoryName);
       
        if(category == null)
        {
            return BadRequest("Invalid Category");
        }

        var auction = _mapper.Map<Auction>(auctionDto);

        auction.Item.CategoryId = category.Id;
        
        auction.Seller = User.Identity.Name; 

        _repo.AddAuction(auction); 

        var newAuction = _mapper.Map<AuctionDto>(auction); 

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction)); 

        var result = await _repo.SaveChangesAsync(); 

        if(!result) return BadRequest("Could not save changes");

        return CreatedAtAction(nameof(GetAuctionById), 
        new{auction.Id}, _mapper.Map<AuctionDto>(auction));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _repo.GetAuctionEntityById(id);
        
        if(auction == null) return NotFound(); 

        if(auction.Seller != User.Identity.Name) return Forbid();

        auction.Item.Title = updateAuctionDto.Title ?? auction.Item.Title; 
        auction.Item.Description = updateAuctionDto.Description ?? auction.Item.Description;

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(auction)); 

        var result = await _repo.SaveChangesAsync();

        if(result) return Ok(); 

        return BadRequest("Problem updating the auction"); 
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _repo.GetAuctionEntityById(id);

        if(auction == null) return NotFound(); 

        if(auction.Seller != User.Identity.Name) return Forbid();

       _repo.RemoveAuction(auction); 

        await _publishEndpoint.Publish<AuctionDeleted>(new {Id = auction.Id.ToString()}); 

        var result = await _repo.SaveChangesAsync();

        if(!result) return BadRequest("Could not update DB"); 

        return Ok(); 
    }

    }
}
