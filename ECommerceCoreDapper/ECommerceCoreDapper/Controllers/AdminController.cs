using ECommerceCoreDapper.Models;
using ECommerceCoreDapper.Models.Repository;
using ECommerceCoreDapper.MVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceCoreDapper.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("ECommerceDBCon");
            ProviderName = "Sytem.Data.SqlClient";
        }
        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }


        public IActionResult ManagementPanel()
        {
            return View();
        }
        public IActionResult KategoriKaydet()
        {
            CategoryRepository cr = new CategoryRepository(configuration);
            List<SelectListItem> clist = (from x in cr.GetAllCategories()
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString()
                                          }).ToList();

            ViewData["clist"] = clist;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KategoriKaydetAsync(Categories cat)
        {
            CategoryRepository cr = new CategoryRepository(configuration);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            bool sonuc = cr.CheckCategoryName(cat.CategoryName.ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (sonuc == false)
            {
                if (cat.CategoryID == 0)
                {
                    cat.ParentID = 0;
                }
                else
                {
                    cat.ParentID = cat.CategoryID;
                }
                await cr.AddAsync(cat);
                ViewBag.basarılı = "Yeni Kategori " + cat.CategoryName + " başarıyla eklendi";
            }
            else
            {
                ViewBag.hata = "Aynı isimli birden fazla kategori olamaz!";
            }
            return RedirectToAction("KategoriKaydet");
        }

        public async Task<IActionResult> KategoriGuncelleSil(int id)
        {
            CategoryRepository cr = new CategoryRepository(configuration);
            List<SelectListItem> clist = (from x in cr.GetAllCategories()
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString(),
                                          }).ToList();

            ViewData["clist"] = clist;

            Categories cat = await cr.GetByIdAsync(id);
            return View("KategoriGuncelleSil", cat);
        }

        [HttpPost]
        public async Task<IActionResult> KategoriGuncelleSil(Categories cat)
        {
            CategoryRepository cr = new CategoryRepository(configuration);


            //cat.ParentID =0 ? cat.ParentID : cat.CategoryID;
            if (cat.ParentID == 0)
            {
                Categories c = await cr.GetByIdAsync(cat.CategoryID);
                cat.ParentID = c.ParentID;
            }

            await cr.UpdataAsync(cat);
            return RedirectToAction("KategoriGuncelleSil");
        }

        public IActionResult MarkaKaydet()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkaKaydet(Brands b, IFormFile fileOne)
        {
            b.Logo = fileOne.FileName.ToString();
            BrandRepository cr = new BrandRepository(configuration);
#pragma warning disable CS8604 // Possible null reference argument.
            bool sonuc = cr.CheckBrandName(b.BrandName);
#pragma warning restore CS8604 // Possible null reference argument.
            if (sonuc == false)
            {
                ViewBag.hata = "";
                await cr.AddAsync(b);
            }
            else
            {
                ViewBag.hata = "Aynı isimli birden fazla marka eklenemez!";
            }

            return RedirectToAction("MarkaKaydet");
        }

        public async Task<IActionResult> MarkaGuncelleSil(int id)
        {
            BrandRepository br = new BrandRepository(configuration);
            List<SelectListItem> blist = (from x in await br.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.BrandName,
                                              Value = x.BrandID.ToString(),
                                          }).ToList();

            ViewData["blist"] = blist;



            Brands b = await br.GetByIdAsync(id);
            return View("MarkaGuncelleSil", b);
        }

        [HttpPost]

        public async Task<IActionResult> MarkaGuncelleSil(Brands b, IFormFile fileOne)
        {
            BrandRepository br = new BrandRepository(configuration);
            b.Logo = fileOne.FileName.ToString();
            await br.UpdataAsync(b);
            return RedirectToAction("MarkaGuncelleSil");
        }


        public IActionResult TedarikciKaydet()
        {
            List<SelectListItem> dlist = new()
            {
                new SelectListItem{Value="1",Text="Sepet"},
                new SelectListItem{Value="2",Text="ÖzelGün"},
                new SelectListItem{Value="3",Text="TümÜrünler"},
                new SelectListItem{Value="4",Text="SeciliÜrünler"},
                new SelectListItem{Value="5",Text="YeniÜrün"},
                new SelectListItem{Value="6",Text="Fırsat"},
                new SelectListItem{Value="7",Text="StokAzaltma"}
            };
            ViewData["dlist"] = dlist;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> TedarikciKaydet(Suppliers sp, IFormFile fileOne, Address add)
        {
            AddressRepository ar = new AddressRepository(configuration);
            int id = await ar.AddAddress(add);
            sp.Logo = fileOne.FileName.ToString();
            sp.AddressID = id;
            SupplierRepository sr = new SupplierRepository(configuration);

            await sr.AddAsync(sp);


            return RedirectToAction("TedarikciKaydet");
        }

        public async Task<IActionResult> TedarikciGuncelleSil(int id)
        {

            SupplierRepository sr = new SupplierRepository(configuration);
            AddressRepository ar = new AddressRepository(configuration);
            ViewSupplierModel sp = new ViewSupplierModel();

            List<SelectListItem> slist = (from x in await sr.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.CompanyName,
                                              Value = x.SupplierID.ToString(),
                                          }).ToList();

            ViewData["slist"] = slist;

            List<SelectListItem> dlist = new()
           {
                new SelectListItem{Value="1",Text="Sepet"},
                new SelectListItem{Value="2",Text="ÖzelGün"},
                new SelectListItem{Value="3",Text="TümÜrünler"},
                new SelectListItem{Value="4",Text="SeciliÜrünler"},
                new SelectListItem{Value="5",Text="YeniÜrün"},
                new SelectListItem{Value="6",Text="Fırsat"},
                new SelectListItem{Value="7",Text="StokAzaltma"}
           };

            ViewData["dlist"] = dlist;
            if (id != 0)
            {
                sp.supplier = await sr.GetByIdAsync(id);
                int? aid = sp.supplier.AddressID;
                sp.address = await ar.GetById(aid);

                return View(sp);
            }


            return View();

        }


        [HttpPost]
        public async Task<IActionResult> TedarikciGuncelleSil(ViewSupplierModel vs, IFormFile fileOne)
        {
            SupplierRepository sr = new SupplierRepository(configuration);
            AddressRepository ar = new AddressRepository(configuration);

            vs.supplier.Logo = fileOne.FileName.ToString();
            await sr.UpdataAsync(vs.supplier);
            await ar.UpdataAsync(vs.address);


            return RedirectToAction("TedarikciGuncelleSil");



        }

        public async Task<IActionResult> UrunKaydet(int id)
        {
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            BrandRepository br = new BrandRepository(configuration);
            SupplierRepository sr = new SupplierRepository(configuration);
            CategoryRepository cr = new CategoryRepository(configuration);
            List<SelectListItem> blist = (from x in await br.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.BrandName,
                                              Value = x.BrandID.ToString(),
                                          }).ToList();

            ViewData["blist"] = blist;
            List<SelectListItem> slist = (from x in await sr.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.CompanyName,
                                              Value = x.SupplierID.ToString(),
                                          }).ToList();

            ViewData["slist"] = slist;

            List<SelectListItem> clist = (from x in await cr.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString(),
                                          }).ToList();

            ViewData["clist"] = clist;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UrunKaydet(Products prd, IFormFile fileOne)
        {
            ProductRepository pr = new ProductRepository(configuration);
            prd.PhotoPath = fileOne.FileName.ToString();
            int pID = await pr.AddAsync(prd);


            ProductPhotos ph = new ProductPhotos();
            ph.ProductID = pID;
            ph.PhotoPath = fileOne.FileName.ToString();

            await pr.AddPhotos(ph);

            return RedirectToAction("UrunKaydet");
        }

        public async Task<IActionResult> UrunGuncelleSil(int id)
        {
            ProductAllJoinRepository pjr = new ProductAllJoinRepository(configuration);
            BrandRepository br = new BrandRepository(configuration);
            SupplierRepository sr = new SupplierRepository(configuration);
            CategoryRepository cr = new CategoryRepository(configuration);
            List<SelectListItem> blist = (from x in await br.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.BrandName,
                                              Value = x.BrandID.ToString(),
                                          }).ToList();

            ViewData["blist"] = blist;
            List<SelectListItem> slist = (from x in await sr.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.CompanyName,
                                              Value = x.SupplierID.ToString(),
                                          }).ToList();

            ViewData["slist"] = slist;

            List<SelectListItem> clist = (from x in await cr.GetAllAsync()
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString(),
                                          }).ToList();

            ViewData["clist"] = clist;

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
            List<SelectListItem> plist = (from x in await pjr.GetAllProducts()
                                          select new SelectListItem
                                          {
                                              Text = x.ProductName,
                                              Value = x.ProductID.ToString(),
                                          }).ToList();

            ViewData["plist"] = plist;

            ProductRepository pr = new ProductRepository(configuration);
            if (id != 0)
            {
                Products p = new Products();
                foreach (var item in plist)
                {

                    if (Convert.ToInt32(item.Value) == id)
                    {
                        p = await pr.GetByIdAsync(id);
                        return View(p);
                    }

                }

            }


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UrunGuncelleSil(Products prd, IFormFile fileOne)
        {
            ProductRepository pr = new ProductRepository(configuration);
            prd.PhotoPath = fileOne.FileName.ToString();
            await pr.UpdataAsync(prd);
            return RedirectToAction("UrunGuncelleSil");
        }
    }
}
