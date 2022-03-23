using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesMvc.Models
{
    public class MovieModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty!")]
        [StringLength(250, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("Movie Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must not be empty!")]
        [DisplayName("Production Year")]
        public string ProductionYear { get; set; }

        [DisplayName("Box Office Return")]
        public double? BoxOfficeReturn { get; set; }

        public List<DirectorModel> Directors { get; set; }

        private string _directorNamesHtml;
        [DisplayName("Directors")]
        public string DirectorNamesHtml
        {
            get
            {
                _directorNamesHtml = "";
                if (Directors != null && Directors.Count > 0)
                {
                    foreach (DirectorModel directormodel in Directors)
                    {
                        _directorNamesHtml += directormodel.Name + "" + directormodel.Surname + "<br />";
                    }

                }
                return _directorNamesHtml;
            }
        }
        [DisplayName("Directors")]
        public List<int> DirectorIds { get; set; }
        public List<ReviewModel> Reviews { get; set; }
       
    }
}