using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApp.ViewModels.Vacation
{
    public class DeleteReservationIndexModel
    {
        [Key]
        public int IdReservation { get; set; }

        [Required]
        public string StartDate { get; set; } = null!;

        [Required]
        public string EndDate { get; set; } = null!;

        [Required]
        public string GuestFirstName { get; set; } = "";

        public string VillaName { get; set; } = "";

    }
}
