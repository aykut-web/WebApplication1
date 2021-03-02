using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.InfrastructureLayer.Context
{
    public class ProjectContext : IdentityDbContext<User>
    {

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        public virtual DbSet<User> ApplicationUser { get; set; }
        public  DbSet<Location> Locations { get; set; }
        public  DbSet<Movie> Movies { get; set; }
        public  DbSet<Employee> Employees { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.ApplyConfiguration(new CategoryMap());
            //builder.ApplyConfiguration(new SliderMap());
            //builder.ApplyConfiguration(new PostMap());
            //builder.ApplyConfiguration(new PostImageMap());
            //builder.ApplyConfigurationsFromAssembly(typeof(IMapper).Assembly);
            

            builder.Entity<LocationMovies>()
                   .HasKey(bc => new { bc.LocationId, bc.MovieId });

            builder.Entity<LocationMovies>()
                   .HasOne(bc => bc.Location)
                   .WithMany(b => b.Movies)
                   .HasForeignKey(bc => bc.LocationId);

            builder.Entity<LocationMovies>()
                   .HasOne(bc => bc.Movie)
                   .WithMany(c => c.Locations)
                   .HasForeignKey(bc => bc.MovieId);

            base.OnModelCreating(builder);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Location>().OwnsOne(x => x.Movies);
        //}

    }
}
