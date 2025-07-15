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
    public class Room
    {

        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRoom { get; set; }


        [Required]
        [MaxLength(ValidationConstants.TownMaxLenght)]
        [MinLength(ValidationConstants.TownMinLenght)]
        public string NameRoom { get; set; }


        [Required]
        public int CapacityAdultsCount { get; set; }

        [Required]
        public int CapacityChildrenCount { get; set; }

        public string RoomSize { get; set; }

        public string? View { get; set; } = "";

        public string? Facilities { get; set; } = "";


    }
}
