using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApp.ViewModels.Hotel
{
    public  class TownModel
    {

        [Key]
        public int IdTown { get; set; }

        [Required]
        public string NameTown { get; set; } = null!;
    }
}
