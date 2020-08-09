using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MStore.Models
{
    public class Singles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genres Genres { get; set; }

        [Required]
        [Display(Name = "Released Date")]
        public string ReleasedDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Art")]
        public string ImageUrl { get; set; }
    }
}
