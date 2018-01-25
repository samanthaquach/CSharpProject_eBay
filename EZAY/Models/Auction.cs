using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System;

namespace User_Dashboard.Models
{
    public class Auction : BaseEntity
    {
        public int AuctionId { get; set; }
        [Required(ErrorMessage = "Product Name cannot be left blank.")]
        [MinLength(3, ErrorMessage = "Product Name cannot be less than 3 characters.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Bid cannot be left blank.")]
        [MinLength(3, ErrorMessage = "Bid cannot be less than 0 dollars.")]
        public int StartingBid { get; set; }
        [Required(ErrorMessage = "Bid cannot be left blank.")]
        [MinLength(3, ErrorMessage = "Bid cannot be less than 0 characters.")]
        public int HighestBid { get; set; }
        public string Bidder { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public Double DaysLeft { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}