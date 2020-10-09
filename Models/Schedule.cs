using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("schedules")] //renomear a tabela no banco
    public class Schedule
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("order_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int OrderId { get; set; }

        [Column("costumer_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string CostumerId { get; set; }


        [Column("company_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string CompanyId { get; set; }

        //0 CANCELADO, 1 ATIVO
        [Column("status")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        public DateTime DateUpdate { get; set; }

        [Column("date_start")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateStart { get; set; }

        [Column("date_end")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateEnd { get; set; }

        //objetos completos
        public Order Order { get; set; }

        public User Costumer { get; set; }
        public Company Company { get; set; }

    }
}