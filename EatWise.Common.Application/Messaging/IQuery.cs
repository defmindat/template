using EatWise.Common.Domain;
using MediatR;

namespace EatWise.Common.Application.Messaging;

public interface IQuery<TRepsonse> : IRequest<Result<TRepsonse>>;

