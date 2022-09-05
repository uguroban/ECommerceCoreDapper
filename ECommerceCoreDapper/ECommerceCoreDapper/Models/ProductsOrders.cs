namespace ECommerceCoreDapper.Models
{
	public class ProductsOrders
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
  
        public Boolean ProductAvailable { get; set; }
      
        public string PhotoPath { get; set; }

        public int OrderID { get; set; }

        public string OrderGroupGUID { get; set; }
        public string OrderGroupID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

    }
}
