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

        [Column("uid")] //id do firebase
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


        [Column("username")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 8 e 20 caracteres")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 8 e 20 caracteres")]
        [DataType("varchar")]
        public string Username { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 6 e 20 caracteres")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 6 e 20 caracteres")]
        [DataType("varchar")]
        public string Password { get; set; }


        [Column("status")] //0 Cancelado - 1 Ativo
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("role")] //Default, Company
        public string Role { get; set; }


        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        public DateTime DateUpdate { get; set; }

    }
}