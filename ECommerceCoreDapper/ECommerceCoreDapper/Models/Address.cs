using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Adres Adı")]// set a basic name for a new address
        public string? AddressName { get; set; }

        [Required]
        [StringLength(60)]
        [Column(TypeName = "nvarchar(60)")]
        public string? Address1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? Address2 { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Şehir")]
        public string? City { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(60)")]
        [Display(Name = "İlçe")]
        public string? State { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Posta Kodu")]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(60)")]
        [Display(Name = "Ülke")]
        public string? Country { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        [Display(Name = "Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        [Display(Name = "Fax Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string? Fax { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        [Display(Name = "Email Adres")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public Boolean IsBill { get; set; }
        public Boolean IsShip { get; set; }
        public Boolean Active { get; set; }




    }
}
