using MediatR;
using MetaBank.Model.Base;

namespace MetaBank.BusinessLogic.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}