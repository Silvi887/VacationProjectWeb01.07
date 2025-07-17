using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationAdd.Data.Models
{
    public class TypePlace
    {
        [Key]
        public int PlaceId { get;set; }

        [Required]
        public string NamePlace { get; set; } = null!;

        public virtual ICollection<VillaPenthhouse> VillaPenthhouses { get; set; } = new HashSet<VillaPenthhouse>();
    }
}
