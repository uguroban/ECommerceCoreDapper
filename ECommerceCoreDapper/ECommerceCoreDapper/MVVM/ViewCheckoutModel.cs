using ECommerceCoreDapper.Models;
namespace ECommerceCoreDapper.MVVM
    {
    public class ViewCheckoutModel
    {
        public Customers customers { get; set; }
        public ProductsOrders proOrder { get; set; }
        public Address address { get; set; }


    }
}
