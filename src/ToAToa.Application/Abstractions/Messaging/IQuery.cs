using MediatR;

namespace ToAToa.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
