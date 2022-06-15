using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Dtos
{
    public class ShowStuAndGradeDTO : BindableBase
    {
        private int studentId;
        private string name;
        private string telephone;
        private string address;
        public int StudentId { get => studentId; set { studentId = value; RaisePropertyChanged(); } }
        [Required]
        [MaxLength(50)]
        public string Name { get => name; set { name = value; RaisePropertyChanged(); } }
        [Required]
        [MaxLength(50)]
        public string Telephone { get => telephone; set { telephone = value; RaisePropertyChanged(); } }
        [Required]
        public string Address { get => address; set { address = value; RaisePropertyChanged(); } }

        private int unit;
        private double math;
        private double english;
        private double chinese;
        private double physical;
        public int Unit { get => unit; set { unit = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Math { get => math; set { math = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double English { get => english; set { english = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Chinese { get => chinese; set { chinese = value; RaisePropertyChanged(); } }
        [Range(0, 200)]
        public double Physical { get => physical; set { physical = value; RaisePropertyChanged(); } }

        public ShowStuAndGradeDTO(StudentDto student, GradeDto grade)
        {
            StudentId = student.StudentId;
            Name = student.Name;
            Telephone = student.Telephone;
            Address = student.Address;
            //下方代码可以不写,这里学生的默认成绩为0;
            if (grade != null)
            {
                Unit = grade.Unit;
                Math = grade.Math;
                English = grade.English;
                Chinese = grade.Chinese;
                Physical = grade.Physical;
            }
        }
    }
}
