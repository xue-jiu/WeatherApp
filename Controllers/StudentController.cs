using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Dtos;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class StudentController
    {
        private readonly IStuRepository _stuRepository;
        private readonly IMapper _mapper;
        private IGradeaRepository _gradeaRepository;
        public StudentController(IStuRepository stuRepository, IMapper mapper, IGradeaRepository gradeaRepository)
        {
            _stuRepository = stuRepository;
            _mapper = mapper;
            _gradeaRepository = gradeaRepository;
        }
        //查出所有或者关键词的学生
        public IEnumerable<StudentDto> GetStudents(string keyword)
        {
            var StudentFromRepo = _stuRepository.GetStudents(keyword);
            if (StudentFromRepo == null || StudentFromRepo.Count() <= 0)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<StudentDto>>(StudentFromRepo);
        }
        //查出一个老师的学生
        public IEnumerable<StudentDto> FindStudentByTeacherId(int TeacherId)
        {
            var StudentFromRepo = _stuRepository.GetStudentsByTeacherId(TeacherId);
            return _mapper.Map<IEnumerable<StudentDto>>(StudentFromRepo);
        }
        //查出部分或者关键词的学生
        public IEnumerable<StudentDto> GetStudents(string keyword,int teacherId)
        {
            var StudentFromRepo = _stuRepository.GetStudents(keyword, teacherId);
            if (StudentFromRepo == null || StudentFromRepo.Count() <= 0)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<StudentDto>>(StudentFromRepo);
        }
        //查出选定的一个学生
        public StudentDto GetStudent(StudentDto student)
        {
            var StuFromUi = _mapper.Map<Student>(student);
            var StudentFromRepo = _stuRepository.GetStudent(StuFromUi.StudentId);
            if (StudentFromRepo == null)
            {
                return null;
            }
            return _mapper.Map<StudentDto>(StudentFromRepo);
        }

        //接受一个学生dto,从数据仓库中找出成绩,返回成绩列表
        public IEnumerable<GradeDto> GetGrade(StudentDto student)
        {
            var StuFromUi = _mapper.Map<Student>(student);
            if (!_stuRepository.StudentExists(StuFromUi.StudentId))
            {
                return null;
            }
            var GradeFromRopo = _stuRepository.GetGradesByStudentId(StuFromUi.StudentId);
            if (GradeFromRopo != null || GradeFromRopo.Count() <= 0)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<GradeDto>>(GradeFromRopo);
        }
        //添加一个学生,并返回
        public StudentDto CreateStudent(StudentCreateDto student)
        {
            //由dto传过来,就需要进行数据验证,故不用if判断
            //已经解决,automapper没有双向转换,故继续使用
            var StuFromUi = _mapper.Map<Student>(student);//这行代码不知道为什么有问题,暂且用下列代替
            //var StuFromUi = new Student() { Address = student.Address, Telephone = student.Telephone, Name = student.Name };
            _stuRepository.CreateStudent(StuFromUi);
            _stuRepository.Save();
            return _mapper.Map<StudentDto>(StuFromUi);
        }
        //更新一个学生信息
        public void UpdateStudent(StudentDto student)
        {
            var StuFromUi = _mapper.Map<Student>(student);
            if (StuFromUi == null)
            {
                return;
            }
            if (!_stuRepository.StudentExists(StuFromUi.StudentId))
            {
                return;
            }
            var stuFromRepo = _stuRepository.GetStudent(StuFromUi.StudentId);
            _mapper.Map(student, stuFromRepo);
            _stuRepository.Save();
            //映射成student
            //更新dto数据
            //映射回Studentdto
        }
        //删除一个学生的信息,尚未定义完整
        public void DeleteStudent(StudentDto student)
        {
            //找到学生,删除他,把他的成绩也给删了
            var GradeFromRopo = _stuRepository.GetStudent(student.StudentId);
            _stuRepository.DeleteStu(GradeFromRopo);
        }
        //新建学生老师关系
        public void AddRelationShip(int teacherId, StudentDto student)
        {
            if (_stuRepository.ClassesExists(student.StudentId, teacherId))
            {
                return;
            }
            var StuFromUi = _mapper.Map<Student>(student);
            if (!_stuRepository.StudentExists(StuFromUi.StudentId))
            {
                return;
            }
            var Classes = new MyClass() { TeacherId = teacherId, StudentId = StuFromUi.StudentId };
            _stuRepository.BeMyStudent(Classes);
            _stuRepository.Save();
        }
        //删除学生老师关系
        public void DeleteRelationship(int teacherId, int studentId)
        {
            if (!_stuRepository.ClassesExists(studentId, teacherId))
            {
                return;
            }
           var Relationship= _stuRepository.FindRelationship(studentId, teacherId);
            _stuRepository.DeleteClass(Relationship);
            _stuRepository.Save();
        }
        //创建ShowAndGrade
        public List<ShowStuAndGradeDTO> CreateGridShowDto(IEnumerable<StudentDto> students, int unit)
        {
             _gradeaRepository = new GradeRepository(new SchoolDbcontext());
            var StudentWithUnitGradeList = new List<ShowStuAndGradeDTO>();
            foreach (var student in students)
            {
                if (!_gradeaRepository.GradeExists(student.StudentId, unit))
                {
                    var newGrad = new Grade() { Unit = unit, StudentId = student.StudentId };
                    _gradeaRepository.AddGrade(newGrad);
                    _gradeaRepository.Save();
                } 
                var GradeFromRepo = _gradeaRepository.GetGrade(student.StudentId, unit);
                GradeDto gradeDto = _mapper.Map<GradeDto>(GradeFromRepo);
                var StudentWithUnitGrade = new ShowStuAndGradeDTO(student, gradeDto);
                StudentWithUnitGradeList.Add(StudentWithUnitGrade);
            }
            return StudentWithUnitGradeList;
        }
    }
}
