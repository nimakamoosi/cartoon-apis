using Microsoft.EntityFrameworkCore;

namespace CartoonApis.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        public DbSet<Family> Families { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Families
            Family flinstoneFamily = new Family(id: 1, name: "Flinstones");
            Family rubbleFamily = new Family(id: 2, name: "Rubbles");
            modelBuilder.Entity<Family>().HasData(flinstoneFamily, rubbleFamily);

            // Perople
            Person fredFlinstone = new Person(id: 1, firstName: "Fred", lastName: "Flinstone", familyId: flinstoneFamily.Id);
            Person wilmaFlinstone = new Person(id: 2, firstName: "Wilma", lastName: "Flinstone", familyId: flinstoneFamily.Id);
            Person pebblesFlinstone = new Person(id: 3, firstName: "Pebbles", lastName: "Flinstone", familyId: flinstoneFamily.Id);
            Person barneyRubble = new Person(id: 4, firstName: "Barney", lastName: "Rubble", familyId: rubbleFamily.Id);
            Person bettyRubble = new Person(id: 5, firstName: "Betty", lastName: "Rubble", familyId: rubbleFamily.Id);
            Person bammbammRubble = new Person(id: 6, firstName: "Bamm-Bamm", lastName: "Rubble", familyId: rubbleFamily.Id);
            modelBuilder.Entity<Person>().HasData(fredFlinstone, wilmaFlinstone, pebblesFlinstone, barneyRubble, bettyRubble, bammbammRubble);
        }
    }
}
