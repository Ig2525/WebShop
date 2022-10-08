using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Data.Entities
{
    [Table("tblProductImage")]
    public class ProductImageEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
