using CartoonApis.DataAccess;
using HotChocolate.Resolvers;

namespace CartoonApis.GraphQL
{
    public class FamilyResolver
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyResolver([Service] IFamilyRepository authorService)
        {
            _familyRepository = authorService;
        }

        public Family GetFamily(IResolverContext resolverContext)
        {
            Person person = resolverContext.Parent<Person>();
            return _familyRepository.GetFamilies().Where(family => family.Id == person.FamilyId).FirstOrDefault();
        }
    }
}
