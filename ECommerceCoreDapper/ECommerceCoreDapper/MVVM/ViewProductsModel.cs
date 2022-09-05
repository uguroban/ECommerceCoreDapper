using ECommerceCoreDapper.Models;

namespace ECommerceCoreDapper.MVVM
{
    public class ViewProductsModel
    {
        public List<ProductAllJoin> AllProduct { get; set; }
        public List<ProductAllJoin> BestSeller { get; set; }
        public List<ProductAllJoin> Featured { get; set; }
        public List<ProductAllJoin> Sale { get; set; }
        public List<ProductAllJoin> TopRate { get; set; }
        public List<ProductAllJoin> Women { get; set; }
        public List<ProductAllJoin> Men { get; set; }
        public List<ProductAllJoin> Bag { get; set; }
        public List<ProductAllJoin> Shoes { get; set; }
        public List<ProductAllJoin> Watches { get; set; }
        public List<ProductAllJoin> WishList { get; set; }
        public List<ProductAllJoin> Related { get; set; }
        public ProductAllJoin details { get; set; }

    }
}
