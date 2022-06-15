using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    class TeaRepository : ITeaRepository
    {
        private readonly SchoolDbcontext _context;
        public TeaRepository(SchoolDbcontext context)
        {
            _context = context;
        }
        public void AddTeacher(Teacher Teacher)
        {
            if (Teacher == null)
            {
                throw new ArgumentNullException(nameof(Teacher));
            }
            _context.Teachers.Add(Teacher);
        }
        public bool TeaExist(string TeacherName)
        {
            return _context.Teachers.Any(t => t.Name == TeacherName);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public string MatchPassWord(string Name)
        {
          return _context.Teachers.Where(c => c.Name == Name).Select(c => c.Password).FirstOrDefault();
        }

        public Teacher FindTeacherByName(string Name)
        {
            return _context.Teachers.Where(c => c.Name == Name).FirstOrDefault();
        }
    }
}
