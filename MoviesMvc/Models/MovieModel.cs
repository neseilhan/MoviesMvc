using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesMvc.Models
{
    public class MovieModel
    {
        //[Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(4)]
        public string ProductionYear { get; set; }

        public double? BoxOfficeReturn { get; set; }
        public List<int> DirectorIds { get; set; }
    }
}