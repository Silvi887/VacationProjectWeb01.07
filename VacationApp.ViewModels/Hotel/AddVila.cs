using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacation.GConstants;
using VacationApp.ViewModels.Vacation;



namespace VacationApp.ViewModels.Hotel
{
    public class AddVila
    {


       // public string Idhotel { get; set; }

        [Required]
        [MaxLength(ValidationConstants.HotelMaxLenght)]
        [MinLength(ValidationConstants.HotelMinLenght)]
        public string VilaName { get; set; } = null!;

        //[Required]
        //public int Stars { get; set; }

        [Required]
        public string VillaAddress { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public int NumberofRooms { get; set; }

        [Required]
        public int CountAdults { get; set; }

        [Required]
        public int CountChildren { get; set; }




        //public string IdsRooms { get; set; } = "";

        [Required]
        public int Bedrooms { get; set; } = 1;

        [Required]
        public int Bathrooms { get; set; } = 1;

        [Required]
        public string Area { get; set; } = null!;

        [Required]
        public bool Parking { get; set; } = true;


        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLenght)]
        [MinLength(ValidationConstants.DescriptionMinLenght)]
        public string VillaInfo { get; set; } = "";


        [Required]
        public string IDManager { get; set; } = "";

        [Required]
        public string TownId { get; set; } = "";




        //[ForeignKey(nameof(IDManager))]
        //public virtual IdentityUser Manager { get; set; } = null!;


        public IEnumerable<TownModel> ListTowns { get; set; } = null!;


        public IEnumerable<PlaceViewModel> placedrp = null!;
    }
}
