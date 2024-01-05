using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDbContext _context;
        private readonly IMapper _mapper;

        public AuctionsController(AuctionDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
                        .Include(x => x.Item)
                        .ThenInclude(x => x.Category)
                        .OrderBy(x => x.Item.Title)
                        .ToListAsync(); 
        
        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
                        .Include(x => x.Item)
                        .ThenInclude(x => x.Category)
                        .FirstOrDefaultAsync(x => x.Id == id); 

        if (auction == null) return NotFound(); 
        
        return _mapper.Map<AuctionDto>(auction); 
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto); 

        // TODO: current user as seller
        auction.Seller = "test"; 

        _context.Auctions.Add(auction); 

        var result = await _context.SaveChangesAsync() > 0; 

        if(!result) return BadRequest("Could not save changes");

        return CreatedAtAction(nameof(GetAuctionById), new{auction.Id}, _mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions
                        .Include(x => x.Item)
                        .FirstOrDefaultAsync(x => x.Id == id); 
        
        if(auction == null) return NotFound(); 

        // TODO: check seller == username

        auction.Item.Title = updateAuctionDto.Title ?? auction.Item.Title; 
        auction.Item.Description = updateAuctionDto.Description ?? auction.Item.Description;

        var result = await _context.SaveChangesAsync() > 0; 

        if(result) return Ok(); 

        return BadRequest("Problem updating the auction"); 
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id); 

        if(auction == null) return NotFound(); 

        // TODO: check seller == username

        _context.Auctions.Remove(auction); 

        var result = await _context.SaveChangesAsync() > 0; 

        if(!result) return BadRequest("Could not update DB"); 

        return Ok(); 
    }

    }
}
