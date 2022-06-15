using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Dtos;
using WeatherApp.Models;

namespace WeatherApp.Profiles
{ 
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentCreateDto>();
            CreateMap<StudentCreateDto,Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentUpdateDto>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<ShowStuAndGradeDTO, StudentDto>();
            CreateMap<StudentDto, ShowStuAndGradeDTO>();
            CreateMap<ShowStuAndGradeDTO, Student>();
            CreateMap<Student, ShowStuAndGradeDTO>();
        }
    }
}
