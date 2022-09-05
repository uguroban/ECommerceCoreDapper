using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
	public class Orders
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public string OrderGroupGUID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

    }
}
