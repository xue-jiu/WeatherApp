using AutoMapper;
using Prism.Ioc;
using System.Windows;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using WeatherApp.Views;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //弹窗部分
            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>();
            containerRegistry.RegisterDialog<RegisterDialog, RegisterDialogViewModel>();
            containerRegistry.RegisterDialog<AddStudent, AddStudentViewModel>();
            containerRegistry.RegisterDialog<ShowGradeDialog, ShowGradeDialogViewModel>();
            //区域管理部分
            containerRegistry.RegisterForNavigation<Index, IndexViewModel>();
            containerRegistry.RegisterForNavigation<StudentData, StudentDataViewModel>();
            containerRegistry.RegisterForNavigation<MyStudent, MyStudentViewModel>();
            //数据仓库接口
            containerRegistry.Register<IStuRepository, StudentRepository>();
            containerRegistry.Register<ITeaRepository, TeaRepository>();
            containerRegistry.Register<IGradeaRepository, GradeRepository>();
            //数据库依赖注入
            containerRegistry.Register<SchoolDbcontext>();
            //控制器注入
            containerRegistry.Register<StudentController>();
            containerRegistry.Register<GradeController>();
            containerRegistry.Register<TeacherController>();
            //注册AutoMapper
            containerRegistry.RegisterSingleton<IAutoMapperProvider, AutoMapperProvider>();
            containerRegistry.Register(typeof(IMapper), GetMapper);
        }
        private IMapper GetMapper(IContainerProvider container)
        {
            var provider = container.Resolve<IAutoMapperProvider>();
            return provider.GetMapper();
        }
    }
}

