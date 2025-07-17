using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationAdd.Data.Models
{
    public class HotelRooms
    {


        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelRoomID { get; set; }

        public int HotelID { get; set; }

        [ForeignKey(nameof(HotelID))]
        public Hotel Hotel { get;set; }

        public int RoomID { get; set; }

        [ForeignKey(nameof(RoomID))]
        public Room Room { get; set; }

        public int CountRooms { get; set; }
    }
}
