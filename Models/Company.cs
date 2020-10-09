using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("company")]
    public class Company
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        //UID do Usuário. Utilizado para identificar o proprietário da Empresa, por meio do uid_user que está presente no token. Mas a chave estrangeira é identificador a cima (UserId).
        [Column("company_uid")]
        // [DataType("varchar")]
        public string Uid { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [DataType("varchar")]
        public string Name { get; set; }


        [Column("fantasy_name")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string FantasyName { get; set; }


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

        //0 CANCELADO, 1 ATIVO
        [Column("status")]
        [DataType("int")]
        public int Status { get; set; }

        //1 DEFAULT, 2 PREMIUM
        [Column("type_entity")]
        [DataType("int")]
        public int EntityType { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        public DateTime DateUpdate { get; set; }

        public User User { get; set; }






    }
}