using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionService.Entities;

namespace lucas.OneDrive.Escritorio.Git.auctionApp.auctionApp.tests.Utils
{
    public class Helpers
    {
        public static ClaimsPrincipal GetClaimsPrincipal()
        {
            var claims = new List<Claim>{new Claim(ClaimTypes.Name, "tests")}; 
            var identity = new ClaimsIdentity(claims, "testing");
            return new ClaimsPrincipal(identity);
        }
    }
}