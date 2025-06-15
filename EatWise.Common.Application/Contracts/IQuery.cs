using EatWise.Common.Domain;
using MediatR;

namespace EatWise.Common.Application.Contracts;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
