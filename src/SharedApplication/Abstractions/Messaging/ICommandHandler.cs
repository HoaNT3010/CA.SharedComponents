using MediatR;
using SharedDomain.Utilities;

namespace SharedApplication.Abstractions.Messaging
{
    /// <summary>
    /// Handles commands that do not return a value.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    public interface ICommandHandler<in TCommand>
        : IRequestHandler<TCommand, Result>
        where TCommand : ICommand;

    /// <summary>
    /// Handles commands that return a value of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <typeparam name="TResponse">The response type returned by the command.</typeparam>
    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>;
}
