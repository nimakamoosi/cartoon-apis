using CartoonApis.DataAccess;
using HotChocolate.Subscriptions;

namespace CartoonApis.GraphQL
{
    public class Mutation
    {
        public async Task<Family> CreateFamily([Service] IFamilyRepository familyRepository,
            [Service] ITopicEventSender eventSender, int id, string name)
        {
            var data = new Family(id: id, name: name);
            var result = await
            familyRepository.CreateFamily(data);
            await eventSender.SendAsync("FamilyCreated", result);
            return result;
        }

        public async Task<Person> CreatePerson([Service] IPersonRepository personRepository,
        [Service] ITopicEventSender eventSender, int id, string firstName, string lastName, int familyId)
        {
            var data = new Person(id: id,
                firstName: firstName,
                lastName: lastName,
                familyId: familyId);

            var result = await
            personRepository.CreatePerson(data);
            await eventSender.SendAsync("PersonCreated", result);
            return result;
        }
    }
}
