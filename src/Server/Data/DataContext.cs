namespace homework_prac4.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } 

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { Id = 1, FirstName = "Anna", LastName = "Walker", Age = 21, Gender = "Female" },
                new UserModel { Id = 2, FirstName = "Bob", LastName = "Marley", Age = 47, Gender = "Male" }
                );

        }
    }
}
