using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApp.ViewModels.Hotel
{
    public class ModelTown
    {
        [Key]
        public int IdTown { get; set; }


        public string TownName { get; set; }
    }
}
