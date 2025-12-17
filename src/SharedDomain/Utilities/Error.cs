using SharedDomain.Enums;

namespace SharedDomain.Utilities
{
    /// <summary>
    /// Represents an error with a code, description, and type.
    /// </summary>
    /// <remarks>Use this record to convey error information in a structured format, such as when returning
    /// error details from an API or service. The properties provide standardized information that can be used for error
    /// handling, logging, or user feedback.</remarks>
    public record Error
    {
        /// <summary>
        /// Gets the code associated with this instance.
        /// </summary>
        public string Code { get; } = null!;
        /// <summary>
        /// Gets the description associated with the current instance.
        /// </summary>
        public string Description { get; } = null!;
        /// <summary>
        /// Gets the type of error represented by this instance.
        /// </summary>
        public ErrorType Type { get; }
        /// <summary>
        /// Initializes a new instance of the Error class with the specified error code, description, and error type.
        /// </summary>
        /// <param name="code">The unique code that identifies the error. Cannot be null.</param>
        /// <param name="description">A human-readable description of the error. Cannot be null.</param>
        /// <param name="type">The type of the error, indicating its category or severity.</param>
        protected internal Error(string code, string description, ErrorType type)
        {
            ArgumentNullException.ThrowIfNull(code, nameof(code));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            Code = code;
            Description = description;
            Type = type;
        }
        /// <summary>
        /// Represents the absence of an error.
        /// </summary>
        /// <remarks>Use this value to indicate a successful operation or when no error has occurred. This
        /// is a predefined instance with no message and an error type of None.</remarks>
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
        /// <summary>
        /// Represents an <see cref="Error"/> where a value is expected but not provided, with error's code, description and error type of <see cref="ErrorType.Problem"/>.
        /// </summary>
        /// <remarks>
        /// Created mainly serving as a semantic error when implicitly convert a null value to a valued <see cref="Result"/>.
        /// </remarks>
        public static readonly Error ValueNotProvided = new("VALUE_NOT_PROVIDED", "The required value was not provided.", ErrorType.Problem);
        /// <summary>
        /// Creates a validation error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the validation error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the validation error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a validation error with the provided code, description and error type of <see cref="ErrorType.Validation"/>.</returns>
        public static Error Validation(string code, string description) => new(code, description, ErrorType.Validation);
        /// <summary>
        /// Creates an unauthorized error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the unauthorized error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the unauthorized error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing an unauthorized error with the provided code, description and error type of <see cref="ErrorType.Unauthorized"/>.</returns>
        public static Error Unauthorized(string code, string description) => new(code, description, ErrorType.Unauthorized);
        /// <summary>
        /// Creates a forbidden error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the forbidden error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the forbidden error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a forbidden error with the provided code, description and error type of <see cref="ErrorType.Forbidden"/>.</returns>
        public static Error Forbidden(string code, string description) => new(code, description, ErrorType.Forbidden);
        /// <summary>
        /// Creates a not found error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the not found error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the not found error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a not found error with the provided code, description and error type of <see cref="ErrorType.NotFound"/>.</returns>
        public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
        /// <summary>
        /// Creates a conflict error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the conflict error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the conflict error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a conflict error with the provided code, description and error type of <see cref="ErrorType.Conflict"/>.</returns>
        public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
        /// <summary>
        /// Creates a failure error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the failure error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the failure error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a failure error with the provided code, description and error type of <see cref="ErrorType.Failure"/>.</returns>
        public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
        /// <summary>
        /// Creates a problem error with the specified error code and description.
        /// </summary>
        /// <param name="code">The unique code that identifies the problem error. Cannot be null.</param>
        /// <param name="description">A descriptive message explaining the problem error. Cannot be null.</param>
        /// <returns>An <see cref="Error"/> instance representing a problem error with the provided code, description and error type of <see cref="ErrorType.Problem"/>.</returns>
        public static Error Problem(string code, string description) => new(code, description, ErrorType.Problem);
        public override string ToString() => $"{Type}: {Code} - {Description}";
    }
}
