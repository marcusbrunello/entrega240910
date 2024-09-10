using MediatR;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.Model.Base;

namespace MetaBank.BusinessLogic.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
	where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
	where TCommand : ICommand<TResponse>
{
}