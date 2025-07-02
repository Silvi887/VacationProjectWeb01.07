using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApp.ViewModels.Vacation
{
    public class AllReservationsViewModel
    {

        [Key]
        public int IdReservation { get; set; }

        [Required]
        public string HotelName { get; set; } = "";

        [Required]
        public string StartDate { get; set; } = null!;

        [Required]
        public string EndDate { get; set; } = null!;

        [Required]
        public bool IsUserGuest { get; set; }

        public bool IsManager { get; set; }



    }
}
