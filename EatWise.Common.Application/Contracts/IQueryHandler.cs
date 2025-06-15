using EatWise.Common.Domain;
using MediatR;

namespace EatWise.Common.Application.Contracts;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
