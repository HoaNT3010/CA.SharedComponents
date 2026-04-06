namespace SharedDomain.Utilities
{
    /// <summary>
    /// Represents the outcome of an operation, indicating success or failure and providing error details if applicable.
    /// </summary>
    /// <remarks>Use the static factory methods to create instances representing either a successful or failed
    /// result. When a result indicates failure, one or more errors are provided to describe the reason for failure.
    /// This type is commonly used to encapsulate the result of operations that may fail, allowing for explicit handling
    /// of errors without relying on exceptions.</remarks>
    public class Result
    {
        /// <summary>
        /// Gets a value indicating whether the operation completed successfully.
        /// </summary>
        public bool IsSuccess { get; }
        /// <summary>
        /// Gets the collection of errors associated with the current operation.
        /// </summary>
        public Error[]? Errors { get; }
        /// <summary>
        /// Initializes a new instance of the Result class with the specified success state and associated errors.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the result represents a successful operation.</param>
        /// <param name="errors">An array of errors associated with the result. Must be null or empty if the result is successful; otherwise,
        /// must contain at least one error.</param>
        /// <exception cref="ArgumentException">Thrown if errors is null or empty when isSuccess is false, or if errors is not null or empty when isSuccess
        /// is true.</exception>
        protected internal Result(bool isSuccess, Error[]? errors = null)
        {
            if (!isSuccess && (errors == null || errors.Length == 0))
            {
                throw new ArgumentException("Errors must be provided when result is not successful.", nameof(errors));
            }
            if (isSuccess && errors?.Length > 0)
            {
                throw new ArgumentException("Errors should not be provided when result is successful.", nameof(errors));
            }
            IsSuccess = isSuccess;
            Errors = errors;
        }
        /// <summary>
        /// Creates a new instance of the Result type that represents a successful operation.
        /// </summary>
        /// <returns>A <see cref="Result"/> instance indicating success.</returns>
        public static Result Success() => new(true);
        /// <summary>
        /// Creates a successful result containing the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to be stored in the result.</typeparam>
        /// <param name="value">The value to associate with the successful result. Can not be null.</param>
        /// <returns>A <see cref="Result{TValue}"/> instance representing a successful operation with the provided value.</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new(true, value);
        /// <summary>
        /// Creates a failed result containing the specified errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="Error"/> with at least one element that describe the reasons for the failure. Cannot be null or contain null elements.</param>
        /// <returns>A <see cref="Result"/> instance representing a failure with the provided errors.</returns>
        public static Result Failure(params Error[] errors) => new(false, errors);
        /// <summary>
        /// Creates a failed result with the specified errors.
        /// </summary>
        /// <remarks>Use this method to represent an operation that has failed and to provide one or more
        /// errors describing the failure.</remarks>
        /// <typeparam name="TValue">The type of the value that would be returned on success.</typeparam>
        /// <param name="errors">An array of <see cref="Error"/> with at least one element that describe the reason for the failure. Cannot be null or contain null elements.</param>
        /// <returns>A failed <see cref="Result{TValue}"/> instance containing the specified errors.</returns>
        public static Result<TValue> Failure<TValue>(params Error[] errors) => new(false, default, errors);
    }
    /// <summary>
    /// Represents the result of an operation that can succeed or fail and, if successful, contains a value of the
    /// specified type.
    /// </summary>
    /// <remarks>Use this type to encapsulate the outcome of an operation that may produce a value on success
    /// or one or more errors on failure. The generic parameter allows you to specify the type of the value returned on
    /// success. If the result is not successful, the value is not set and errors are available from the base class.
    /// This pattern helps to avoid exceptions for control flow and makes error handling explicit.</remarks>
    /// <typeparam name="TValue">The type of the value returned when the operation is successful.</typeparam>
    public class Result<TValue> : Result
    {
        /// <summary>
        /// Value hold by the instance.
        /// </summary>
        private readonly TValue? value;

        /// <summary>
        /// Gets the current value held by the instance, only accessible when the result is success.
        /// An <see cref="InvalidOperationException"/> will be thrown when trying to access the value of a failed result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if trying to access value of a failed result.</exception>
        public TValue? Value
            => IsSuccess
            ? value
            : throw new InvalidOperationException("Value of a failed result cannot be accessed");
        /// <summary>
        /// Initializes a new instance of the Result class with the specified success state, value, and errors.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the result represents a successful operation. If <see langword="true"/>, a value
        /// must be provided; if <see langword="false"/>, no value should be provided.</param>
        /// <param name="value">The value associated with a successful result. Must be non-null if <paramref name="isSuccess"/> is <see
        /// langword="true"/>.</param>
        /// <param name="errors">An array of errors associated with an unsuccessful result. This parameter is ignored if <paramref
        /// name="isSuccess"/> is <see langword="true"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="isSuccess"/> is <see langword="true"/> and <paramref name="value"/> is null, or if
        /// <paramref name="isSuccess"/> is <see langword="false"/> and <paramref name="value"/> is not null.</exception>
        protected internal Result(bool isSuccess, TValue? value = default, Error[]? errors = null) : base(isSuccess, errors)
        {
            if (isSuccess && value is null)
            {
                throw new ArgumentException("Value must be provided when result is successful.", nameof(value));
            }
            this.value = value;
        }
        /// <summary>
        /// Converts a value of type TValue to a Result<TValue> representing success if the value is not null, or
        /// failure if the value is null.
        /// </summary>
        /// <remarks>This implicit conversion allows direct assignment of a TValue to a Result<TValue>. If
        /// the value is null, the resulting Result<TValue> will be a failure with Error.None; otherwise, it will be a
        /// success containing the value.</remarks>
        /// <param name="value">The value to convert to a Result<TValue>. If the value is null, the result will represent failure.</param>
        public static implicit operator Result<TValue>(TValue? value)
            => value is not null
            ? Success(value)
            : Failure<TValue>(Error.ValueNotProvided);
    }
}
