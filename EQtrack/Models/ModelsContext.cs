using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EQtrack.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<tool> Tools { get; set; }
        public DbSet<inventory> Inventories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ReturnTicket> Returns { get; set; }


    }
}