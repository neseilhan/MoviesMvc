using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MoviesMvc.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        [StringLength(1000, ErrorMessage = "{0} must have maximum {1} characters!")]

        public string Content { get; set; }

        public int Rating { get; set; }

        [StringLength(200, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string Reviewer { get; set; }

        [Required(ErrorMessage = "{0} must not be empty!")]
        public DateTime? Date { get; set; }

        [DisplayName("Date")]
        public string DateText => Date.HasValue ? Date.Value.ToString("yyyy/MM/dd", new CultureInfo("en")) : "";

        [Required(ErrorMessage = "{0} must not be empty!")]
        [DisplayName("Date")]
        public string DateValue // edit view
        {
            get
            {
                if (Date == null)
                    return "";
                return Date.Value.ToString("MM/dd/yyyy", new CultureInfo("en"));

            }
            set
            {
                Date = null;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Date = DateTime.Parse(value, new CultureInfo("en"));

                }
            }
        }

        [Required(ErrorMessage ="{0} must be selected!")]
        [DisplayName("Movie")]

        public int MovieId { get; set; }

        public MovieModel Movie { get; set; }

        public List<int> AllRatings { get; set; }

    }
}