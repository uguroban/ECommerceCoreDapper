using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Colors
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Color { get; set; }
    }
}
