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
            Family FlintstoneFamily = new Family(id: 1, name: "Flintstones");
            Family rubbleFamily = new Family(id: 2, name: "Rubbles");
            Family smithFamily = new Family(id: 3, name: "Smiths");
            Family simpsonFamily = new Family(id: 4, name: "Simpsons");
            modelBuilder.Entity<Family>().HasData(FlintstoneFamily, rubbleFamily, smithFamily, simpsonFamily);

            // People
            Person fredFlintstone = new Person(id: 1, firstName: "Fred", lastName: "Flintstone", familyId: FlintstoneFamily.Id);
            Person wilmaFlintstone = new Person(id: 2, firstName: "Wilma", lastName: "Flintstone", familyId: FlintstoneFamily.Id);
            Person pebblesFlintstone = new Person(id: 3, firstName: "Pebbles", lastName: "Flintstone", familyId: FlintstoneFamily.Id);
            Person barneyRubble = new Person(id: 4, firstName: "Barney", lastName: "Rubble", familyId: rubbleFamily.Id);
            Person bettyRubble = new Person(id: 5, firstName: "Betty", lastName: "Rubble", familyId: rubbleFamily.Id);
            Person bammbammRubble = new Person(id: 6, firstName: "Bamm-Bamm", lastName: "Rubble", familyId: rubbleFamily.Id);

            Person rickSanchez = new Person(id: 7, firstName: "Rick", lastName: "Sanchez", familyId: smithFamily.Id);
            Person mortySmith = new Person(id: 8, firstName: "Morty", lastName: "Smith", familyId: smithFamily.Id);
            Person summerSmith = new Person(id: 9, firstName: "Summer", lastName: "Smith", familyId: smithFamily.Id);
            Person jerrySmith = new Person(id: 10, firstName: "Jerry", lastName: "Smith", familyId: smithFamily.Id);
            Person bethSmith = new Person(id: 11, firstName: "Beth", lastName: "Smith", familyId: smithFamily.Id);

            Person homerSimpson = new Person(id: 12, firstName: "Homer", lastName: "Simpson", familyId: simpsonFamily.Id);
            Person margeSimpson = new Person(id: 13, firstName: "Marge", lastName: "Simpson", familyId: simpsonFamily.Id);
            Person bartSimpson = new Person(id: 14, firstName: "Bart", lastName: "Simpson", familyId: simpsonFamily.Id);
            Person lisaSimpson = new Person(id: 15, firstName: "Lisa", lastName: "Simpson", familyId: simpsonFamily.Id);
            Person maggieSimpson = new Person(id: 16, firstName: "Maggie", lastName: "Simpson", familyId: simpsonFamily.Id);

            modelBuilder.Entity<Person>().HasData(fredFlintstone, wilmaFlintstone, pebblesFlintstone,
                barneyRubble, bettyRubble, bammbammRubble,
                rickSanchez, mortySmith, summerSmith, jerrySmith, bethSmith,
                homerSimpson, margeSimpson, bartSimpson, lisaSimpson, maggieSimpson
                );
        }
    }
}
