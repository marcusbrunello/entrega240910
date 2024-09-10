using MediatR;
using MetaBank.BusinessLogic.Contracts.DTOs;
using MetaBank.BusinessLogic.Features.Cards.Commands.LogInCard;
using MetaBank.BusinessLogic.Features.Cards.Commands.RegisterCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaBank.Api.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class CardsController : ControllerBase
{
	private readonly ISender _sender;

	public CardsController(ISender sender)
	{
		_sender = sender;
	}

	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> Register(
		RegisterCardRequest request,
		CancellationToken cancellationToken)
	{
		var command = new RegisterCardCommand(
			request.Number,
			request.PinToken,
			request.Holder);

		var result = await _sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Ok(result.Value);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> LogIn(
		LogInCardRequest request,
		CancellationToken cancellationToken)
	{
		var command = new LogInCardCommand(request.CardNumber, request.Pin);

		var result = await _sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return Unauthorized(result.Error);
		}

		return Ok(result.Value);
	}
}
