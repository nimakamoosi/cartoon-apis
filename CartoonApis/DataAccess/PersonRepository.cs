using Microsoft.EntityFrameworkCore;

namespace CartoonApis.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public PersonRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            using (var _applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                _applicationDbContext.Database.EnsureCreated();
            }
        }

        public List<Person> GetPeople()
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.People.ToList();
            }
        }

        public Person GetPersonById(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.People.SingleOrDefault(x => x.Id == id);
            }
        }

        public async Task<Person> CreatePerson(Person person)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                await applicationDbContext.People.AddAsync(person);
                await applicationDbContext.SaveChangesAsync();
                var createdPerson = applicationDbContext.People.SingleOrDefault(x => x.Id == person.Id);
                createdPerson.Family = applicationDbContext.Families.SingleOrDefault(x => x.Id == person.FamilyId);
                return createdPerson;
            }
        }

        public async Task<Person> UpdatePerson(Person family)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                var result = applicationDbContext.People.Update(family).Entity;
                await applicationDbContext.SaveChangesAsync();
                return result;
            }
        }

        public async Task DeletePerson(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                var result = applicationDbContext.People.Remove(new Person(id, default, default, default)).Entity;
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
