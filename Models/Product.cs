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

        [Column("company_id")]
        // [DataType("varchar")]
        public int CpnyId { get; set; }

        //UID da Empresa. Utilizado para identificar o proprietário da categoria, por meio do uid_user que está presente no token. Mas a chave estrangeira é identificador a cima (CpnyId).
        [Column("company_uid")]
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

        //0 Cancelado - 1 Ativo
        [Column("status")]
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        //1 -Produto, 2 -Serviço
        [Column("type_product")]
        [Required(ErrorMessage = "Tipo de Produto - Este campo é obrigatorio")]
        [Range(1, 2, ErrorMessage = "Tipo inválido - Os valores deve ser 1 ou 2")]
        [DataType("int")]
        public int TypeProduct { get; set; }


        [Column("stock")]
        [DataType("decimal")]
        public decimal Stock { get; set; }

        [Column("thumb_image")]
        [Required(ErrorMessage = "Imagem - Este campo é obrigatorio")]
        [DataType("varchar")]
        public string ThumbImage { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        public DateTime DateUpdate { get; set; }

        public Category Category { get; set; }
        public Company Cpny { get; set; }
    }
}