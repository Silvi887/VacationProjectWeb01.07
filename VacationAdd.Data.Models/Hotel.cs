using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacation.GConstants;

namespace VacationAdd.Data.Models
{
    public class Hotel
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHotel { get; set; }


        [Required]
        [MaxLength(ValidationConstants.HotelMaxLenght)]
        [MinLength(ValidationConstants.HotelMinLenght)]
        public string HotelName { get; set; } = null!;

        [Required]
        public string AddressHotel { get; set; } = null!;

        [Required]
        public int Stars { get; set; }

       
        public string IdsRooms { get; set; } = "";

        [Required]
        public int RoomCapacityCount { get; set; }

        [Required]
        public int RoomBookedRooms { get; set; } = 0;


        public string? ImageUrl { get; set; }


        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLenght)]
        [MinLength(ValidationConstants.DescriptionMinLenght)]
        public string HotelInfo { get; set; } = null!;


        [Required]
        public string IDManager { get; set; } = null!;

        [ForeignKey(nameof(IDManager))]
        public virtual IdentityUser Manager { get; set; }=null!;


        [Required]
        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Reservation>  Reservations {get;set;}=new HashSet<Reservation>();

        public virtual ICollection<UserHotel> UsersHotels { get; set; } = new HashSet<UserHotel>();
       public virtual ICollection<HotelRooms> hotelRooms { get; set; } = new HashSet<HotelRooms>();





    }
}
