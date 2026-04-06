using MediatR;
using SharedDomain.Utilities;

namespace SharedApplication.Abstractions.Messaging
{
    /// <summary>
    /// Marker interface for commands that do not return a value.
    /// Commands are requests that represent an intent to perform an action.
    /// </summary>
    public interface ICommand : IRequest<Result>;

    /// <summary>
    /// Marker interface for commands that return a value of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response produced by the command.</typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
}
