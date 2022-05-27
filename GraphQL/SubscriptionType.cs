using CartoonApis.DataAccess;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace CartoonApis.GraphQL
{
    public class SubscriptionType : ObjectType<Subscription>
    {
        protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
        {
            descriptor.Field(subscription => subscription.OnFamilyCreated(default, default)).Type<FamilyType>();

            descriptor.Field(subscription => subscription.OnFamilyGet(default, default)).Type<FamilyType>();

            descriptor.Field(subscription => subscription.OnSpecificFamilyGet(default, default, default)).Type<FamilyType>();

            descriptor.Field(subscription => subscription.OnFamiliesGet(default, default)).Type<ListType<FamilyType>>();
        }
    }
}
