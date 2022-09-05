using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class CreditCardRepository : IGenericRepository<CreditCards>
    {
        private readonly IConfiguration configuration;

        public CreditCardRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.Client";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public async Task AddAsync(CreditCards entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("insert into CreditCards(CreditCardNumber,CExpYear,CExpMonth,CVV)" +
                    "values(@CreditCardNumber,@CExpYear,@CExpMonth,@CVV,@CardName)",
                    new { entity.CreditCardNumber, entity.CExpYear, entity.CExpMonth, entity.CVV, entity.CardName });
                dbConnection.Close();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("update from CreditCards set Active=0 where CreditCardID=@CreditCardID", id);
                dbConnection.Close();
            }
        }

        public Task<IReadOnlyList<CreditCards>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CreditCards> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<CreditCards> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<CreditCards>("select * from CreditCards where CreditCardID=@CreditCardID", id);
                dbConnection.Close();
                return result;
            }
        }

        public Task<CreditCards> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdataAsync(CreditCards entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("update from CreditCards set CreditCardNumber=@CreditCardNumber," +
                    "CExpYear=@CExpYear,CExpMonth=@CExpMonth,CVV=@CVV,Active=@Active,CardName=@CardName" +
                    "where CreditCardID=@CreditCardID,", new { entity.CreditCardNumber, entity.CExpYear, entity.CExpMonth, entity.CVV, entity.Active, entity.CardName, entity.CreditCardID });
                dbConnection.Close();
            }
        }
    }
}
