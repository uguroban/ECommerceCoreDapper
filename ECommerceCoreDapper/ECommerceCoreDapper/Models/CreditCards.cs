using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class CreditCards
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditCardID { get; set; }
        public enum CreditCardType
        {
            Visa = 0,
            MasterCard = 1,
            AmericanExpress = 2,
            Discover = 3,
            Amex = 4,
            BCGlobal = 5,
            CarteBlanch = 6,
            DinersClub = 7,
            InstaPaymentCard = 8,
            JCBCard = 9,
            KoreanLocal = 10,
            LaserCard = 11,
            Maestro = 12,
            Solo = 13,
            SwitchCard = 14,
            UnionPay = 15,
            NotFormatted = 16,
            Unknown = 17
        }


        [Required]
        public string? CreditCardNumber { get; set; } //will check with method
        public DateTime CExpYear { get; set; } = DateTime.Parse(DateTime.MinValue.Year.ToString());
        public DateTime CExpMonth { get; set; } = DateTime.Parse(DateTime.MinValue.Month.ToString());
        public int CVV { get; set; } //will check with method
        public DateTime TimeStamp { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? TransactStatus { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ErrLoc { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? ErrMsg { get; set; }
        public Boolean Active { get; set; }


        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? CardName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string CardFName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string CardLName { get; set; }




    }
}
