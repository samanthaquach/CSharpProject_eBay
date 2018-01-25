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
    public class DashboardController : Controller
    {

        private static List<string> errors = new List<string>();
        private static string whichErr = null;

        private User_DashboardContext _context;
        public DashboardController(User_DashboardContext context)
        {
            _context = context;
        }

//dashboard where all auction are displayed

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            
            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);
            var UseId = Logger.UserId;
            ViewBag.Logger = Logger;
            ViewBag.LoggerId = Logger.UserId;
            ViewBag.LoggerWallet = Logger.wallet;
            var thisAuction = _context.Auction.ToList();
            List<User> Users = _context.Users.ToList();
            List<Auction> Auction = _context.Auction.ToList();
            Wrapper model = new Wrapper(Users, Auction);
            
            // list of items in the auction
            List<User> Auctions = _context.Users
                                            .Include(u => u.Auction)
                                            // .OrderBy(x => x.Auction.DaysLeft)
                                            .ToList();
            ViewBag.Auction = Auctions;

            return View("dashboard", model);
        }
 // new auction

        [HttpGet]
        [Route("NewAuction")]
        public IActionResult accept()
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);
            var UseId = Logger.UserId;
            ViewBag.Logger = Logger;
            ViewBag.LoggerId = Logger.UserId;

            return View("CreateAuction");
        }

// new auction

        [HttpPost]
        [Route("CreateAuction")]
        public IActionResult accept(string ProductName, int StartingBid, string Description, DateTime EndDate, int UserId)
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);
            String date = DateTime.Now.ToString("dd.MM.yyy");
            var currdate = DateTime.Now;
            var TotalDays = (EndDate - currdate).TotalDays;

            Auction newAuction = new Auction
            {
                UserId = Logger.UserId,
                ProductName = ProductName,
                StartingBid = StartingBid,
                Description = Description,
                EndDate = EndDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DaysLeft = Math.Ceiling(TotalDays)


            };
            _context.Add(newAuction);
            _context.SaveChanges();

            return Redirect("/dashboard");
        }

// show profile

        [HttpGet]
        [Route("show/{id}")]
        public IActionResult Show(int id)
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }

            ViewBag.showReg = false; //hide the registration notification box
            ViewBag.errors = errors; //set ViewBag.errors equal to the errors list

            if (whichErr == "bid") //if registration errors were set...
            {
                ViewBag.showReg = true; //unhide the registration notification box
            }
            
            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);
            ViewBag.thisUser = _context.Users.SingleOrDefault(user => user.UserId == id);

            List<User> Users = _context.Users.ToList();
            List<Auction> Auction = _context.Auction.Include(u => u.User).Where(item => item.AuctionId == id).ToList();
            Wrapper model = new Wrapper(Users, Auction);



            return View("show", model);
        }
// Bidding User Bidding

        [HttpPost]
        [Route("show/{id}")]
        public IActionResult Connect(int Bidder, int id, int HighestBid)
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            errors.Clear(); //clear out all errors to begin
            whichErr = null; //reset whichErr

            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);
            var HighestBidNow = _context.Auction.SingleOrDefault(user => user.AuctionId == id);
            
            if(HighestBid > HighestBidNow.HighestBid && HighestBid > HighestBidNow.StartingBid && Logger.wallet > HighestBid)
            {
                Auction DBAuction = _context.Auction.SingleOrDefault(my => my.AuctionId == id); //retrieve the particular user's information from the DB
                DBAuction.HighestBid = HighestBid;
                DBAuction.Bidder = Logger.firstname;
                _context.SaveChanges(); //save the changes in the DB     

                User DBUserWallet = _context.Users.SingleOrDefault(my => my.username == currUser); //retrieve the particular user's information from the DB
                DBUserWallet.wallet = Logger.wallet - HighestBid;
                _context.SaveChanges(); //save the changes in the DB     
            }
            else
            {
                errors.Add("Sorry bid is too low, please bid higher, or you may have insufficient funds."); //add error to lowest bidder
                whichErr = "bid"; 
                return RedirectToAction("Show");
            }

            return Redirect("/dashboard");
        }

//delete auction 
        [HttpGet]
        [Route("delete/{AuctionId}/")]
        public IActionResult delete(int AuctionId)
        {
            if (HttpContext.Session.GetString("LOGGED_IN_USER") == null)
            {
                return Redirect("/");
            }
            string currUser = HttpContext.Session.GetString("LOGGED_IN_USER");
            var Logger = _context.Users.SingleOrDefault(user => user.username == currUser);

            Auction delAuction = _context.Auction.SingleOrDefault(my => my.AuctionId == AuctionId); //retrieve the particular user's information from the DB
            _context.Auction.Remove(delAuction); //delete the auction from the the DB
            _context.SaveChanges(); //save the changes in the DB
            return Redirect("/dashboard"); //return the user to the dashboard

        }

        // end of public class <==
    }
}
