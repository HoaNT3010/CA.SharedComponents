using Ardalis.Specification;

namespace SharedInfrastructure.Tests.Stubs
{
    internal class TestByNameSpec : Specification<TestAggregate>
    {
        public TestByNameSpec(string name)
        {
            Query.Where(x => x.Name == name);
        }
    }
}
