using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Payments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public Boolean Allowed { get; set; }
    }
}
