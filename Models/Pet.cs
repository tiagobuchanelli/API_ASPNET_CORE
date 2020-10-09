using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("pets")] //renomear a tabela no banco
    public class Pet
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }

        [Column("user_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(1, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        public int UserId { get; set; }

        //UID do Usuário. Utilizado para identificar por meio do uid_user que está presente no token. 
        [Column("user_uid")]
        // [DataType("varchar")]
        public string UserUid { get; set; }


        //0 CANCELADO, 1 ATIVO
        [Column("status")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("name_pet")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string NamePet { get; set; }

        [Column("color_pet")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string ColorPet { get; set; }

        [Column("age")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Age { get; set; }

        [Column("species")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Species { get; set; }

        [Column("race")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Race { get; set; }

        [Column("pelage")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Pelage { get; set; }

        //1 MACHO, 2 FEMEA, 3 INDEFINIDO
        [Column("gender")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Gender { get; set; }

        [Column("microship_nr")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string MicroshipNr { get; set; }

        [Column("pedigree_nr")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string PedigreeNr { get; set; }

        [Column("birth_date")]
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime BirthDate { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }


        [Column("date_update")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public User User { get; set; }




    }
}