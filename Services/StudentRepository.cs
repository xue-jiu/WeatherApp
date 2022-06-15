using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class StudentRepository : IStuRepository
    {
        private readonly  SchoolDbcontext _context;
        public StudentRepository(SchoolDbcontext context)
        {
            _context = context;
        }
        //获取某个单元学生的成绩
        public Grade GetGrade(int unit, int studentId)
        {
            return _context.Grades.FirstOrDefault(c => c.Unit == unit && c.StudentId == studentId);
        }
        //获得某个学生的成绩
        public IEnumerable<Grade> GetGradesByStudentId(int studentId)
        {
            return _context.Grades.Where(c => c.StudentId == studentId).ToList();
        }
        //查到一个学生,并将其成绩也查出来
        public Student GetStudent(int studentId)
        {
            return _context.Students.Include(t=>t.Grades).FirstOrDefault(c => c.StudentId == studentId);
        }
        //根据姓名查出某些学生
        public IEnumerable<Student> GetStudents(string keyword)
        {
            IQueryable<Student> result = _context.Students.Include(t => t.Grades);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword.Trim();
                result = result.Where(t => t.Name.Contains(keyword));
            }
            return result.ToList();
        }
        public IEnumerable<Student> GetStudents(string keyword,int teacherId)
        {
            IQueryable<Student> result =
                _context.MyClasses
                .Join(_context.Students, c => c.StudentId, m => m.StudentId, (c, m) => new { MyClass = c, Student = m })
                .Where(c => c.MyClass.TeacherId == teacherId)
                .Select(c => c.Student);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword.Trim();
                result = result.Where(t => t.Name.Contains(keyword));
            }
            return result.ToList();
        }

        //判断某个学生是否存在
        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(c => c.StudentId == studentId);
        }

        //增
        public void CreateStudent(Student student) 
        {
            if (student==null)
            {
                throw new ArgumentException(nameof(student));
            }
            _context.Students.Add(student);
        }
        //保存
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        //删除
        public void DeleteStu(Student student)
        {
            _context.Students.Remove(student);
        }
        //查到某个教师所有学生的信息
        public IEnumerable<Student> GetStudentsByTeacherId(int TeacherId)
        {
            return _context.MyClasses
                .Join(_context.Students, c => c.StudentId, m => m.StudentId, (c, m) => new { MyClass = c, Student = m })
                .Where(c => c.MyClass.TeacherId == TeacherId)
                .Select(c => c.Student)
               .Include(t=>t.Grades).ToList();
        }
        //定义某个学生成为一个老师的学生
        public void BeMyStudent(MyClass myClass)
        {
            _context.MyClasses.Add(myClass);
        }

        public bool ClassesExists(int studentId, int teacherId)
        {
            return _context.MyClasses.Any(c => c.StudentId == studentId&&c.TeacherId==teacherId);
        }
        //定义找到某个关系
        public MyClass FindRelationship(int studentId, int teacherId)
        {
            return _context.MyClasses.FirstOrDefault(c => c.StudentId == studentId && c.TeacherId == teacherId);
        }
        public void DeleteClass(MyClass myClass)
        {
            _context.MyClasses.Remove(myClass);
        }
    }
}
