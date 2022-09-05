using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ECommerceCoreDapper.Models.Repository
{
	public class ProductsOrdersRepository : IGenericRepository<ProductsOrders>
	{
		private readonly IConfiguration configuration;
        

		public ProductsOrdersRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
			
			ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
			ProviderName = "System.Data.SqlClient";
		}



		public IDbConnection Connection
		{
			get
			{
				return new SqlConnection(ConnectionString);
			}
		}
        public string ConnectionString { get; private set; }
		public string ProviderName { get; private set; }

		public async Task AddOrdAsync(Orders entity)
		{
            using (IDbConnection dbConnection=Connection)
            {
                dbConnection.Open();
                var result =await dbConnection.ExecuteScalarAsync("insert into Orders(OrderGroupID,ProductID,CustomerID,OrderDate,Quantity) values(@OrderGroupID,@ProductID,@CustomerID," +
                    "@OrderDate,@Quantity)", new
                    {
                        OrderGroupID = entity.OrderGroupGUID,
                        ProductID = entity.ProductID,
                        CustomerID = entity.CustomerID,
                        OrderDate = entity.OrderDate,
                        Quantity = entity.Quantity
                    });               
            }
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<ProductsOrders>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ProductsOrders> GetByDateAsync(DateTime date)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ProductsOrders>> GetByCustomerIdAsync(int id)
		{
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =await dbConnection.QueryAsync<ProductsOrders>("select * from vw_ProductOrder where CustomerID=@CustomerID", new { CustomerID = id });
                return result.ToList();
            }
        }

		public Task<ProductsOrders> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

		public Task UpdataAsync(ProductsOrders entity)
		{
			throw new NotImplementedException();
		}

        public static string cart;
        public static int quantity=1;
        public Boolean AddCartList(int id)
        {
            bool isAdded = false;
            string[] cartList = cart.Split('&');         
                for (int i = 0; i < cartList.Length; i++)
                {
                    string[] cart_pID_qt = cartList[i].Split('=');
                    if (cart_pID_qt[0] == id.ToString())
                    {
                        quantity = Convert.ToInt32(cart_pID_qt[1]);
                        quantity++;
                        cart_pID_qt[1] = quantity.ToString();
                        isAdded = true;
                    }
                }
            
          
            return isAdded;
        }

        public  List<ProductsOrders> CartList()
        {


            List<ProductsOrders> clist = new List<ProductsOrders>();

               string[] cartList = cart.Split('&');
               for (int i = 0; i < cartList.Length; i++)
                {
                  string[] cart_pID_qt = cartList[i].Split('=');              
                                                
                    if (!String.IsNullOrEmpty(cart_pID_qt[0]))
                    {
                    int cartId = int.Parse(cart_pID_qt[0]);
                    using (IDbConnection dbConnection = Connection)
                        {
                            Products prd = dbConnection.QueryFirstOrDefault<Products>("select * from Products where ProductID=@ProductID", new { ProductID = cartId });
                            ProductsOrders ord = new ProductsOrders();
                            ord.ProductID = prd.ProductID;
                            ord.ProductName = prd.ProductName;
                            ord.UnitPrice = prd.UnitPrice;
                            ord.Quantity = Convert.ToInt32(cart_pID_qt[1]);
                            ord.Discount = Convert.ToInt32(prd.Discount);
                            ord.PhotoPath = prd.PhotoPath;
                            clist.Add(ord);
                        }
                    }
                }

           
    
            return clist;
        }

        public void DeleteCartList(int dId)
        {
            string[] cartList = cart.Split('&');
            string newcList = "";
            int count = 1;
            for (int i = 0; i < cartList.Length; i++)
            {
                string[] cart_pID_qt = cartList[i].Split('=');
                if (cart_pID_qt[0] != dId.ToString())
                {
                    if (count == 1)
                    {
                        newcList += cart_pID_qt[0] + "=" + cart_pID_qt[1];
                        count++;
                    }
                    else
                    {
                        newcList += "&" + cart_pID_qt[0] + "=" + cart_pID_qt[1];
                    }

                }
            }

            cart = newcList;

        }

        public async Task<string> AddOrder(int id)
        {
           
            List<ProductsOrders> list = CartList();
            string OrderGroupID = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
            foreach (var item in list)
            {               
                Orders ord = new Orders();
                ord.OrderDate = DateTime.Now;
                ord.OrderGroupGUID = OrderGroupID;
                ord.CustomerID = id;
                ord.ProductID = item.ProductID;
                ord.Quantity = item.Quantity;
                await AddOrdAsync(ord);                
            }          
            return OrderGroupID;
        }

       

        public Task AddAsync(ProductsOrders entity)
        {
            throw new NotImplementedException();
        }

        Task<ProductsOrders> IGenericRepository<ProductsOrders>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
