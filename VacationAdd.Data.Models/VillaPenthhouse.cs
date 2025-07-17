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
    public class VillaPenthhouse
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVilla { get; set; }


        [Required]
        [MaxLength(ValidationConstants.HotelMaxLenght)]
        [MinLength(ValidationConstants.HotelMinLenght)]
        public string NameVilla { get; set; } = null!;

        [Required]
        public int IdPlace { get; set; }

        [ForeignKey(nameof(IdPlace))]
        public virtual TypePlace TypePlace { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLenght)]
        [MinLength(ValidationConstants.DescriptionMinLenght)]
        public string VillaInfo { get; set; } = null!;

        [Required]
        public string VillaAddress { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public int CountRooms { get; set; }

        [Required]
        public int CountAdults { get; set; }

        [Required]
        public int CountChildren { get; set; }

        [Required]
        public int Bedrooms { get; set; } = 1;

        [Required]
        public int Bathrooms { get; set; } = 1;

        [Required]
        public string Area { get; set; }=null!;

        [Required]
        public bool Parking { get; set; }=true;

        [Required]
        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;


        [Required]
        public string IDManager { get; set; } = null!;

        [ForeignKey(nameof(IDManager))]
        public virtual IdentityUser Manager { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public virtual ICollection<UserVilla> UserVillas { get; set; } = new HashSet<UserVilla>();
    }


}
