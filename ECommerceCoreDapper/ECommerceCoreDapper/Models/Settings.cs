using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Settings
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string CompanyName { get; set; }
        public int? AddressID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string Phone { get; set; }
        public int? PageSizeHome { get; set; }
        public int PageSizeSub { get; set; }
        public string? XMaps { get; set; }
        public string? YMaps { get; set; }
        public string Logo { get; set; }
    }
}
