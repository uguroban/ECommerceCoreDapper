using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Customers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [MinLength(11), MaxLength(11)]
        private string? _TCKN { get; set; } //web api ile geçerliliği kontrol edilecek
        public string? TCKN
        {
            get { return _TCKN; }
            set { _TCKN = value; }
        }


        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EMail Adresiniz")]
        public string? EMail { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifreler eşleşmedi!")]
        public string? ConfirmPassword { get; set; }
        public Boolean IsAdmin { get; set; }
        public Boolean IsSuperAdmin { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string? Class { get; set; }
        public int? AddressID { get; set; }
        public int? CreditCardID { get; set; }
        public DateTime DateEntered { get; set; }
        public Boolean Active { get; set; }






    }
}
