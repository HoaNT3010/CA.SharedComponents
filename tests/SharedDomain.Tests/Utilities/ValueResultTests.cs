using FluentAssertions;
using SharedDomain.Utilities;

namespace SharedDomain.Tests.Utilities
{
    public class ValueResultTests
    {
        const string ErrorCode = "error_code";
        const string ErrorDescription = "Error description";

        [Fact]
        public void Success_WithValue_ShouldCreateSuccessfulResult()
        {
            // Arrange
            var value = 10;

            // Act
            var result = Result.Success(value);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(value);
            result.Errors.Should().BeNull();
        }

        [Fact]
        public void Failure_ShouldCreateFailedResultWithoutValue()
        {
            // Arrange
            var error = Error.Problem(ErrorCode, ErrorDescription);

            // Act
            var result = Result.Failure<int>(error);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.Should().Be(error);
            // Skip accessing value of failed result.
        }

        [Fact]
        public void Failure_TryingToAccessValue_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var error = Error.Problem(ErrorCode, ErrorDescription);

            // Act
            var result = Result.Failure<int>(error);
            var act = () => result.Value;

            // Assert
            result.IsSuccess.Should().BeFalse();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Success_WithNullValue_ShouldThrowArgumentException()
        {
            // Act
            var act = () => Result.Success<string>(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithParameterName("value");
        }

        [Fact]
        public void ImplicitConversion_WithNonNullValue_ShouldCreateSuccessResult()
        {
            // Act
            Result<string> result = "test";

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().Be("test");
        }

        [Fact]
        public void ImplicitConversion_WithNullValue_ShouldCreateFailedResult()
        {
            // Arrange
            string? value = null;

            // Act
            Result<string> result = value;

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors[0].Should().Be(Error.ValueNotProvided);
        }
    }
}
