using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace User_Dashboard.Models
{
    public class User_DashboardContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public User_DashboardContext(DbContextOptions<User_DashboardContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auction { get; set; }

    }
}