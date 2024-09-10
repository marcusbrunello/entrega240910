using Azure.Core;
using MediatR;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.BusinessLogic.Features.Accounts.Commands.Withdraw;
using MetaBank.BusinessLogic.Features.Accounts.Queries.GetAccountDetails;
using MetaBank.BusinessLogic.Features.Withdrawals.Queries.GetWithdrawalsPaginated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Swashbuckle.AspNetCore.Annotations;


namespace MetaBank.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly ISender _sender;

    public AccountController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("balance")]
    [SwaggerOperation(
            Summary = "Account details by card number",
            Description = "Will respond with account info.",
            OperationId = "Get",
            Tags = new string[] { "Get" })]
    [SwaggerResponse(200, "Successful", typeof(IQuery<AccountResponse>))]
    [SwaggerResponse(404, "Notfound")]
    public async Task<IActionResult> GetAccountDetails(
        string cardNumber,
        CancellationToken cancellationToken)
    {
		if (NotAuthorized(cardNumber)) return Unauthorized();

		var query = new GetAccountDetailsQuery(cardNumber);
		
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

	[HttpPost("withdraw")]
	[SwaggerOperation(
		Summary = "Withdraw",
		Description = "Will respond with withdrawal details.",
		OperationId = "Post",
		Tags = new string[] { "Post" })]
	[SwaggerResponse(200, "Successful", typeof(ICommand<WithdrawResponse>))]
	[SwaggerResponse(404, "Notfound")]
	public async Task<IActionResult> Withdraw(
	WithdrawCommand request,
	CancellationToken cancellationToken)
	{
		if (NotAuthorized(request.CardNumber)) return Unauthorized();

		var command = new WithdrawCommand(request.CardNumber, request.AmountInCents);
		var result = await _sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest();
		}

		return Ok(result.Value);
	}

	[HttpGet("withdrawals")]
	[SwaggerOperation(
	Summary = "Withdrawals",
	Description = "Will respond with withdrawal details.",
	OperationId = "Get",
	Tags = new string[] { "Get" })]
	[SwaggerResponse(200, "Successful", typeof(IQuery<IEnumerable<WithdrawalsResponse>>))]
	[SwaggerResponse(404, "Notfound")]
	public async Task<IActionResult> WithdrawalsPaginated(
		string CardNumber,
		int PageNumber,
		CancellationToken cancellationToken)
	{
		if (NotAuthorized(CardNumber)) return Unauthorized();

		var query = new WithdrawalsPaginatedQuery(CardNumber, PageNumber);
		var result = await _sender.Send(query, cancellationToken);

		if (result == null || result.IsFailure || result?.Value.Count() < 1)
		{
			return NotFound(result?.Error);
		}

		return Ok(result!.Value);
	}
	private bool NotAuthorized(string cardNumber)
	{
		return User.Identity?.Name != cardNumber;
	}

}