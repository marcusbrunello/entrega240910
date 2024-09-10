using System.Data;

namespace MetaBank.BusinessLogic.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}