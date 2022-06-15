using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface ITeaRepository
    {
        public void AddTeacher(Teacher Teacher);
        public bool TeaExist(string TeacherName);
        public string MatchPassWord(string Name);
        public Teacher FindTeacherByName(string Name);
        bool Save();
    }
}
