using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class GradeRepository : IGradeaRepository
    {
        private readonly SchoolDbcontext _context;
        public GradeRepository(SchoolDbcontext context)
        {
            _context = context;
        }

        public void AddGrade(Grade Grade)
        {
            if (Grade == null)
            {
                throw new ArgumentException(nameof(Grade));
            }
            _context.Grades.Add(Grade);
        }

        public void DeleGrade(Grade Grade)
        {
            _context.Grades.Remove(Grade);
        }

        public void DeleGradeMore(List<int> GradeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> FindMathByStudentId(int StudentId)
        {
            return _context.Grades.Where(c => c.StudentId == StudentId).OrderBy(c => c.Unit).Select(c => c.Math).ToList();
        }
        public IEnumerable<double> FindEnglishByStudentId(int StudentId)
        {
            return _context.Grades.Where(c => c.StudentId == StudentId).OrderBy(c => c.Unit).Select(c => c.English).ToList();
        }
        public IEnumerable<double> FindChineseByStudentId(int StudentId)
        {
            return _context.Grades.Where(c => c.StudentId == StudentId).OrderBy(c => c.Unit).Select(c => c.Chinese).ToList();
        }
        public IEnumerable<double> FindPhysicalByStudentId(int StudentId)
        {
            return _context.Grades.Where(c => c.StudentId == StudentId).OrderBy(c => c.Unit).Select(c => c.Physical).ToList();
        }
        public IEnumerable<int> FindUnitByStudentId(int StudentId)
        {
            return _context.Grades.Where(c => c.StudentId == StudentId).OrderBy(c => c.Unit).Select(c => c.Unit).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
       
        public bool GradeExists(int studentId,int unit)
        {
            return _context.Grades.Any(c => c.StudentId == studentId&&c.Unit== unit);
        }

        public Grade GetGrade(int studentId, int unit)
        {
            return _context.Grades.FirstOrDefault(c => c.StudentId == studentId && c.Unit == unit);
        }
    }
}
