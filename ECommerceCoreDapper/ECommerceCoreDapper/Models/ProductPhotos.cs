using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCoreDapper.Models
{
    [Keyless]
    public class ProductPhotos
    {
        public int ProductID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? PhotoPath { get; set; }
    }
}
