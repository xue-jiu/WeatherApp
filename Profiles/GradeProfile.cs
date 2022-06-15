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
    public class GradeProfile:Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeCreateDto>();
            CreateMap<GradeCreateDto, Grade>();
            CreateMap<GradeDto, Grade>();
            CreateMap<Grade, GradeDto>();
            CreateMap<ShowStuAndGradeDTO, GradeDto>();
            CreateMap<GradeDto, ShowStuAndGradeDTO > ();
            CreateMap<ShowStuAndGradeDTO, Grade>();
            CreateMap<Grade, ShowStuAndGradeDTO>();
        }
    }
}
