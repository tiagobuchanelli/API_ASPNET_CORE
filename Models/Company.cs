using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("company")] //renomear a tabela no banco
    public class Company
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }

        [Column("user_id")] //Não é obrigatório pois a API é quem identifica e grava o ID pelo token (uid do firebase)
        //[Required(ErrorMessage = "Este campo é obrigatorio")]
        // [DataType("varchar")]
        public string CpnyUid { get; set; }


        [Column("name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [DataType("varchar")]
        public string Name { get; set; }


        [Column("last_name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string LastName { get; set; }


        [Column("email")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [DataType("varchar")]
        public string Email { get; set; }


        [Column("address")]
        [MaxLength(300, ErrorMessage = "Este campo deve conter no máximo 300 caracteres")]
        [DataType("varchar")]
        public string Address { get; set; }

        [Column("address_nr")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [DataType("varchar")]
        public string AddressNr { get; set; }

        [Column("neighborhood")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Neighborhood { get; set; }


        [Column("zip_code")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter no máximo 10 caracteres")]
        [DataType("varchar")]
        public string ZipCode { get; set; }

        [Column("phone")]
        [MaxLength(15, ErrorMessage = "Este campo deve conter no máximo 15 caracteres")]
        [DataType("varchar")]
        public string Phone { get; set; }

        [Column("status")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("type_entity")] //1 CLIENTE, 2 FORNECEDOR, 3 OUTRS
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [Range(1, 3, ErrorMessage = "Tipo inválido - Os valores devem ser 1,2 ou 3")]
        [DataType("int")]
        public int EntityType { get; set; }

        [Column("date_created")]
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }

        public User Cpny { get; set; }


    }
}