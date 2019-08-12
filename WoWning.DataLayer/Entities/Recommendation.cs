using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoWning.DataLayer.Entities
{
    public class Recommendation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GiverId { get; set; }
        public string TakerId { get; set; }
        public DateTime Date { get; set; }
    }
}