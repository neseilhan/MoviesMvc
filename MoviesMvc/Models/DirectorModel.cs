using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesMvc.Models
{
    public class DirectorModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool Retired { get; set; }
    }
}