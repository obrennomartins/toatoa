using MediatR;

namespace ToAToa.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
