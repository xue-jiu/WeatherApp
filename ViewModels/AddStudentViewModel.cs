using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;
using WeatherApp.Dtos;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    public class AddStudentViewModel : BindableBase,IDialogAware
    {
        public DelegateCommand<StudentCreateDto> CreateStuCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ReturnCommand { get; set; }

        //返回学生数据界面
        private void ReturnToStudentData()
        {
            RequestClose?.Invoke(new DialogResult());
        }
        //传参返回注册好的学生
        private void SaveAndReturnStuDto() 
        {
            StudentCreateDto student = new StudentCreateDto { Address = AddressFromUi, Telephone = TelephoneFromUi, Name = nameFromUi };
            StudentDto studentDto= _studentController.CreateStudent(student);
            DialogParameters param = new DialogParameters();
            param.Add("student", studentDto);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
        }
        //构造函数
        private readonly StudentController _studentController;
        public AddStudentViewModel(StudentController studentController)
        {
            _studentController = studentController;
            SaveCommand = new DelegateCommand(SaveAndReturnStuDto);
            ReturnCommand = new DelegateCommand(ReturnToStudentData);
        }

        private string nameFromUi;
        public string NameFromUi
        {
            get { return nameFromUi; }
            set { nameFromUi = value; RaisePropertyChanged(); }
        }

        private string telephoneFromUi;
        public string TelephoneFromUi
        {
            get { return telephoneFromUi; }
            set { telephoneFromUi = value; RaisePropertyChanged(); }
        }

        private string addressFromUi;
        public string AddressFromUi
        {
            get { return addressFromUi; }
            set { addressFromUi = value; RaisePropertyChanged(); }
        }

        public string Title { get; set;}
        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog()
        {
            return true;
        }
        public void OnDialogClosed()
        {
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
        }
    }
}
