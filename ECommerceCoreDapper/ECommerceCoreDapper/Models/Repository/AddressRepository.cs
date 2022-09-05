using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class AddressRepository : IGenericRepository<Address>
    {
        private readonly IConfiguration configuration;

        public AddressRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.SqlClinet";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        public Task AddAsync(Address entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAddress(Address entity)
        {
            using (IDbConnection dbConnection = Connection)
            {

                DynamicParameters param = new DynamicParameters();

                param.Add("@AddressID", DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@AddressName", entity.AddressName);
                param.Add("@Address1", entity.Address1);
                param.Add("@Address2", entity.Address2);
                param.Add("@City", entity.City);
                param.Add("@State", entity.State);
                param.Add("@PostalCode", entity.PostalCode);
                param.Add("@Country", entity.Country);
                param.Add("@Phone", entity.Phone);
                param.Add("@Fax", entity.Fax);
                param.Add("@Email", entity.Email);
                param.Add("@AddressName", entity.AddressName);
                param.Add("@IsBill", entity.IsBill);
                param.Add("@IsShip", entity.IsShip);
                param.Add("@Active", entity.Active);



                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("sp_InsertAddress", param, commandType: CommandType.StoredProcedure);
                var AddressID = param.Get<int>("@AddressID");
                return AddressID;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_DeleteAddress", id, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
        }

        public async Task<IReadOnlyList<Address>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<IReadOnlyList<Address>>("select * from vw_GetAddresses");
                dbConnection.Close();
                return result;
            }
        }

        public Task<Address> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> GetById(int? id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Address>("select * from Address where AddressID=@AddressID", new { AddressID = id });
                return result;
            }
        }

        public async Task<Address> GetByNameAsync(string name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Address>("select * from vw_GetAddresses where AddressName=" + name + "");
                dbConnection.Close();
                return result;
            }
        }

        public async Task UpdataAsync(Address entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("update Address set AddressName=@AddressName,Address1=@Address1,Address2=@Address2," +
                    "City=@City,State=@State,PostalCode=@PostalCode,Country=@Country,Phone=@Phone,Fax=@Fax,Email=@Email,IsBill=@IsBill," +
                    "IsShip=@IsShip,Active=@Active",
                    new
                    {
                        AddressID = entity.AddressID,
                        AddressName = entity.AddressName,
                        Address1 = entity.Address1,
                        Address2 = entity.Address2,
                        City = entity.City,
                        State = entity.State,
                        PostalCode = entity.PostalCode,
                        Country = entity.Country,
                        Phone = entity.Phone,
                        Fax = entity.Fax,
                        Email = entity.Email,
                        IsBill = entity.IsBill,
                        IsShip = entity.IsShip,
                        Active = entity.Active,
                    });

            }
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Address>("select * from Address where AddressID=@AddressID", new { AddressID = id });
                return result;
            }
        }
    }
}
