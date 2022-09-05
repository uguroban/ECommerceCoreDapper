using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class OrderRepository : IGenericRepository<Orders>
    {
        private readonly IConfiguration configuration;

        public OrderRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.Client";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public async Task AddAsync(Orders entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_InsertOrder", entity, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Orders>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<IReadOnlyList<Orders>>("select * from vw_GetOrders");
                dbConnection.Close();
                return result;
            }
        }

        public async Task<Orders> GetByDateAsync(DateTime date)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Orders>("select * from vw_GetOrders where OrderDate=@OrderDate", date);
                dbConnection.Close();
                return result;
            }
        }

        public async Task<Orders> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Orders>("select * from vw_GetOrders where OrderID=@OrderID", id);
                dbConnection.Close();
                return result;
            }
        }

        public Task<Orders> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdataAsync(Orders entity)
        {

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_UpdateOrder", entity, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
        }
    }
}
