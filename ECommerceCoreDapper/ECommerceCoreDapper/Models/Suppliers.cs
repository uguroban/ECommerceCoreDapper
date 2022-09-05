using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Suppliers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Şirket Adı")]
        public string? CompanyName { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Yetkili Adı")]
        public string? ContactFName { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Yetkili Soyadı")]
        public string? ContactLName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Yetkili Ünvanı")]
        public string? ContactTitle { get; set; }
        public int? AddressID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Şirket WebSitesi")]
        public string? WebSite { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "İndirim Tipi")]
        public string? DiscountType { get; set; }
        public int DiscountRate { get; set; } //can set standart rate discount and then apply for Supllier's all products
        public Boolean DiscountAvailable { get; set; }

        [StringLength(75)]
        [Column(TypeName = "nvarchar(75)")]
        [Display(Name = "Logo")]
        public string? Logo { get; set; }
        public int? Ranking { get; set; }
        public string? Note { get; set; }


    }
}
