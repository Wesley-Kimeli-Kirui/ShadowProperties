using Microsoft.EntityFrameworkCore;

namespace ShadowProperties
{
    // Define the User class, representing a user entity in the database.
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user.
        public string Name { get; set; } = string.Empty; // User's name.

        // Navigation property representing a collection of associated Test entities.
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }

    // Define the Test class, representing a test entity in the database.
    public class Test
    {
        public int Id { get; set; } // Unique identifier for the test.
        public string Text { get; set; } = string.Empty; // Text associated with the test.
    }

    // Define the DbContext class named "Db" for interacting with the database.
    public class Db : DbContext
    {
        // Configure the database connection options when the DbContext is created.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify that the DbContext should use SQL Server as the database provider
            // and provide the connection string.
            optionsBuilder.UseSqlServer("# YOUR CONNECTION STRING HERE #");
        }

        // Define the database model by configuring entities and their relationships.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Test and User entities.
            modelBuilder.Entity<Test>()
                .HasOne<User>()
                .WithMany(x => x.Tests)
                .HasForeignKey("UserId") // Define a foreign key named "UserId."
                .IsRequired(); // Specify that the foreign key is required.

            // Define a shadow property "LastModified" for the Test entity.
            modelBuilder.Entity<Test>()
                .Property<DateTime>("LastModified");
        }

        // Define DbSet properties for User and Test entities, allowing database queries.
        public DbSet<User> Users { get; set; } = null!; // DbSet for User entities.
        public DbSet<Test> Tests { get; set; } = null!; // DbSet for Test entities.
    }
}
