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
    public class TeacherDto : BindableBase
    {
        private int teacherId;
        private string profession;
        private string telephone;
        private string address;
        private string name;
        private string password;

        [Key]
        public int TeacherId { get => teacherId; set { teacherId = value; RaisePropertyChanged(); } }
        [Required]
        public string Profession { get => profession; set { profession = value; RaisePropertyChanged(); } }
        [Required]
        public string Telephone { get => telephone; set { telephone = value; RaisePropertyChanged(); } }
        [Required]
        public string Address { get => address; set { address = value; RaisePropertyChanged(); } }
        [Required]
        public string Name { get => name; set { name = value; RaisePropertyChanged(); } }
        public string Password { get => password; set { password = value; RaisePropertyChanged(); } }
    }
}
