namespace SharedDomain.Enums
{
    /// <summary>
    /// Specifies the set of error types that can be returned or encountered during an operation.
    /// </summary>
    /// <remarks>Use this enumeration to categorize errors for consistent handling and reporting throughout an
    /// application. The values represent common error scenarios such as validation failures, resource conflicts, or
    /// authorization issues. The <see cref="ErrorType.None"/> value can be used as a default or sentinel to indicate
    /// that no error type has been assigned.</remarks>
    public enum ErrorType
    {
        /// <summary>
        /// Represents an undefined or uninitialized value.
        /// </summary>
        /// <remarks>Use this value to indicate that no valid option or state has been selected. This is
        /// typically used as a default or sentinel value in enumerations.</remarks>
        None = -1,
        /// <summary>
        /// Indicates that the operation did not complete successfully, typically due to critical system failure.
        /// </summary>
        Failure = 0,
        /// <summary>
        /// Indicates that the status represents a problem or error condition.
        /// </summary>
        Problem = 1,
        /// <summary>
        /// Indicates that the operation or result is related to validation.
        /// </summary>
        Validation = 2,
        /// <summary>
        /// Indicates that the requested item or resource was not found.
        /// </summary>
        NotFound = 3,
        /// <summary>
        /// Indicates that a conflict has occurred, typically representing a state where an operation cannot be
        /// completed due to conflicting data or conditions.
        /// </summary>
        /// <remarks>This value is commonly used to signal a conflict in scenarios such as resource
        /// updates, concurrent modifications, or when an action cannot proceed because it would violate consistency
        /// constraints.</remarks>
        Conflict = 4,
        /// <summary>
        /// Indicates that the request was not authorized and access is denied.
        /// </summary>
        Unauthorized = 5,
        /// <summary>
        /// Indicates that the request was understood but is refused due to insufficient permissions.
        /// </summary>
        /// <remarks>This value typically corresponds to HTTP status code 403. It is returned when the
        /// server recognizes the request but the client does not have authorization to access the requested
        /// resource.</remarks>
        Forbidden = 6,
    }
}
