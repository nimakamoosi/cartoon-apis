using CartoonApis.DataAccess;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace CartoonApis.GraphQL
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Family>> OnFamilyCreated([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Family>("FamilyCreated", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<List<Family>>> OnFamiliesGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, List<Family>>("ReturnedFamilies", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Family>> OnFamilyGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Family>("ReturnedFamily", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Family>> OnSpecificFamilyGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken, int id)
        {
            return await eventReceiver.SubscribeAsync<string, Family>($"ReturnedFamily.{id}", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Person>> OnPersonCreated([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Person>("PersonCreated", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Person>> OnPeopleGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Person>("ReturnedPeople", cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Person>> OnPersonGet([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Person>("ReturnedPerson", cancellationToken);
        }    }
}
