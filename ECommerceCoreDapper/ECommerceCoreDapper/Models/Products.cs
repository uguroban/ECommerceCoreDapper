using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public int BrandID { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        [StringLength(60, ErrorMessage = "Daha fazla karakter girişi yapamazsınız!")]
        [Display(Name = "Ürün Adı")]
        [Column(TypeName = "nvarchar(60)")]
        public string? ProductName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Ürün Açıklaması")]
        public string? ProductDescription { get; set; }

        public int SupplierID { get; set; }
        public int CategoryID { get; set; }

        public bool WomenorMen { get; set; }
        public int QuantityPerUnit { get; set; }

        //encapsulation used for UnitPrice
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        [Display(Name = "Ürün Fiyatı")]
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

        //encapsulation used for UnitPrice
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        [Display(Name = "Ürün Perakende Fiyatı")]
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


        /*UnitsInStock - UnitsonOrder = X
        If X is > ReorderLevel then "Item is in Stock"
        If X is <= ReorderLevel then "Item is Out of Stock" */
        public Boolean ProductAvailable { get; set; }
        public Boolean DiscountAvailable { get; set; }
        public int TopSell { get; set; } = 0; //en çok satanlar
        public int Featured { get; set; } = 0; //öne çıkanlar
        public string PhotoPath { get; set; }
        public int? Ranking { get; set; } //belirli ürünleri görüntülemek veya bir sıralamada göstermek için
        public string? Note { get; set; }








    }
}
