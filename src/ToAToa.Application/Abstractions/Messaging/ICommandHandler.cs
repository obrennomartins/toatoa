using MediatR;

namespace ToAToa.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>;
