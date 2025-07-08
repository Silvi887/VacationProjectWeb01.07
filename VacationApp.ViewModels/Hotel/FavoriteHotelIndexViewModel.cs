using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApp.ViewModels.Hotel
{
    public class FavoriteHotelIndexViewModel
    {
        [Key]
        public int IdHotel { get; set; }

        [Required]
        public string HotelName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public string TownName { get; set; } = null!;
    }
}
