namespace CartoonApis.DataAccess
{
    public interface IPersonRepository
    {
        public List<Person> GetPeople();
        public Person GetPersonById(int id);
        public Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
