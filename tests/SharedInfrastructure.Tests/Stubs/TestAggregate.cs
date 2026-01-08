using SharedDomain.Primitives;

namespace SharedInfrastructure.Tests.Stubs
{
    public sealed class TestAggregate : AggregateRoot<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
