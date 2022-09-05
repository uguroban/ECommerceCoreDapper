using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceCoreDapper.Models.Repository
{
    public class CustomerRepository : IGenericRepository<Customers>
    {
        private readonly IConfiguration configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.SqlClient";
        }

        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public async Task AddAsync(Customers entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                entity.Password = MD5Crypto(entity.Password);
                entity.ConfirmPassword = MD5Crypto(entity.ConfirmPassword);
#pragma warning restore CS8604 // Possible null reference argument.
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_InsertCustomer", entity, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }

        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("update from Customers set Active=0 where CustomerID=@CustomerID", id);
                dbConnection.Close();
            }
        }

        public async Task<IReadOnlyList<Customers>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Customers>(" select * vw_GetCustomers");
                return result.ToList();
            }
        }

        public bool CheckEmail(string mail)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Query("select Email from Customers where EMail=@EMail", new { EMail = mail });
                return result.Count() == 0 ? true : false;
            }
        }

        public async Task<Customers> GetByDateAsync(DateTime date)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Customers>("select * from vw_GetCustomers where DateEntered=@DateEntered", date);
                dbConnection.Close();
                return result;

            }
        }

        public async Task<Customers> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Customers>("select * from Customers where CustomerID=@CustomerID",new {CustomerID=id });
                return result;

            }
        }

        public Customers CheckLogin(Customers customers)
        {
            using (IDbConnection dbConnection = Connection)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                customers.Password = MD5Crypto(customers.Password);
#pragma warning restore CS8604 // Possible null reference argument.
                dbConnection.Open();
                var result = dbConnection.QueryFirstOrDefault<Customers>("select * from Customers where Email=@EMail and Password=@Password",
                    new { EMail = customers.EMail, Password = customers.Password });
                return result;
            }
        }

        public static string MD5Crypto(string password)
        {
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete

            byte[] btr = Encoding.UTF8.GetBytes(password);
            btr = md5.ComputeHash(btr);

            StringBuilder sb = new StringBuilder();

            foreach (byte item in btr)
            {
                sb.Append(item.ToString("x2").ToLowerInvariant());
            }
            return sb.ToString();
        }

        public Task<Customers> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTCKNAsync(Customers entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("Update Customers set TCKN=@TCKN where CustomerID=@CustomerID", new {TCKN=entity.TCKN,CustomerID=entity.CustomerID} );
                dbConnection.Close();

            }
        }

        public async Task UpdataAsync(Customers entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_UpdateCustomer", commandType: CommandType.StoredProcedure);
                dbConnection.Close();

            }
        }
    }
}
