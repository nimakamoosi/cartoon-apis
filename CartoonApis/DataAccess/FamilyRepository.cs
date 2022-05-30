using Microsoft.EntityFrameworkCore;

namespace CartoonApis.DataAccess
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public FamilyRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            using (var _applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                _applicationDbContext.Database.EnsureCreated();
            }
        }

        public List<Family> GetFamilies()
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.Families.ToList();
            }
        }

        public Family GetFamilyById(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.Families.SingleOrDefault(x => x.Id == id);
            }
        }

        public async Task<Family> CreateFamily(Family family)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                await applicationDbContext.Families.AddAsync(family);
                await applicationDbContext.SaveChangesAsync();
                return family;
            }
        }

        public async Task<Family> UpdateFamily(Family family)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                var result = applicationDbContext.Families.Update(family).Entity;
                await applicationDbContext.SaveChangesAsync();
                return result;
            }
        }

        public async Task DeleteFamily(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                var result = applicationDbContext.Families.Remove(new Family(id, string.Empty)).Entity;
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
