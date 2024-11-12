using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace soft20181_starter.Models
{
    public class EventAppDbContext : IdentityDbContext<User>
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<User> Customers{ get; set; } 

        public DbSet<Event> Events { get; set; }

        public DbSet<Booking> Bookings { get; set; }

    }

}
 