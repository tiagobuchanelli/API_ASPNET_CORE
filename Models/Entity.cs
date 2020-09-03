using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("COSTUMERS")] //renomear a tabela no banco
    public class Entity
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("NAME")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [DataType("varchar")]
        public string Name { get; set; }


        [Column("LAST_NAME")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string LastName { get; set; }


        [Column("EMAIL")]
        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [DataType("varchar")]
        public string Email { get; set; }


        [Column("ADDRESS")]
        [MaxLength(300, ErrorMessage = "Este campo deve conter no máximo 300 caracteres")]
        [DataType("varchar")]
        public string Address { get; set; }

        [Column("ADDRESS_NR")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [DataType("varchar")]
        public string AddressNr { get; set; }

        [Column("NEIGHBORHOOD")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Neighborhood { get; set; }


        [Column("ZIP_CODE")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter no máximo 10 caracteres")]
        [DataType("varchar")]
        public string ZipCode { get; set; }

        [Column("PHONE")]
        [MaxLength(15, ErrorMessage = "Este campo deve conter no máximo 15 caracteres")]
        [DataType("varchar")]
        public string Phone { get; set; }

        [Column("STATUS")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("TYPE_ENTITY")] //1 CLIENTE, 2 FORNECEDOR, 3 OUTRS
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [Range(1, 3, ErrorMessage = "Tipo inválido - Os valores devem ser 1,2 ou 3")]
        [DataType("int")]
        public int EntityType { get; set; }

        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


    }
}