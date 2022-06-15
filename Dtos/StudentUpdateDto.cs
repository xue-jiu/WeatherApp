using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Dtos
{
    public class StudentUpdateDto
    {
        private string name;
        private string telephone;
        private string address;

        [Required]
        [MaxLength(50)]
        public string Name { get => name; set { name = value; } }
        [Required]
        [MaxLength(50)]
        public string Telephone { get => telephone; set { telephone = value; } }
        [Required]
        [MaxLength(50)]
        public string Address { get => address; set { address = value; } }
    }
}
