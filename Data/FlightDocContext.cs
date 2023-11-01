using FlightDocV1._1.Models;
using Microsoft.EntityFrameworkCore;
using Version = FlightDocV1._1.Models.Version;

namespace FlightDocV1._1.Data
{
    public class FlightDocContext : DbContext
    {
        public FlightDocContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentPermission> DocumentPermissions { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSection> UserSections { get; set; }
        public DbSet<Version> Versions { get; set; }
    }
}
