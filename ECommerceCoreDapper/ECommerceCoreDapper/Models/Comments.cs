using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    public class Comments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public int? CustomerID { get; set; }
        public int? ProductID { get; set; }
        public string Comment { get; set; }
        public int? ProductPhotoID { get; set; }
    }
}
