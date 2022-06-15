using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IStuRepository
    {
        //查
        IEnumerable<Student> GetStudents(string keyword);
        IEnumerable<Student> GetStudents(string keyword, int teacherId);
        IEnumerable<Student> GetStudentsByTeacherId(int TeacherId);
        Student GetStudent(int studentId);
        IEnumerable<Grade> GetGradesByStudentId(int studentId);
        Grade GetGrade(int unit, int studentId);
        void BeMyStudent(MyClass myClass);
        MyClass FindRelationship(int studentId, int teacherId);
        //增
        void CreateStudent(Student student);
        //删
        void DeleteStu(Student student);
        void DeleteClass(MyClass myClass);
        //保存,更新
        public bool Save();
        //判断存在与否
        bool StudentExists(int studentId);
        bool ClassesExists(int studentId,int teacherId);
    }
}
