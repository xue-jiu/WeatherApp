using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using MaterialDesignThemes.Wpf;
using WeatherApp.Controllers;
using Prism.Mvvm;

namespace WeatherApp.ViewModels
{
    public class LoginDialogViewModel : BindableBase,IDialogAware
    {
        public string Title { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public Teacher teacher { get; set; }
        public string Name { get; set; } = "江强";
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value;
                RaisePropertyChanged();
            }
        }
        TeacherController _teacherController;
        public LoginDialogViewModel(TeacherController teacherController)
        {
            _teacherController = teacherController;
            Password = "123";
            SaveCommand = new DelegateCommand(() =>
            {
                if (_teacherController.LoginTea(Name, Password))
                {
                    teacher = _teacherController.FindTeacherName(Name);
                    DialogParameters param = new DialogParameters();
                    param.Add("TeacherId", teacher.TeacherId);
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
                }
            });
            CancelCommand = new DelegateCommand(()=> {
                RequestClose?.Invoke(new DialogResult());
            });
        }
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
           if(parameters.ContainsKey("value"))
                Title=parameters.GetValue<string>("value");
        }
    }
}
