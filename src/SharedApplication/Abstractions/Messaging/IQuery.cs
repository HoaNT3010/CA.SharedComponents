using MediatR;
using SharedDomain.Utilities;

namespace SharedApplication.Abstractions.Messaging
{
    /// <summary>
    /// Marker interface for queries that return a <see cref="Result{TResponse}"/>.
    /// Queries represent a request for data and should not cause side effects.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
