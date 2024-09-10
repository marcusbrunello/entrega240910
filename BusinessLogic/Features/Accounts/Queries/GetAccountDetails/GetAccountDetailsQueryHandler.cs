using Dapper;
using MetaBank.BusinessLogic.Abstractions.Data;
using MetaBank.BusinessLogic.Abstractions.Messaging;
using MetaBank.Model.Base;
using MetaBank.Model.Entities;
using System.Security.Principal;

namespace MetaBank.BusinessLogic.Features.Accounts.Queries.GetAccountDetails;

internal sealed class GetAccountDetailsQueryHandler
    : IQueryHandler<GetAccountDetailsQuery, AccountResponse>
{

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAccountDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AccountResponse>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();


        const string sql = "SELECT Accounts.Number, FORMAT(CAST(Accounts.CashAvailable AS DECIMAL(18, 2)) / 100, 'N2') AS CashAvailable, Cards.Holder, Accounts.UpdatedAt AS LastWithdrawal FROM Accounts INNER JOIN Cards ON Accounts.CardId = Cards.Id WHERE Cards.Number = @CardNumber";

        var account = await connection.QueryFirstOrDefaultAsync<AccountResponse>(
            sql,
            new
            {
                request.CardNumber
            });

        if (account is null)
        {
            return Result.Failure<AccountResponse>(AccountErrors.NotFound);
        }

        return account;

    }
}