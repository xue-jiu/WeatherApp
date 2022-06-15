using LiveCharts;
using LiveCharts.Wpf;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Controllers;

namespace WeatherApp.ViewModels
{
    public class ShowGradeDialogViewModel : BindableBase, IDialogAware
    {
        public SeriesCollection SeriesCollection { get; set; }
        ChartValues<double> MathValue = new ChartValues<double>();
        ChartValues<double> EnglishValue = new ChartValues<double>();
        ChartValues<double> ChineseValue = new ChartValues<double>();
        ChartValues<double> PhyValue = new ChartValues<double>();
        List<int> Unit = new List<int>();
        List<string> columnValues = new List<string>();
        private int studentId = 1;
        public int StudentId
        {
            get { return studentId; }
            set 
            { 
                studentId = value;
                RaisePropertyChanged();
                InitalList();
            }
        }
        private void InitalList()
        {
            Unit.AddRange(_gradeController.GetUnitById(studentId));
            //for (int i = 0; i < Unit.Count; i++)
            //{
            //    columnValues[i] = "Unit" + Unit[i].ToString();
            //}
            MathValue.AddRange(_gradeController.GetMathById(studentId));
            EnglishValue.AddRange(_gradeController.GetEnglishById(studentId));
            ChineseValue.AddRange(_gradeController.GetChineseById(studentId));
            PhyValue.AddRange(_gradeController.GetPhysicalById(studentId));
        }

        private readonly GradeController _gradeController;
        public ShowGradeDialogViewModel(GradeController gradeController)
        {
            _gradeController = gradeController;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    
                    LineSmoothness=0,
                    Title="Math",
                    Values = MathValue,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 8
                },
                new LineSeries
                {
                    LineSmoothness=0,
                    Title="English",
                    Values =EnglishValue,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 8
                },
                 new LineSeries
                {
                    LineSmoothness=0,
                    Title="Chinese",
                    Values = ChineseValue,
                    PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize = 8
                },
                 new LineSeries
                {
                    LineSmoothness=0,
                    Title="Physical",
                    Values = PhyValue,
                    PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize = 8
                }
            };
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
            if (parameters.ContainsKey("StudentId"))
                StudentId = parameters.GetValue<int>("StudentId");
        }
    }
}
