using AutoMapper;
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
    public class GradeDto : BindableBase
    {
        private int unit;
        private double math;
        private double english;
        private double chinese;
        private double physical;
        private int studentId;
        public int Unit { get => unit; set { unit = value;RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Math { get => math; set { math = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double English { get => english; set { english = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Chinese { get => chinese; set { chinese = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Physical { get => physical; set { physical = value; RaisePropertyChanged(); } }
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        //public int StudentId { get => studentId; set { studentId = value; RaisePropertyChanged(); } }
    }
}
