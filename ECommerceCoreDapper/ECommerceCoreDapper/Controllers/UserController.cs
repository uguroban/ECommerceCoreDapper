using ECommerceCoreDapper.Models;
using ECommerceCoreDapper.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerceCoreDapper.MVVM;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;

namespace ECommerceCoreDapper.Controllers
{
    public class UserController : BaseController
    {


        //private readonly ILogger<UserController> _logger;
        private readonly IGenericRepository<Customers> repository;
        private readonly IConfiguration configuration;

        public UserController(IGenericRepository<Customers> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "Sytem.Data.SqlClient";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }

     

        [Route("Login")]
        public IActionResult Login()
        {
            ViewBag.register = TempData["register"];
            return View();
        }

        [HttpPost]
        public IActionResult Login(Customers cus)
        {

            CustomerRepository cr = new CustomerRepository(configuration);
            Customers customers = cr.CheckLogin(cus);
            if (customers == null)
            {
                ViewBag.logging = "basarisiz";
                              
            }

            else
            {
                ViewBag.logging = "basarili";
                HttpContext.Session.SetString("userID", customers.CustomerID.ToString());
#pragma warning disable CS8604 // Possible null reference argument.
                HttpContext.Session.SetString("EMail", customers.EMail);
#pragma warning restore CS8604 // Possible null reference argument.

                if (customers.IsAdmin == true)
                {
                    return RedirectToAction("ManagementPanel", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                  
                }

            }
            return View();

        }

        public void SetCookie(string key,string value,int day)
        {
            //Erase the data in the cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(day);
            option.Secure = true;
            option.IsEssential = true;
            option.HttpOnly = true;
            Response.Cookies.Append(key,value, option);
        }


        public IActionResult Mywish(int id) 
        {
            
            ProductRepository pr = new ProductRepository(configuration);
            Products prd = new Products();

            pr.IncreaseFeatured(id);

            CookieOptions ckOpt = new CookieOptions();
            ckOpt.Expires = DateTime.Now.AddDays(5);
            Response.Cookies.Append("wished", id.ToString(),ckOpt);

            var cookie=Request.Cookies["wished"];
            if (String.IsNullOrEmpty(cookie))
            {
                Response.Cookies.Append("wished", id.ToString());
                Response.Cookies.Append("wished", id.ToString(), ckOpt);
                ProductRepository.wish = id.ToString();

            }
            else
            {
                //ProductRepository.wish = Request.Cookies["wished"];
                ProductRepository.wish = cookie;
                //first time added
                if (pr.AddWishList(id) == false)
                {
                    if (!String.IsNullOrEmpty(cookie))
                    {
                        ProductRepository.wish = cookie + '&' + id.ToString();
                    }
                    else
                    {
                        ProductRepository.wish = id.ToString();
                    }

                    Response.Cookies.Append("wished", ProductRepository.wish, ckOpt);

                }
                else
                {
                    pr.DeleteWishedList(id);
                    cookie = ProductRepository.wish;
                    //if (!String.IsNullOrEmpty(cookie))
                    //{
                    //    ProductRepository.wish = cookie + '&' + id.ToString();
                    //}
                    //else
                    //{
                    //    ProductRepository.wish = id.ToString();
                    //}

                    Response.Cookies.Append("wished", ProductRepository.wish);
                }
            }

          

            return RedirectToAction("Index", "Home");
        }

        [Route("WishList")]
        public IActionResult WishList()
        {
            if (HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }
            ProductRepository pr = new ProductRepository(configuration);

            string scid = Request.Query["scid"];
            if (scid!=null)
            {
                int id = Convert.ToInt32(scid);
                var cookie = Request.Cookies["wished"];
                ProductRepository.wish = cookie;
                pr.DeleteWishedList(id) ;
                Response.Cookies.Append("wished", ProductRepository.wish);
                List<Products> wlist = pr.WishedList();
                ViewBag.wished = wlist;

            }
            else
            {
                var cookie = Request.Cookies["wished"];
                List<Products> wlist;
                if (cookie==null)
                {
                    Response.Cookies.Append("wished", ProductRepository.wish);
                    ProductRepository.wish = "";
                    wlist = pr.WishedList();
                    ViewBag.wished = wlist;

                }
                else
                {
                    ProductRepository.wish = Request.Cookies["wished"];
                    wlist = pr.WishedList();
                    ViewBag.wished = wlist;
                }

            }

            return View();
        }

        public IActionResult GetCart(int id)
        {
            ProductsOrdersRepository pr = new ProductsOrdersRepository(configuration);
            ProductsOrders pod = new ProductsOrders();
            ProductRepository prdRep = new ProductRepository(configuration);
            prdRep.IncreaseFeatured(id);


            CookieOptions ckOpt = new CookieOptions();
            ckOpt.Expires = DateTime.Now.AddDays(5);
           Response.Cookies.Append("mycart",id.ToString(),ckOpt);

            var cookie = Request.Cookies["mycart"];
          
            if (String.IsNullOrEmpty(cookie))
            {
                Response.Cookies.Append("mycart", id.ToString()+"="+ProductsOrdersRepository.quantity,ckOpt);
                ProductsOrdersRepository.cart = id.ToString() + "=" + ProductsOrdersRepository.quantity;

            }
            else
            {
                //ProductRepository.wish = Request.Cookies["wished"];
                ProductsOrdersRepository.cart = cookie;
                //first time added
                if (pr.AddCartList(id) == false)
                {
                    
                    ProductsOrdersRepository.cart = cookie + '&' + id.ToString() + "=" + ProductsOrdersRepository.quantity;
                    Response.Cookies.Append("mycart", ProductsOrdersRepository.cart,ckOpt);                   

                }
                else
                {
                    pr.DeleteCartList(id);
                    cookie = ProductsOrdersRepository.cart;
                    if (!String.IsNullOrEmpty(cookie))
                    {

                        ProductsOrdersRepository.cart = cookie + '&' + id.ToString() + "=" + ProductsOrdersRepository.quantity;
                    }
                    else
                    {
                        ProductsOrdersRepository.cart = id.ToString() + "=" + ProductsOrdersRepository.quantity;
                    }
                    Response.Cookies.Append("mycart", ProductsOrdersRepository.cart,ckOpt);
                }
            }

          
            return RedirectToAction("Index", "Home");
        }

        [Route("Cart")]
        public IActionResult Cart()
        {
           
            if (HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }
            CookieOptions ckOpt = new CookieOptions();
            ckOpt.Expires = DateTime.Now.AddDays(5);
            ProductsOrdersRepository pr = new ProductsOrdersRepository(configuration);            
            string scid = Request.Query["scid"];
            if (scid != null)
            {
                int id = Convert.ToInt32(scid);
                var cookie = Request.Cookies["mycart"];
                ProductsOrdersRepository.cart = cookie;
                pr.DeleteCartList(id);
                Response.Cookies.Append("mycart", ProductsOrdersRepository.cart,ckOpt);
                List<ProductsOrders> clist = pr.CartList();
                ViewBag.cart = clist;
                ViewBag.cartdetails = clist;


            }
           
            else
            {
                var cookie = Request.Cookies["mycart"];
                List<ProductsOrders> clist;
                if (cookie == null)
                {
                    Response.Cookies.Append("mycart", ProductsOrdersRepository.cart, ckOpt);
                    ProductsOrdersRepository.cart = "";
                    clist = pr.CartList();
                    ViewBag.cart = clist;
                    ViewBag.cartdetails = clist;

                }
                else
                {
                    ProductsOrdersRepository.cart = Request.Cookies["mycart"];
                    clist = pr.CartList();
                    ViewBag.cart = clist;
                    ViewBag.cartdetails = clist;
                }

            }

            return View();

        }

        public static string OrdGUID;

       
        public async Task<IActionResult> SaveOrder()
        {
            var cookie = Request.Cookies["mycart"];
            ProductsOrdersRepository por = new ProductsOrdersRepository(configuration);
            int cid = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            if (!String.IsNullOrEmpty(cookie))
            {
                ProductsOrdersRepository.cart = cookie;
                OrdGUID=await por.AddOrder(cid);
                ViewBag.ordGuid = OrdGUID;
                ProductsOrdersRepository.cart = "";
                //ViewBag.cart = "";
                Response.Cookies.Append("mycart", ProductsOrdersRepository.cart);

                return RedirectToAction("ComingSoon", "User");
            }
           
                return RedirectToAction("Index", "Home");
            

          
        }

        [Route("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            int cid = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            
            CustomerRepository cr = new CustomerRepository(configuration);
            ViewCheckoutModel vc = new ViewCheckoutModel();
            Customers cs = await cr.GetByIdAsync(cid);
            vc.customers = cs;
            int? adID = cs.AddressID;
            AddressRepository ar = new AddressRepository(configuration);
            vc.address =await ar.GetById(adID);
            ProductsOrdersRepository pr = new ProductsOrdersRepository(configuration);
            List<ProductsOrders> list = pr.CartList();
            ViewBag.cart = list;
            return View(vc);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int id)
        {
                     
            return RedirectToAction("ComingSoon", "User");

        }

            [HttpPost]
        public async Task<IActionResult> UpdateAddress(Address add,Customers cus)
        {
            
            AddressRepository ar = new AddressRepository(configuration);
            Address adr =await ar.GetById(add.AddressID);
            add.AddressName = adr.AddressName;
            add.Active = true;
            add.IsBill = true;
            add.IsShip = true;

            await ar.UpdataAsync(add);

            CustomerRepository cr = new CustomerRepository(configuration);
            Customers cs = await cr.GetByIdAsync(cus.CustomerID);
            cs.TCKN = cus.TCKN;
            await cr.UpdateTCKNAsync(cs);

            return RedirectToAction("Checkout", "User");
        }

        [Route("ComingSoon")]
        public  IActionResult ComingSoon()
        {
            ViewBag.ordGuid= OrdGUID;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Route("MyOrders")]
        public async Task<IActionResult> MyOrders()
        {
            if (HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }
            int id = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            ProductsOrdersRepository pr = new ProductsOrdersRepository(configuration);
            List<ProductsOrders> list=await pr.GetByCustomerIdAsync(id);
            ViewBag.list = list;
            return View(list);
        }

        public IActionResult Register()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customers cus, Address add)
        {
            CustomerRepository cr = new CustomerRepository(configuration);
            AddressRepository ar = new AddressRepository(configuration);

#pragma warning disable CS8604 // Possible null reference argument.
            bool result = cr.CheckEmail(cus.EMail);
            if (result == true)
#pragma warning restore CS8604 // Possible null reference argument.
            {
                add.AddressName = cus.FirstName + " " + cus.LastName;
                add.Address2 = "";
                add.Fax = add.Phone;
                add.PostalCode = "";
                add.Email = cus.EMail;
                add.IsBill = add.IsBill = add.Active = true;
                int addID = await ar.AddAddress(add);



                cus.AddressID = addID;
                cus.DateEntered = System.DateTime.Now;
                cus.ConfirmPassword = cus.Password;
                cus.Class = "basic";
                cus.Active = true;
                cus.IsAdmin = false;
                cus.IsAdmin = false;
                await cr.AddAsync(cus);
                TempData["register"] = "basarili";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["register"] = "basarisiz";
            }
            
           
            
            return RedirectToAction("Login","User");

        }

    }
}
