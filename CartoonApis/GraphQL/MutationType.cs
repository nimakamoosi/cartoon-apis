using CartoonApis.DataAccess;
using HotChocolate.Subscriptions;

namespace CartoonApis.GraphQL
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(mutation => mutation.CreateFamily(default, default, default, default)).Type<FamilyType>();
            descriptor.Field(mutation => mutation.CreatePerson(default, default, default, default, default, default)).Type<PersonType>();
        }
    }
}
