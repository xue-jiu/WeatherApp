using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class RegisterDialogViewModel : IDialogAware
    {
        public Teacher teacher { get; set; } = new Teacher();
        public RegisterDialogViewModel()
        {
            teacher.Name = "hah";
        }
        public int Register(Teacher teacher)
        {
            return 1;
        }
        public string Title { get; set; }
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
        }
    }
}
