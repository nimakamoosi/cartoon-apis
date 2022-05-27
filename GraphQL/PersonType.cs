using CartoonApis.DataAccess;
using HotChocolate.Types;

namespace CartoonApis.GraphQL
{
    public class PersonType : ObjectType<Person>
    {
        protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
        {
            descriptor.Field(a => a.Id);
            descriptor.Field(a => a.FirstName);
            descriptor.Field(a => a.LastName);
            descriptor.Field("family").ResolveWith<FamilyResolver>(familyResolver => familyResolver.GetFamily(default)).Type<FamilyType>();
        }
    }

}
