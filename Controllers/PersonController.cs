using CartoonApis.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CartoonApis.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonController> _logger;

        public PersonController([Service] IPersonRepository authorService, ILogger<PersonController> logger)
        {
            _logger = logger;            
            _personRepository = authorService;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _personRepository.GetPeople();
        }

        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            return _personRepository.GetPeople().SingleOrDefault(p => p.Id == id);
        }

        [HttpPut]
        public async Task<Person> CreateOrUpdateFamily(Person person)
        {
            var existingFamily = _personRepository.GetPersonById(person.Id);
            if (existingFamily == null)
            {
                return await _personRepository.CreatePerson(person);
            }
            else
            {
                return await _personRepository.UpdatePerson(person);
            }
        }

        [HttpPost]
        public async Task<Person> CreatePerson(Person person)
        {
            return await _personRepository.CreatePerson(person);
        }

        [HttpDelete]
        public async Task DeletePerson(int id)
        {
            await _personRepository.DeletePerson(id);
        }
    }
}