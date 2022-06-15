using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class TeacherController
    {
        ITeaRepository _teaRepository;
        IMapper _mapper;
        public TeacherController(IMapper mapper, ITeaRepository teaRepository)
        {
            _mapper = mapper;
            _teaRepository = teaRepository;
        }
        public bool LoginTea(string teacherName,string Password)
        {
            if(string.IsNullOrWhiteSpace(teacherName)||string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            if (!_teaRepository.TeaExist(teacherName))
            {
                return false;
            }
           if( _teaRepository.MatchPassWord(teacherName)==Password)
            {
                return true;
            }
            return false;
        }

        public Teacher FindTeacherName(string name)
        {
            if (!_teaRepository.TeaExist(name))
            {
                return null;
            }
            return _teaRepository.FindTeacherByName(name);
        }
    }
}
