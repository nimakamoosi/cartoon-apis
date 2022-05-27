using CartoonApis.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CartoonApis.Controllers
{
    [ApiController]
    [Route("api/family")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ILogger<FamilyController> _logger;

        public FamilyController([Service] IFamilyRepository authorService, ILogger<FamilyController> logger)
        {
            _logger = logger;            
            _familyRepository = authorService;
        }
                
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            return _familyRepository.GetFamilies();
        }

        [HttpGet("{id}")]
        public Family GetFamily(int id)
        {
            return _familyRepository.GetFamilies().SingleOrDefault(f => f.Id == id);
        }

        [HttpPut]
        public async Task<Family> CreateOrUpdateFamily(Family family)
        {
            var existingFamily = _familyRepository.GetFamilyById(family.Id);
            if (existingFamily == null)
            {
                return await _familyRepository.CreateFamily(family);
            }
            else
            {
                return await _familyRepository.UpdateFamily(family);
            }
        }

        [HttpPost]
        public async Task<Family> CreateFamily(Family family)
        {
            return await _familyRepository.CreateFamily(family);
        }

        [HttpDelete]
        public async Task DeleteFamily(int id)
        {
            await _familyRepository.DeleteFamily(id);
        }
    }
}