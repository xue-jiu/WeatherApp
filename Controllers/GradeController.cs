using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Dtos;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class GradeController
    {
        IGradeaRepository _gradeRepository;
        IMapper _mapper;
        public GradeController(IMapper mapper, IGradeaRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }
        public IEnumerable<double> GetMathById(int studentId) 
        {
            if (studentId != 0) 
            { 
            return _gradeRepository.FindMathByStudentId(studentId);
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<double> GetEnglishById(int studentId)
        {
            if (studentId != 0)
            {
                return _gradeRepository.FindEnglishByStudentId(studentId);
            }
            return null;
        }
        public IEnumerable<double> GetChineseById(int studentId)
        {
            if (studentId != 0)
            {
                return _gradeRepository.FindChineseByStudentId(studentId);
            }
            return null;
        }
        public IEnumerable<double> GetPhysicalById(int studentId)
        {
            if (studentId != 0)
            {
                return _gradeRepository.FindPhysicalByStudentId(studentId);
            }
            return null;
        }
        public IEnumerable<int> GetUnitById(int studentId)
        {
            if (studentId != 0)
            {
                return _gradeRepository.FindUnitByStudentId(studentId);
            }
            return null;
        }
        //增加一段成绩
        public void AddGrade(ShowStuAndGradeDTO showStuAndGradeDTO,int unit)
        {
           var GradeFromUi= _mapper.Map<GradeDto>(showStuAndGradeDTO);
            var grade = _mapper.Map<Grade>(GradeFromUi);
            if (!_gradeRepository.GradeExists(grade.StudentId, unit))
            {
                _gradeRepository.AddGrade(grade);
                _gradeRepository.Save();
            }
        }

        public void UpdateGrade(ShowStuAndGradeDTO showStuAndGradeDTO, int unit)
        {
            //var grade = _mapper.Map<Grade>(GradeFromUi);
            //var gradeFromRepo = _gradeRepository.GetGrade(grade.StudentId, unit);
            if (showStuAndGradeDTO == null)
            {
                return;
            }
            var gradeFromRepo = _gradeRepository.GetGrade(showStuAndGradeDTO.StudentId, unit);
            _mapper.Map(showStuAndGradeDTO, gradeFromRepo);
            _gradeRepository.Save();
        }
    }
}
