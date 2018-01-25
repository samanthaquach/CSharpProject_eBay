using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace User_Dashboard.Models
{
    public class Wrapper : BaseEntity
    {
        public List<User> Users { get; set; }
        public List<Auction> Auction { get; set; }

        public Wrapper(List<User> theUsers, List<Auction> theAuction)
        {
            Users = theUsers;
            Auction = theAuction;

        }
    }
}