using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Sizes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Size { get; set; }
    }
}
