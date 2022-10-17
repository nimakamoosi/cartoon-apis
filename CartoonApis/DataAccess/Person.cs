using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CartoonApis.DataAccess
{
    public class Person
    {
        public Person(int id, string firstName, string lastName, int familyId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FamilyId = familyId;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[GraphQLType(typeof(string))]
        //[GraphQLIgnore]
        public int FamilyId { get; set; }

        //[GraphQLType(typeof(Family))]
        [GraphQLIgnore]
        [NotMapped]
        public Family? Family { get; set; }
    }
}
