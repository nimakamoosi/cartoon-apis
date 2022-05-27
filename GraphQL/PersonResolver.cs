using CartoonApis.DataAccess;
using HotChocolate.Resolvers;

namespace CartoonApis.GraphQL
{
    public class PersonResolver
    {
        private readonly IPersonRepository _personRepository;

        public PersonResolver([Service] IPersonRepository blogPostRepository)
        {
            _personRepository = blogPostRepository;
        }

        public IEnumerable<Person> GetMembers(IResolverContext resolverContext)
        {
            Family family = resolverContext.Parent<Family>();
            return _personRepository.GetPeople().Where(person => person.FamilyId == family.Id);
        }
    }
}
