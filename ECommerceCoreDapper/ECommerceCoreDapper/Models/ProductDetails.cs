using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class ProductDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDetailID { get; set; }
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public int ColorID { get; set; }
        public int SizeID { get; set; }
    }
}
