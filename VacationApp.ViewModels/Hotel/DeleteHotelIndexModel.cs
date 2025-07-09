using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacation.GConstants;

namespace VacationApp.ViewModels.Hotel
{
    public class DeleteHotelIndexModel
    {

        public string Idhotel { get; set; }

        [Required]
        [MaxLength(ValidationConstants.HotelMaxLenght)]
        [MinLength(ValidationConstants.HotelMinLenght)]
        public string HotelName { get; set; } = null!;


        [Required]
        public string NameManager { get; set; } = null!;
    }
}
