using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationAdd.Data.Models
{
    public class UserVilla
    {


        [Required]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        [Required]
        public int VillaId { get; set; }

        [ForeignKey(nameof(VillaId))]
        public VillaPenthhouse VillaPenthhouse { get; set; } = null!;
    }
}

