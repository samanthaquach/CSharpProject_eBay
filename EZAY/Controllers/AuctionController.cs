using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using User_Dashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace User_Dashboard.Controllers
{
    public class AuctionController : Controller
    {

        private static List<string> errors = new List<string>();

        private User_DashboardContext _context;
        public AuctionController(User_DashboardContext context)
        {
            _context = context;
        }

        //dashboard where all auction are displayed

        [HttpGet]
        [Route("Auction")]
        public IActionResult Auction(string search, string category)
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            //placing all the search function
            if (search != null)
            {
                if (category == "Name")
                {
                    List<Auction> NameSearch = _context.Auction.Where(l => l.ProductName.ToLower().Contains(search.ToLower())).ToList();
                    return View(NameSearch);
                }
                if (category == "City")
                {
                    List<Auction> DescriptionSearch = _context.Auction.Where(l => l.Description.ToLower().Contains(search.ToLower())).ToList();
                    return View(DescriptionSearch);
                }
            }

            List<Auction> model = _context.Auction.ToList();
            List<Auction> Auction = _context.Auction.ToList();
            
            ViewBag.Auction = model;

            return View("auction", model);

        }

        [HttpPost]
        [Route("/search")]
        public IActionResult Search(string searchContent, string categoryContent)
        {
            return RedirectToAction("Auction", new { search = searchContent, category = categoryContent });
        }

        // end of public class <==
    }
}
