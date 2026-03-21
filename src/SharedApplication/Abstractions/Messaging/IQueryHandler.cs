using MediatR;
using SharedDomain.Utilities;

namespace SharedApplication.Abstractions.Messaging
{
    /// <summary>
    /// Handles queries of type <typeparamref name="TQuery"/> and returns a <see cref="Result{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TQuery">The query type.</typeparam>
    /// <typeparam name="TResponse">The response type returned by the query.</typeparam>
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>;
}
