using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Dtos
{
    public class StudentDto : BindableBase
    {
        private int studentId;
        private string name;
        private string telephone;
        private string address;

        [Key]
        public int StudentId { get => studentId; set { studentId = value; RaisePropertyChanged(); } }
        [Required]
        [MaxLength(50)]
        public string Name { get => name; set { name = value; RaisePropertyChanged(); } }
        [Required]
        [MaxLength(50)]
        public string Telephone { get => telephone; set { telephone = value; RaisePropertyChanged(); } }
        [Required]
        public string Address { get => address; set { address = value; RaisePropertyChanged(); } }

        public List<GradeDto> Grades { get; set; } = new List<GradeDto>();
    }
}
