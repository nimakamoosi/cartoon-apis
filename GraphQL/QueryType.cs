using HotChocolate.Types;

namespace CartoonApis.GraphQL
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(query => query.GetAllFamilies(default, default)).Type<ListType<FamilyType>>();
            descriptor.Field(query => query.GetFamilyById(default, default, default)).Type<FamilyType>();
            descriptor.Field(f => f.GetAllPeople(default, default)).Type<ListType<PersonType>>();
            descriptor.Field(query => query.GetPersonById(default, default, default)).Type<PersonType>();

        }
    }
}
