using CartoonApis.DataAccess;

namespace CartoonApis.GraphQL
{
    public class FamilyType : ObjectType<Family>
    {
        protected override void Configure(IObjectTypeDescriptor<Family> descriptor)
        {
            descriptor.Field(a => a.Id);
            descriptor.Field(a => a.Name);
            descriptor.Field("members").ResolveWith<PersonResolver>(personResolver => personResolver.GetMembers(default)).Type<ListType<PersonType>>();
        }
    }
}
