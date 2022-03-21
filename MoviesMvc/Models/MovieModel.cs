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
        //[Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(4)]
        public string ProductionYear { get; set; }

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
        public List<ReviewModel> Reviews { get; set; }
        public List<int> DirectorId { get; set; }
    }
}