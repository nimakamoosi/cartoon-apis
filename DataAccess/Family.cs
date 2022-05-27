namespace CartoonApis.DataAccess
{
    public class Family
    {
        public Family(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
