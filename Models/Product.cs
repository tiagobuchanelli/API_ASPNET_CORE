using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("PRODUCTS")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Column("TITLE")]
        [Required(ErrorMessage = "Título - Este campo é obrigatorio")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter entre 3 e 150 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 150 caracteres")]
        [DataType("varchar")]
        public string Title { get; set; }


        [Column("DESCRIPTION")]
        [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
        [DataType("varchar")]
        public string Description { get; set; }


        [Column("PRICE")]
        [Required(ErrorMessage = "Price - Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O preço deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Price { get; set; }


        [Column("COST")]
        [DataType("decimal")]
        public decimal Cost { get; set; }


        [Column("ID_CAT")]
        [DataType("int")]
        public int CategoryId { get; set; }

        [Column("STATUS")] //0 Cancelado - 1 Ativo
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        //Obejto completo da categoriacom Id, Desc
        //Não será salvo no banco mas permitira fazer INCLUDE para por exemplo inserir a categoria no produto. (Entity trabalha como um JOIN)
        public Category Category { get; set; }
    }
}