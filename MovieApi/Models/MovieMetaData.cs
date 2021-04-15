using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models
{
    public class MovieMetaData
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Duration {get ; set;}
        [Required]
        public string ReleaseYear { get; set; }

    }
}
