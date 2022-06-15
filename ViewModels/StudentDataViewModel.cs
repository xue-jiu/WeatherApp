using AutoMapper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;
using WeatherApp.Dtos;
using WeatherApp.Extensions;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class StudentDataViewModel : BindableBase,INavigationAware
    {
        public ObservableCollection<StudentDto> Students { get; set; } = new ObservableCollection<StudentDto>();
        public DelegateCommand<StudentDto> DeleStuCommand { get; set; }
        public DelegateCommand NavigaCommand { get; private set; }
        public DelegateCommand CreateStuCommand { get; private set; }
        public DelegateCommand<string> GetStudentCommand { get; set; }
        public DelegateCommand<StudentDto> BeMyStuCommand { get; set; }
        private int teacherId;
        public int TeacherId
        {
            get { return teacherId; }
            set { teacherId = value; RaisePropertyChanged(); }
        }

        public void BeMyStu(StudentDto studentDto)
        {
            _studentController.AddRelationShip(TeacherId, studentDto);
        }

        //弹出新建学生窗体
        private void DialogToCreateStu()
        {
            DialogParameters Para = new DialogParameters();
            Para.Add("title", "AddStudent");
            //arg是IDialogResult
            _dialog.ShowDialog("AddStudent", Para, arg =>
            {
                if (arg.Result == ButtonResult.OK)
                {
                    var studentNewCreate = arg.Parameters.GetValue<StudentDto>("student");
                    Students.Add(studentNewCreate);
                }
            });
        }

        //导航到MyStudent
        private void NavigateToGrade()
        {
            //if (obj == null || string.IsNullOrWhiteSpace(obj))
            //{
            //    return;
            //}
            NavigationParameters key = new NavigationParameters();
            key.Add("TeacherId", TeacherId);
            _regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate("MyStudent", key);
        }
        //获取学生成绩
        private void GetStudent(string keyword) 
        {
            var stus =  _studentController.GetStudents(keyword);
            Students.Clear();
            Students.AddRange(stus);
        }
        //更新学生信息
        private void UpdateStudentData(StudentDto student)
        {
            _studentController.UpdateStudent(student);
        }
        //接收navigation传来的信息
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //if (navigationContext.Parameters.ContainsKey("TeacherId"))
                TeacherId = navigationContext.Parameters.GetValue<int>("TeacherId");
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        //构造函数
        private readonly StudentController _studentController;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialog;
        public StudentDataViewModel(StudentController studentController, IRegionManager regionManager,IDialogService dialog)
        {
            _studentController = studentController;
            _regionManager = regionManager;
            _dialog = dialog;
            GetStudentCommand = new DelegateCommand<string>(GetStudent);
            NavigaCommand = new DelegateCommand(NavigateToGrade);
            CreateStuCommand = new DelegateCommand(DialogToCreateStu);
            BeMyStuCommand = new DelegateCommand<StudentDto>(BeMyStu);
            GetStudent(" ");
            TryCommand = new DelegateCommand(Try);
        }
        //绑定datagrid row
        private StudentDto _selectedStu;
        public StudentDto SelectedStu
        {
            get => _selectedStu;
            set
            {
                _studentController.UpdateStudent(_selectedStu);
                _selectedStu = value;
                RaisePropertyChanged();
            }
        }

        public string TryDoIt { get; set; }
        public void Try()
        {
            TryDoIt = "hahahah";
        }
        public DelegateCommand TryCommand { get; set; }
    }
}
