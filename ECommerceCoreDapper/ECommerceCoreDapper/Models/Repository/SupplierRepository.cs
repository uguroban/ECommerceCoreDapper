using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class SupplierRepository : IGenericRepository<Suppliers>
    {
        private readonly IConfiguration configuration;

        public SupplierRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.SqlClient";
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        public async Task AddAsync(Suppliers entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("insert into Suppliers(CompanyName,ContactFName,ContactLName,ContactTitle," +
                    "AddressID,WebSite,DiscountType,DiscountRate,DiscountAvailable,Logo,Note,Ranking) values(@CompanyName,@ContactFName,@ContactLName," +
                    "@ContactTitle,@AddressID,@WebSite,@DiscountType,@DiscountRate,@DiscountAvailable,@Logo,@Note,@Ranking)", new
                    {
                        CompanyName = entity.CompanyName,
                        ContactFName = entity.ContactFName,
                        ContactLName = entity.ContactLName,
                        ContactTitle = entity.ContactTitle,
                        AddressID = entity.AddressID,
                        WebSite = entity.WebSite,
                        DiscountType = entity.DiscountType,
                        DiscountRate = entity.DiscountRate,
                        DiscountAvailable = entity.DiscountAvailable,
                        Logo = entity.Logo,
                        Note = entity.Note,
                        Ranking = entity.Ranking
                    });

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_DeleteSupplier", id, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
        }

        public async Task<IReadOnlyList<Suppliers>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Suppliers>("select * from Suppliers");
                return result.ToList();

            }

        }

        public Task<Suppliers> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Suppliers> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Suppliers>("select * from Suppliers where SupplierID=@SupplierID",
                    new { SupplierID = id });
                return result;

            }
        }
        public async Task<Suppliers> GetByView(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Suppliers>("select * from vw_GetSupplierAddress where SupplierID=@SupplierID",
                    new { SupplierID = id });
                return result;

            }
        }

        public async Task<Suppliers> GetByNameAsync(string name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Suppliers>("select  from vw_GetSuppliers where CompanyName=" + name + "");
                dbConnection.Close();
                return result;

            }
        }

        public async Task UpdataAsync(Suppliers entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("update Suppliers set CompanyName=@CompanyName,ContactFName=@ContactFName,ContactLName=@ContactLName,ContactTitle=@ContactTitle," +
                    "AddressID=@AddressID,WebSite=@WebSite,DiscountType=@DiscountType,DiscountRate=@DiscountRate,DiscountAvailable=@DiscountAvailable," +
                    "Logo=@Logo,Note=@Note,Ranking=@Ranking where SupplierID=@SupplierID", new
                    {
                        SupplierID = entity.SupplierID,
                        CompanyName = entity.CompanyName,
                        ContactFName = entity.ContactFName,
                        ContactLName = entity.ContactLName,
                        ContactTitle = entity.ContactTitle,
                        AddressID = entity.AddressID,
                        WebSite = entity.WebSite,
                        DiscountType = entity.DiscountType,
                        DiscountRate = entity.DiscountRate,
                        DiscountAvailable = entity.DiscountAvailable,
                        Logo = entity.Logo,
                        Note = entity.Note,
                        Ranking = entity.Ranking
                    });
            }
        }
    }
}
