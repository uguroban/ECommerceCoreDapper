using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class ProductAllJoinRepository : IGenericRepository<ProductAllJoin>
    {
        private readonly IConfiguration configuration;

        public ProductAllJoinRepository(IConfiguration configuration)
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
        public Task AddAsync(ProductAllJoin entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ProductAllJoin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductAllJoin> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductAllJoin> GetByIdAsync(int id)
        {
           using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =await dbConnection.QueryFirstOrDefaultAsync<ProductAllJoin>("select * from vw_ProductAllJoin where ProductID=@ProductID", new { ProductID = id });
                return result;
            }
        }

        public async Task<List<ProductAllJoin>> GetRelatedProducts(int id)
        {
            using (IDbConnection dbConnection=Connection)
            {
                dbConnection.Open();
                var result =await dbConnection.QueryAsync<ProductAllJoin>("select * from vw_ProductAllJoin where CategoryID in(select CategoryID from Products where ProductID=@ProductID) and ProductID!=@ProductID", new { ProductID = id });
                return result.ToList();
            }
        }

        public Task<ProductAllJoin> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdataAsync(ProductAllJoin entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Sizes>> GetAllSizes()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Sizes>("select * from Sizes");
                dbConnection.Close();
                return result.ToList();

            }
        }

        public async Task<IReadOnlyList<Colors>> GetAllColors()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Colors>("select * from Colors");
                dbConnection.Close();
                return result.ToList();

            }
        }

        public async Task<IReadOnlyList<Products>> GetAllProducts()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Products>("select * from Products");
                return result.ToList();

            }

        }

      
         public int pageSizeHome = 0;
         public int pageSizeSub = 0;
        public async Task<List<ProductAllJoin>> GetCategorizedProducts(string categoryName,string page,int pageNumber)
        {
            int skipHome= pageNumber * pageSizeHome;
            int skipSub=pageNumber* pageSizeSub;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            List<ProductAllJoin> list = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            using (IDbConnection dbConnection= Connection)
            {
                dbConnection.Open();
                if (page == "HomePage")
                {
                    if (categoryName == "BestSeller")
                    {
                       var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where SupplierID in (select TOP 1 SupplierID from Products group by SupplierID " +
                            "order by SUM(TopSell) desc) order by ProductID desc");

                        list=result.ToList();                        
                    }
                    else if (categoryName=="Featured")
                    {
                        var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where Featured>7 order by Featured desc");
                        list = result.ToList();
                    }
                    else if (categoryName == "Sale")
                    {
                        var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where TopSell>0 order by TopSell desc");
                        list= result.ToList();
                    }
                    else if (categoryName == "TopRate")
                    {
                        var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where Ranking>7 order by Ranking desc");
                        list = result.ToList();
                    }
                }
                else
                {
                    //pageNumber=0 : Load More butonun tıklanmadıysa
                    //else butona tıklandıysa
                   
                        if (categoryName == "AllProducts")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        //LoadMore butona basıldıysa
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                           
                        }
                        if (categoryName == "Women")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where WomenorMen=1 order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where WomenorMen=1 order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                            
                        }
                        if (categoryName == "Men")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where WomenorMen=0 order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where WomenorMen=0 order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                            
                        }
                        if (categoryName == "Bag")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=1 order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=1 order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                            
                        }
                        if (categoryName == "Shoes")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=2 order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=2 order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                           
                        }
                        if (categoryName == "Watches")
                        {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=3 order by ProductID asc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where CategoryID=3 order by ProductID asc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                            
                        }

                    if (categoryName == "Features")
                    {
                        if (pageNumber == 0)
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where Featured>7 order by Discount desc offset " + pageNumber + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }
                        else
                        {
                            var result = await dbConnection.QueryAsync<ProductAllJoin>("select * from Products where Featured>7 order by Discount desc offset " + skipSub + " rows fetch next " + pageSizeSub + " rows only");
                            list = result.ToList();
                        }

                    }


                }
            }

#pragma warning disable CS8603 // Possible null reference return.
            return list;
#pragma warning restore CS8603 // Possible null reference return.

        }

       
    }
}
