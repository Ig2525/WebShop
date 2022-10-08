using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Data.Entities
{
    [Table("tblCategoris")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(255)]
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
