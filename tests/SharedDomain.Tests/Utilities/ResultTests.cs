using FluentAssertions;
using SharedDomain.Utilities;

namespace SharedDomain.Tests.Utilities
{
    public class ResultTests
    {
        const string ErrorCode = "error_code";
        const string ErrorDescription = "Error description";

        [Fact]
        public void Success_ShouldCreateSuccessfulResultWithoutErrors()
        {
            // Act
            var result = Result.Success();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact]
        public void Failure_ShouldCreateFailedResultWithErrors()
        {
            // Arrange
            var error = Error.Failure(ErrorCode, ErrorDescription);

            // Act
            var result = Result.Failure(error);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.Should().Be(error);
        }

        [Fact]
        public void Failure_WithMultipleErrors_ShouldStoreAllErrors()
        {
            // Arrange
            var errors = new[]
            {
                Error.Failure(ErrorCode, ErrorDescription),
                Error.Problem(ErrorCode, ErrorDescription),
            };

            // Act
            var result = Result.Failure(errors);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
        }

        [Fact]
        public void Failure_WithNoError_ShouldThrowArgumentException()
        {
            // Act
            var act = () => Result.Failure();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithParameterName("errors");
        }

        [Fact]
        public void Failure_WithEmptyErrorCollection_ShouldThrowArgumentException()
        {
            // Arrange
            var errors = Array.Empty<Error>();

            // Act
            var act = () => Result.Failure(errors);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithParameterName("errors");
        }
    }
}
