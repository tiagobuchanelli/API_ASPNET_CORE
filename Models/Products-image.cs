using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("products_image")]
    public class ProductsImage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int ProductId { get; set; }

        [Column("image_url")]
        [Required(ErrorMessage = "Imagem - Este campo é obrigatorio")]
        [DataType("varchar")]
        public string ImageUrl { get; set; }



    }
}