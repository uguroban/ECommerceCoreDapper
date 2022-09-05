using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Brands
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }

        [Required]
        [StringLength(75)]
        [Column(TypeName = "nvarchar(75)")]
        public string? Logo { get; set; }
    }
}
