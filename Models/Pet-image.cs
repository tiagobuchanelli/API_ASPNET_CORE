using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("pets-image")]
    public class PetImage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("pet_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int PetId { get; set; }

        [Column("image_url")]
        [Required(ErrorMessage = "Imagem - Este campo é obrigatorio")]
        [DataType("varchar")]
        public string ImageUrl { get; set; }



    }
}