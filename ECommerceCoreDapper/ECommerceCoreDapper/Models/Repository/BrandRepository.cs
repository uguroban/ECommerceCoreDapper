using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class BrandRepository : IGenericRepository<Brands>
    {
        private readonly IConfiguration configuration;

        public BrandRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.SqlClient";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public async Task AddAsync(Brands entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("insert into Brands(BrandName,BrandDescription,Logo) " +
                    "values(@BrandName,@BrandDescription,@Logo)", new { BrandName = entity.BrandName, BrandDescription = entity.BrandDescription, Logo = entity.Logo });

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("delete from Brands where BrandID=@BrandID", id);
                dbConnection.Close();
            }
        }

        public async Task<IReadOnlyList<Brands>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Brands>("select * from Brands");
                return result.ToList();

            }
        }

        public Task<Brands> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Brands> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Brands>("select * from Brands where BrandID=@BrandID", new { BrandID = id });
                return result;

            }
        }

        public async Task<Brands> GetByNameAsync(string name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Brands>("select * from Brands where BrandName=@BrandName", new { BrandName = name });
                return result;

            }
        }

        public Boolean CheckBrandName(string brandName)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                bool result = dbConnection.Query("select * from Brands where BrandName=@BrandName", new { BrandName = brandName.ToLower() }).Any();
                return result;
            }
        }

        public async Task UpdataAsync(Brands entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("update Brands set BrandName=@BrandName,BrandDescription=@BrandDescription," +
                    "Logo=@Logo where BrandID=@BrandID", new { BrandName = entity.BrandName, BrandDescription = entity.BrandDescription, Logo = entity.Logo, BrandID = entity.BrandID });

            }
        }
    }
}
