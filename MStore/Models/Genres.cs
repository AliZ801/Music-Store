using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Models
{
    public class Genres
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}
