using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private string _fullName;
        public string FullName
        {
            get
            {
                _fullName = Name + "" + Surname;
                return _fullName;
            }
        }

        public bool Retired { get; set; }
        [DisplayName ("Retired")]
        public string RetiredText => Retired ? "Yes" : "No";

        public List<MovieModel> Movies { get; set; } 
        [Required(ErrorMessage ="At  least one movie must be selected!")]

        [DisplayName("Movies")]
        public List<int> MovieIds { get; set; }

        [DisplayName("Movies")]
        public string MoviesText => Movies == null || Movies.Count == 0 ? "" : string.Join("<br />", Movies.Select(m => m.Name));
    }
}