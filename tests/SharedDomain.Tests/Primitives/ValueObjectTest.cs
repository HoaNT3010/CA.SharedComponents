using FluentAssertions;
using SharedDomain.Primitives;

namespace SharedDomain.Tests.Primitives
{
    public class ValueObjectTest
    {
        private sealed class TestMoney : ValueObject
        {
            public decimal Amount { get; }
            public string Currency { get; }
            public TestMoney(decimal amount, string currency)
            {
                Amount = amount;
                Currency = currency;
            }

            protected override IEnumerable<object?> GetEqualityComponents()
            {
                yield return Amount;
                yield return Currency;
            }
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(10, "USD");

            // Assert
            money1.Should().Be(money2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenValuesAreDifferent()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(20, "VND");

            // Assert
            money1.Should().NotBe(money2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualValues()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(10, "USD");

            // Act
            var hashCode1 = money1.GetHashCode();
            var hashCode2 = money2.GetHashCode();

            // Assert
            money1.Should().Be(money2);
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDiffrentValues()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(5, "EUR");

            // Act
            var hashCode1 = money1.GetHashCode();
            var hashCode2 = money2.GetHashCode();

            // Assert
            money1.Should().NotBe(money2);
            hashCode1.Should().NotBe(hashCode2);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnTrue_ForEqualObjects()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(10, "USD");

            // Assert
            (money1 == money2).Should().BeTrue();
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_ForDifferentObjects()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            var money2 = new TestMoney(5, "EUR");

            // Assert
            (money1 != money2).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenComparedWithNull()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            TestMoney? money2 = null;

            // Assert
            money1.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_WhenComparedWithNull()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            TestMoney? money2 = null;

            // Assert
            (money1 == money2).Should().BeFalse();
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenComparedWithNull()
        {
            // Arrange
            var money1 = new TestMoney(10, "USD");
            TestMoney? money2 = null;

            // Assert
            (money1 != money2).Should().BeTrue();
        }

        [Fact]
        public void ValueObject_ShouldWorkAsDictionaryKey()
        {
            // Arrange
            var dict = new Dictionary<ValueObject, string>();
            var key1 = new TestMoney(10, "USD");
            var key2 = new TestMoney(10, "USD");

            // Act
            dict[key1] = "value";

            // Assert
            dict[key2].Should().Be("value");
        }
    }
}
