namespace CartoonApis.DataAccess
{
    public interface IFamilyRepository
    {
        public List<Family> GetFamilies();
        public Family GetFamilyById(int id);
        public Task<Family> CreateFamily(Family family);
        Task<Family> UpdateFamily(Family family);
        Task DeleteFamily(int id);
    }
}
