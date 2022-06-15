using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IGradeaRepository
    {
        public void AddGrade(Grade Grade);
        public void DeleGrade(Grade Grade);
        public void DeleGradeMore(List<int> GradeIds);
        Grade GetGrade(int studentId, int unit);
        bool Save();
        bool GradeExists(int studentId, int unit);
        //查
         IEnumerable<double> FindMathByStudentId(int StudentId);
         IEnumerable<double> FindEnglishByStudentId(int StudentId);
         IEnumerable<double> FindChineseByStudentId(int StudentId);
         IEnumerable<double> FindPhysicalByStudentId(int StudentId);
         IEnumerable<int> FindUnitByStudentId(int StudentId);
    }
}
