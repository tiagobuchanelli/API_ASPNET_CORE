using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("USERS")] //renomear a tabela no banco
    public class User
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("NAME")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string Name { get; set; }

        [Column("EMAIL")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [DataType("varchar")]
        public string Email { get; set; }


        [Column("USERNAME")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 8 e 20 caracteres")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 8 e 20 caracteres")]
        [DataType("varchar")]
        public string Username { get; set; }

        [Column("PASSWORD")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 6 e 20 caracteres")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 6 e 20 caracteres")]
        [DataType("varchar")]
        public string Password { get; set; }


        [Column("STATUS")] //0 Cancelado - 1 Ativo
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("DATE_CREATED")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        public DateTime DateUpdate { get; set; }

    }
}