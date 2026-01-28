using agent.entityClasses;
using Microsoft.EntityFrameworkCore;

namespace agent
{
    public class agentDbContextSqlite : DbContext
    {
        public agentDbContextSqlite(DbContextOptions<agentDbContextSqlite> options) : base(options)
        {
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyReview> AgencyReviews { get; set; }
        public DbSet<AgencyCountry> AgencyCountries { get; set; }
        public DbSet<AgencyDocument> AgencyDocuments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}