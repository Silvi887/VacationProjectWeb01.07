﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacation.GConstants;

namespace VacationApp.ViewModels.Hotel
{
    public class EditHotelModel
    {

        public string Idhotel { get; set; }

        [Required]
        [MaxLength(ValidationConstants.HotelMaxLenght)]
        [MinLength(ValidationConstants.HotelMinLenght)]
        public string HotelName { get; set; } = null!;

        [Required]
        public int Stars { get; set; }


        [Required]
        public int NumberofRooms { get; set; }


        public string? ImageUrl { get; set; }


        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLenght)]
        [MinLength(ValidationConstants.DescriptionMinLenght)]
        public string HotelInfo { get; set; } = null!;


        [Required]
        public string IDManager { get; set; } = null!;

        //[ForeignKey(nameof(IDManager))]
        //public virtual IdentityUser Manager { get; set; } = null!;


      //  public IEnumerable<TownModel> ListTowns { get; set; } = null!;
    }
}
