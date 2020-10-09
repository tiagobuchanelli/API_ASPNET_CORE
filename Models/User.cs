using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("users")] //renomear a tabela no banco
    public class User
    {
        [Key]
        [Column("id")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Id { get; set; }

        //UID gerado pelo Firebase
        [Column("uid")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Uid { get; set; }


        [Column("name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string Name { get; set; }

        [Column("email")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [DataType("varchar")]
        public string Email { get; set; }

        //0 Cancelado - 1 Ativo
        [Column("status")]
        [DataType("int")]
        public int Status { get; set; }

        //Default, Company
        [Column("role")]
        public string Role { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        public DateTime DateUpdate { get; set; }

    }
}