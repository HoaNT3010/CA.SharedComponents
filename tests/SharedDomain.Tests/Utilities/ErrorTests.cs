using FluentAssertions;
using SharedDomain.Enums;
using SharedDomain.Utilities;

namespace SharedDomain.Tests.Utilities
{
    public class ErrorTests
    {
        const string Code = "error_code";
        const string Description = "Error Description";

        [Fact]
        public void Failure_ShouldCreateErrorWithCorrectValues()
        {
            // Act
            var error = Error.Failure(Code, Description);

            // Assert
            error.Code.Should().Be(Code);
            error.Description.Should().Be(Description);
            error.Type.Should().Be(ErrorType.Failure);
        }

        [Fact]
        public void Failure_WithNullCode_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => Error.Failure(null!, Description);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("code");
        }

        [Fact]
        public void Failure_WithNullDescription_ShouldThrowArgumentNullException()
        {
            // Act
            var act = () => Error.Failure(Code, null!);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("description");
        }

        [Fact]
        public void Error_OfTypeNone_ShouldRepresentNoError()
        {
            // Act
            var error = Error.None;

            // Assert
            error.Code.Should().BeEmpty();
            error.Description.Should().BeEmpty();
            error.Type.Should().Be(ErrorType.None);
        }

        [Theory]
        [InlineData(ErrorType.Validation)]
        [InlineData(ErrorType.Failure)]
        [InlineData(ErrorType.Conflict)]
        [InlineData(ErrorType.Problem)]
        [InlineData(ErrorType.Forbidden)]
        [InlineData(ErrorType.Unauthorized)]
        [InlineData(ErrorType.NotFound)]
        public void FactoryMethods_ShouldCreateErrorWithCorrectType(ErrorType expectedType)
        {
            // Act
            var error = expectedType switch
            {
                ErrorType.Validation => Error.Validation(Code, Description),
                ErrorType.Failure => Error.Failure(Code, Description),
                ErrorType.Conflict => Error.Conflict(Code, Description),
                ErrorType.Problem => Error.Problem(Code, Description),
                ErrorType.Forbidden => Error.Forbidden(Code, Description),
                ErrorType.Unauthorized => Error.Unauthorized(Code, Description),
                ErrorType.NotFound => Error.NotFound(Code, Description),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Assert
            error.Code.Should().Be(Code);
            error.Description.Should().Be(Description);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void Errors_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var error1 = Error.Validation(Code, Description);
            var error2 = Error.Validation(Code, Description);

            // Assert
            error1.Should().Be(error2);
            error1.GetHashCode().Should().Be(error2.GetHashCode());
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var error = Error.Validation(Code, Description);

            // Act
            var result = error.ToString();

            // Assert
            result.Should().Be($"{ErrorType.Validation}: {Code} - {Description}");
        }
    }
}
