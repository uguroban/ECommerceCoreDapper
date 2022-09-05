using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Shippers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipperID { get; set; }

        [Required]
        [StringLength(75)]
        [Column(TypeName = "nvarchar(75)")]
        public string? CompanyName { get; set; }
        public int AddressID { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
