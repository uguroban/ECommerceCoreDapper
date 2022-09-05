namespace ECommerceCoreDapper.Models
{
    public class ProductAllJoin
    {
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }

        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public bool WomenorMen { get; set; }
        public int QuantityPerUnit { get; set; }
        private decimal _UnitPrice { get; set; }

        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set
            {
                //ternary if used
                _UnitPrice = value > 0 ? value : Math.Abs(value);
            }
        }
        private decimal _RetailPrice { get; set; }

        public decimal RetailPrice
        {
            get { return _RetailPrice; }
            set
            {
                //ternary if used
                _RetailPrice = value > 0 ? value : Math.Abs(value);
            }
        }

        public int SizeID { get; set; }
        public int ColorID { get; set; }
        public decimal Discount { get; set; }
        public int? UnitWeight { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }

        /*UnitsInStock - UnitsonOrder = X(jquery ile hesapla)
        If X is > ReorderLevel then "Item is in Stock"
        If X is <= ReorderLevel then "Item is Out of Stock" */
        public int ReorderLevel { get; set; }
        public Boolean ProductAvailable { get; set; }
        public Boolean DiscountAvailable { get; set; }
        public int TopSell { get; set; } = 0; //en çok satanlar
        public int Featured { get; set; } = 0; //öne çıkanlar
        public string PhotoPath { get; set; }
        public int? Ranking { get; set; } //belirli ürünleri görüntülemek veya bir sıralamada göstermek için
        public string? Note { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public string? Logo { get; set; }
        public int ParentID { get; set; }
        public string? CategoryName { get; set; }
        public Boolean Active { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string? CompanyName { get; set; }
        public int sRank { get; set; }
        public int AddressID { get; set; }
        public string? WebSite { get; set; }

    }
}
