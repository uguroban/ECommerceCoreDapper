using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceCoreDapper.Models.Repository
{
    public class ProductRepository : IGenericRepository<Products>
    {
        private readonly IConfiguration configuration;
        //public static IConfiguration staticConf { get; private set;}

        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            //staticConf = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            //string connString = ProductRepository.staticConf.GetConnectionString("ECommerceDBCon");
            ProviderName = "System.Data.SqlClient";
        }

      

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        //public static IDbConnection conn
        //{
        //    get
        //    {
        //        return new SqlConnection(ProductRepository.staticConf.GetConnectionString("ECommerceDBCon"));
        //    }
        //}

        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

        public async Task AddPhotos(ProductPhotos ph)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("sp_InsertPhoto", ph, commandType: CommandType.StoredProcedure);
            }

        }
        public async Task<int> AddAsync(Products entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductID", DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@BrandID", entity.BrandID);
                param.Add("@ProductName", entity.ProductName);
                param.Add("@ProductDescription", entity.ProductDescription);
                param.Add("@SupplierID", entity.SupplierID);
                param.Add("@CategoryID", entity.CategoryID);
                param.Add("@QuantityPerUnit", entity.QuantityPerUnit);
                param.Add("@UnitPrice", entity.UnitPrice);
                param.Add("@RetailPrice", entity.RetailPrice);
                param.Add("@SizeID", entity.SizeID);
                param.Add("@ColorID", entity.ColorID);
                param.Add("@Discount", entity.Discount);
                param.Add("@UnitWeight", entity.UnitWeight);
                param.Add("@UnitsInStock", entity.UnitsInStock);
                param.Add("@ProductAvailable", entity.ProductAvailable);
                param.Add("@DiscountAvailable", entity.DiscountAvailable);
                param.Add("@TopSell", entity.TopSell);
                param.Add("@Featured", entity.Featured);
                param.Add("@Note", entity.Note);
                param.Add("@Ranking", entity.Ranking);
                param.Add("@PhotoPath", entity.PhotoPath);
                param.Add("@WomenorMen", entity.WomenorMen);


                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync("sp_InsertProduct", entity, commandType: CommandType.StoredProcedure);
                var pID = param.Get<int>("@ProductID");
                return pID;

            }

        }

        public void UpdatePhotos(ProductPhotos ph)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Execute("update ProductPhotos set ProductID=@ProductID,PhotoPath=@PhotoPath where ProductID=@ProductID", new { ProductID = ph.ProductID, PhotoPath = ph.PhotoPath });

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_DeleteProduct", id, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }
        }

        public List<SearchProduct> GetSearchPrd(string id)
        {
            
            using (IDbConnection dbConnection=Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Query<SearchProduct>("select top 10 * from vw_DetailSrchProduct where SearchName like @id", new {id="%"+id+"%"});
                return result.ToList();
            }
        }
        public async Task<IReadOnlyList<Products>> GetAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<IReadOnlyList<Products>>("select * from vw_GetProducts");
                return result;

            }

        }

        public async Task<Products> GetByDateAsync(DateTime date)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Products>("select * from vw_GetProducts Order By AddDate==" + date + " Desc");
                dbConnection.Close();
                return result;

            }
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<Products>("select * from Products where ProductID=@ProductID", new { ProductID = id });
                return result;

            }

        }

        public async Task<Products> GetByNameAsync(string name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<Products>("select * from vw_GetProducts where ProductName==" + name + "");
                dbConnection.Close();
                return result;

            }
        }

        public async Task UpdataAsync(Products entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync("sp_UpdateProduct", entity, commandType: CommandType.StoredProcedure);

            }
        }

        Task IGenericRepository<Products>.AddAsync(Products entity)
        {
            throw new NotImplementedException();
        }

        public void IncreaseFeatured(int id)
        {
            using (IDbConnection dbConnection=Connection)
            {
                dbConnection.Open();
                Products prd = new Products();
                prd = dbConnection.QueryFirstOrDefault<Products>("select * from Products where ProductID=@ProductID", new { ProductID = id });
                prd.Featured=prd.Featured+1;
                dbConnection.QueryFirstOrDefault<Products>("update Products set Featured=@Featured where ProductID=@ProductID", new { Featured = prd.Featured, ProductID = id });
            }
        }

        public static string wish;
        public Boolean AddWishList(int id)
        {
            bool isAdded = false;
            string[] wishList = wish.Split('&');
            for (int i = 0; i < wishList.Length; i++)
            {
                
                if (wishList[i]==id.ToString())
                {
                    isAdded= true;
                }
            }
            return isAdded;
        }

        public List<Products> WishedList()
        {
            List<Products> list = new List<Products>();
            string[] wishList = wish.Split('&');
            for (int i = 0; i < wishList.Length; i++)
            {
               
                if (!String.IsNullOrEmpty(wishList[i]))
                {
                    int wishId = int.Parse(wishList[i]);
                    using (IDbConnection dbConnection=Connection)
                    {
                        Products prd = dbConnection.QueryFirstOrDefault<Products>("select * from Products where ProductID=@ProductID", new { ProductID = wishId });
                        list.Add(prd);
                    }
                }
            }
            return list;
        }

        public void DeleteWishedList(int dId)
        {
            string[] wishList = wish.Split('&');
            string newList = "";
            int count = 1;
            for (int i = 0; i < wishList.Length; i++)
            {
                if (wishList[i]!=dId.ToString())
                {
                    if (count==1)
                    {
                        newList += wishList[i];
                        count++;
                    }
                    else
                    {
                        newList += "&" + wishList[i];
                    }
                   
                }
            }

        wish=newList;

        }

      
    }
}
