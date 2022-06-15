using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Models
{
    public class Grade
    {
        public int Unit { get; set; }
        [Range(0, 200)]
        public double Math { get; set; }
        [Range(0, 200)]
        public double English { get; set; }
        [Range(0, 200)]
        public double Chinese { get; set; }
        [Range(0, 200)]
        public double Physical { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
