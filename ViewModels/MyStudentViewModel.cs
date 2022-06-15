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
using System.Windows;
using WeatherApp.Controllers;
using WeatherApp.Dtos;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class MyStudentViewModel : BindableBase, IConfirmNavigationRequest
    {
        public ObservableCollection<ShowStuAndGradeDTO> Students { get; set; } = new ObservableCollection<ShowStuAndGradeDTO>();
        public DelegateCommand<string> GetStudentCommand { get; set; }
        public DelegateCommand AddUnit { get; set; }
        public DelegateCommand RemoveStudent { get; set; }
        public DelegateCommand DialogToShowGradeCommand { get; set; }
        public ObservableCollection<int> Unit { get; set; } = new ObservableCollection<int>();
        public Action<string> EnterCommand { get; set; }
        private IEnumerable<StudentDto> stuFromRepo;
        //在切换单元的时候.刷新页面
        private int unitSelected = 1;
        public int UnitSelected
        {
            get { return unitSelected; } 
            set
            { 
                unitSelected = value;
                RaisePropertyChanged();
                AddStudents();
            }
        }

        //更新datagrid
        private ShowStuAndGradeDTO selectedStu;
        public ShowStuAndGradeDTO SelectedStu
        {
            get { return selectedStu; }
            set {
                if (value != null)//若不加null判断,Unit主键就被修改了
                {
                    _gradeController.UpdateGrade(selectedStu, unitSelected);
                }
                selectedStu = value;
                RaisePropertyChanged(); 
            }
        }
        public void keyDown(string t)
        {
            if (selectedStu == Students.Last() && t == "enter")
            {
                _gradeController.UpdateGrade(selectedStu, unitSelected);
            }
        }
        //接收到teacherId后初始化datagrid
        private int teacherId;
        public int TeacherId
        {
            get { return teacherId; }
            set
            { 
                teacherId = value;
                RaisePropertyChanged();
                stuFromRepo = _studentController.FindStudentByTeacherId(teacherId);
                AddStudents();
            }
        }
        //增加一个新的单元
        public void AddNewUnit()
        {
            var last = Unit.Last();
            Unit.Add(last+1);
            UnitSelected= Unit.Last();
        }
        //刷新datagrid
        private void AddStudents()
        {
            Students.Clear();
            var studentAndGradeWithUnit = _studentController.CreateGridShowDto(stuFromRepo, UnitSelected);
            _ = Students.AddRange(studentAndGradeWithUnit);
        }
        //关键字查学生
        private void GetStudent(string keyword)
        {
            stuFromRepo = _studentController.GetStudents(keyword,TeacherId);
            AddStudents();
        }
        //删除选中的学生
        private void DeleteStudent()
        {
            _studentController.DeleteRelationship(TeacherId, SelectedStu.StudentId);
            stuFromRepo = _studentController.FindStudentByTeacherId(teacherId);
            Students.Remove(selectedStu);
        }

        readonly StudentController _studentController;
        readonly IDialogService _dialog;
        readonly GradeController _gradeController;
        public MyStudentViewModel(StudentController studentController, IDialogService dialog, GradeController gradeController)
        {
            _gradeController = gradeController;
            Unit .AddRange(gradeController.GetUnitById(1));
            _studentController = studentController;
            _dialog = dialog;
            RemoveStudent = new DelegateCommand(DeleteStudent);
            GetStudentCommand = new DelegateCommand<string>(GetStudent);
            AddUnit = new DelegateCommand(AddNewUnit);
            DialogToShowGradeCommand = new DelegateCommand(DialogToShowGrade);
            EnterCommand += keyDown;
        }
        private void DialogToShowGrade()
        {
            DialogParameters Para = new DialogParameters();
            Para.Add("StudentId", SelectedStu.StudentId);
            //arg是IDialogResult
            _dialog.ShowDialog("ShowGradeDialog", Para, arg =>
            {
                return;
            });
        }
        //获取参数
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("TeacherId")) { 
                TeacherId = navigationContext.Parameters.GetValue<int>("TeacherId");
            }
        }
        //是否重用原来的实例
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }
        //拦截导航
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
            if (MessageBox.Show("温馨提示", "确认导航", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                result = false;
            }
            continuationCallback(result);
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

    }
}
