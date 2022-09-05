using ECommerceCoreDapper.Models;
using ECommerceCoreDapper.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ECommerceCoreDapper.MVVM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace ECommerceCoreDapper.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;


        //}

        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "Sytem.Data.SqlClient";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }




        public async Task<IActionResult> Index()
        {
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            ViewProductsModel vp = new ViewProductsModel();
            pjr.pageSizeHome = 4;
            if (HttpContext.Session.GetString("EMail") == null)
            {
                ViewBag.login = "My Account";
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }

           
            vp.BestSeller =await pjr.GetCategorizedProducts("BestSeller", "HomePage", 0);
            vp.Featured = await pjr.GetCategorizedProducts("Featured", "HomePage", 0);
            vp.Sale = await pjr.GetCategorizedProducts("Sale", "HomePage", 0);
            vp.TopRate = await pjr.GetCategorizedProducts("TopRate", "HomePage", 0);

            return View(vp);
        }

        [Route("Shop")]
        public async Task<IActionResult> Shop()
        {
            if (HttpContext.Session.GetString("EMail") == null)
            {
                ViewBag.login = "My Account";
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            ViewProductsModel vp = new ViewProductsModel();
            pjr.pageSizeSub = 4;

            vp.AllProduct =await pjr.GetCategorizedProducts("AllProducts", "SubPage", 0);
            vp.Women = await pjr.GetCategorizedProducts("Women", "SubPage", 0);
            vp.Men = await pjr.GetCategorizedProducts("Men", "SubPage", 0);
            vp.Bag = await pjr.GetCategorizedProducts("Bag", "SubPage", 0);
            vp.Shoes = await pjr.GetCategorizedProducts("Shoes", "SubPage", 0);
            vp.Watches = await pjr.GetCategorizedProducts("Watches", "SubPage", 0); 

            return View(vp);
        }

        public async Task<IActionResult> _partialShop(string model, string pageNumber)
        {
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            ViewProductsModel vp = new ViewProductsModel();
            pjr.pageSizeSub = 4;           
            int pageNo = Convert.ToInt32(pageNumber);
            

            if (model == "AllProduct") { vp.AllProduct = await pjr.GetCategorizedProducts("AllProducts", "SubPage", pageNo); TempData["model"] = vp.AllProduct; }
            else if (model == "Women") { vp.Women = await pjr.GetCategorizedProducts("Women", "SubPage", pageNo); TempData["model"] = vp.Women; }
            else if (model == "Men") { vp.Men = await pjr.GetCategorizedProducts("Men", "SubPage", pageNo); TempData["model"] = vp.Men; }
            else if (model == "Bag") { vp.Bag = await pjr.GetCategorizedProducts("Bag", "SubPage", pageNo); TempData["model"] = vp.Bag; }
            else if (model == "Shoes") { vp.Shoes = await pjr.GetCategorizedProducts("Shoes", "SubPage", pageNo); TempData["model"] = vp.Shoes; }
            else if (model == "Watches") { vp.Watches = await pjr.GetCategorizedProducts("Watches", "SubPage", pageNo); TempData["model"] = vp.Watches; }
            else if (model == "Features") { vp.Featured = await pjr.GetCategorizedProducts("Features", "SubPage", pageNo); TempData["model"] = vp.Featured; }


            return View(vp);
        }

        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("EMail") == null)
            {
                ViewBag.login = "My Account";
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }

            
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            ViewProductsModel vp = new ViewProductsModel();
            vp.details=await pjr.GetByIdAsync(id);
            vp.Related = await pjr.GetRelatedProducts(id);

            List<SelectListItem> szlist = (from x in await pjr.GetAllSizes()
                                           select new SelectListItem
                                           {
                                               Text = x.Size,
                                               Value = x.SizeID.ToString(),
                                           }).ToList();

            ViewData["szlist"] = szlist;

            List<SelectListItem> cllist = (from x in await pjr.GetAllColors()
                                           select new SelectListItem
                                           {
                                               Text = x.Color,
                                               Value = x.ColorID.ToString(),
                                           }).ToList();

            ViewData["cllist"] = cllist;

            return View(vp);
        }


        public JsonResult GetSearch(string id)
          {
            ProductRepository pr = new ProductRepository(configuration);
            id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR", false));
            List<SearchProduct> slist = pr.GetSearchPrd(id);
            ViewBag.slist = slist;           
            return Json(slist);
        }


        [Route("Features")]
        public async Task<IActionResult> Features()
        {
            if (HttpContext.Session.GetString("EMail") == null)
            {
                ViewBag.login = "My Account";
            }
            else
            {
                ViewBag.login = HttpContext.Session.GetString("EMail");
            }
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            ViewProductsModel vp = new ViewProductsModel();
            pjr.pageSizeSub = 4;

            vp.Featured = await pjr.GetCategorizedProducts("Features", "SubPage", 0);

            return View(vp);
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}