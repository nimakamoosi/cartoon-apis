using CartoonApis.DataAccess;
using HotChocolate.Subscriptions;

namespace CartoonApis.GraphQL
{
    public class Query
    {
        public async Task<List<Family>> GetAllFamilies([Service] IFamilyRepository familyRepository, [Service] ITopicEventSender eventSender)
        {
            List<Family> families = familyRepository.GetFamilies();
            await eventSender.SendAsync("ReturnedFamilies", families);
            return families;
        }

        public async Task<Family> GetFamilyById([Service] IFamilyRepository familyRepository, [Service] ITopicEventSender eventSender, int id)
        {
            Family family = familyRepository.GetFamilyById(id);
            await eventSender.SendAsync("ReturnedFamily", family);
            await eventSender.SendAsync($"ReturnedFamily.{id}", family);
            return family;
        }

        public async Task<List<Person>> GetAllPeople([Service] IPersonRepository personRepository, [Service] ITopicEventSender eventSender)
        {
            List<Person> people = personRepository.GetPeople();
            await eventSender.SendAsync("ReturnedPeople", people);
            return people;
        }

        public async Task<Person> GetPersonById([Service] IPersonRepository personRepository, [Service] ITopicEventSender eventSender, int id)
        {
            Person person = personRepository.GetPersonById(id);
            await eventSender.SendAsync("ReturnedPerson", person);
            return person;
        }
    }
}
