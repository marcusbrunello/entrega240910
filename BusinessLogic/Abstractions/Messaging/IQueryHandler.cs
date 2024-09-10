using MediatR;
using MetaBank.Model.Base;

namespace MetaBank.BusinessLogic.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}