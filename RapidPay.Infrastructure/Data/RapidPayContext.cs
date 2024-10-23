using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Data
{
    public class RapidPayContext : DbContext
    {
        public RapidPayContext(DbContextOptions<RapidPayContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}
