using EatWise.Common.Domain;
using MediatR;

namespace EatWise.Common.Application.Messaging;

public interface IQueryHandler<TResponse> : IRequest<Result<TResponse>>;
