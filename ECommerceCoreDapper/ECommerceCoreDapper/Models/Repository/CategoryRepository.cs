using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class CategoryRepository : IGenericRepository<Categories>
    {
        private readonly IConfiguration configuration;

        public CategoryRepository(IConfiguration configuration)
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


        public async Task AddAsync(Categories entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sqlquery = "insert into Categories(ParentID,CategoryName,Active) values(@ParentID,@CategoryName,@Active)";
                var result = await dbConnection.ExecuteAsync(sqlquery, new { ParentID = entity.ParentID, CategoryName = entity.CategoryName, Active = 1 });

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync("delete from Categories where CategoryID=@CategoryID", new { CategoryID = id });
            }
        }

        public async Task<IReadOnlyList<Categories>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Categories>("select * from Categories");
                return result.ToList();
            }

        }

        public List<Categories> GetAllCategories()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Query<Categories>("select * from Categories");
                return result.ToList();
            }

        }

        public Task<Categories> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Categories>("select * from Categories where CategoryID=@CategoryID", new { CategoryID = id });
                return result;
            }
        }

        public async Task<Categories> GetByNameAsync(string name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Categories>("select * from Categories where CategoryName=@CategoryName", name);
                dbConnection.Close();
                return result;
            }
        }

        public Boolean CheckCategoryName(string categoryName)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                bool result = dbConnection.Query("select * from Categories where CategoryName=@CategoryName", new { CategoryName = categoryName.ToLower() }).Any();
                return result;
            }
        }
        public async Task UpdataAsync(Categories entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Categories>("update Categories set ParentID=@ParentID,CategoryName=@CategoryName where CategoryID=@CategoryID",
                    new { ParentID = entity.ParentID, CategoryName = entity.CategoryName, CategoryID = entity.CategoryID });

            }
        }


    }
}
