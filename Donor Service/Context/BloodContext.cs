using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class BloodContext : DbContext
    {
        public BloodContext(DbContextOptions options)
            : base(options)
        {

        }

        //public DbSet<House> Houses{ get; set; }
        //public DbSet<Booking> Bookings { get; set; }

        public DbSet<Donor> Donors { get; set; }

       // public DbSet<Branch> Branches { get; set; }

        public DbSet<BloodDonation> BloodDonations { get; set; }
    }
}
