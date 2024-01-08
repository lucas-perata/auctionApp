using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Item, Item>(); 

            if(!string.IsNullOrEmpty(searchParams.searchTerm))
            {
                query.Match(Search.Full, searchParams.searchTerm).SortByTextScore();
            }

            query = searchParams.OrderBy switch 
            {
                "categoryName" =>  query.Sort(x => x.Ascending(a => a.CategoryName)),
                "new" => query.Sort(x => x.Ascending(a => a.CreatedAt)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
            };  

            query = searchParams.FilterBy switch 
            {
                "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(12) && x.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
            }; 

            if (!string.IsNullOrEmpty(searchParams.Seller))
            {
                query.Match(x => x.Seller == searchParams.Seller);
            }

            if (!string.IsNullOrEmpty(searchParams.Winner))
            {
                query.Match(x => x.CategoryName == searchParams.Winner);
            }

            if (!string.IsNullOrEmpty(searchParams.CategoryName))
            {
                query.Match(x => x.CategoryName == searchParams.CategoryName);
            }

            query.PageNumber(searchParams.pageNumber);
            query.PageSize(searchParams.pageSize);

            var result = await query.ExecuteAsync(); 

            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            }); 
        }
    }
}