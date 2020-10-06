using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("company_id")] //Não é obrigatório pois a API é quem identifica e grava o ID pelo token (uid do firebase)
        // [Required(ErrorMessage = "Este campo é obrigatorio")]
        // [DataType("varchar")]
        public string CpnyUid { get; set; }

        [Column("title")]
        [Required(ErrorMessage = "Título - Este campo é obrigatorio")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter entre 3 e 150 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 150 caracteres")]
        [DataType("varchar")]
        public string Title { get; set; }


        [Column("description")]
        [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
        [DataType("varchar")]
        public string Description { get; set; }


        [Column("price")]
        [Required(ErrorMessage = "Price - Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O preço deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Price { get; set; }


        [Column("cost")]
        [DataType("decimal")]
        public decimal Cost { get; set; }


        [Column("id_category")]
        [Required(ErrorMessage = "Price - Este campo é obrigatorio")]
        [DataType("int")]
        public int CategoryId { get; set; }

        [Column("status")] //0 Cancelado - 1 Ativo
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("type_product")] //1 -Produto, 2 -Serviço
        [Required(ErrorMessage = "Tipo de Produto - Este campo é obrigatorio")]
        [Range(1, 2, ErrorMessage = "Tipo inválido - Os valores deve ser 1 ou 2")]
        [DataType("int")]
        public int TypeProduct { get; set; }


        [Column("stock")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "A quanitade deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Stock { get; set; }

        [Column("thumb_image")]
        [Required(ErrorMessage = "Imagem - Este campo é obrigatorio")]
        [DataType("varchar")]
        public string ThumbImage { get; set; }

        [Column("date_created")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        //Obejto completo da categoriacom Id, Desc
        //Não será salvo no banco mas permitira fazer INCLUDE para por exemplo inserir a categoria no produto. (Entity trabalha como um JOIN)
        public Category Category { get; set; }
        public Company Cpny { get; set; }
    }
}