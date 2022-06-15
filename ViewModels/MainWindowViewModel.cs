using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using WeatherApp.Extensions;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    public class MainWindowViewModel : BindableBase, IConfigure
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private int nowTime = DateTime.Now.Day;
        public int NowTime
        {
            get { return nowTime; }
            set { nowTime = value; RaisePropertyChanged(); }
        }

        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialog;
        private IRegionNavigationJournal journal;
        public DelegateCommand GobackCommand { get; set; }
        public DelegateCommand GofowordCommand { get; set; }
        public DelegateCommand OpenLoginDialogCommand { get; set; }
        public DelegateCommand<string> NavigaCommand { get; private set; }//用于导航,接受一个委托为参数,若为泛型,则<>括号内表示委托需传入的参数类型
        public DelegateCommand OpenRegisterDialogCommand { get; set; }
        private bool buttonIsEable;
        public bool ButtonIsEable
        {
            get { return buttonIsEable; }
            set { buttonIsEable = value;
                RaisePropertyChanged();
            }
        }
        private int teacherId;
        public int TeacherId
        {
            get { return teacherId; }
            set 
            {//teacherId在数据库中是从1开始算的,故不可能为0
                if (value!=0)
                {
                    teacherId = value;
                    ButtonIsEable = true;
                }
                RaisePropertyChanged();
            }
        }
        private void Navigate(string obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj))
            {
                return;
            }
            if (obj == "StudentData")
            {
                NavigationParameters param  = new NavigationParameters();
                param.Add("TeacherId", TeacherId);
                _regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate(obj, back => {
                    journal = back.Context.NavigationService.Journal;
                },param);
            }
            else 
            { 
                _regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate(obj,back=> {
                journal = back.Context.NavigationService.Journal;} 
              );
            }
        }
        public void LoginDialog()
        {
            DialogParameters Para = new DialogParameters();
            Para.Add("value", "haha");
            //arg是IDialogResult
            _dialog.ShowDialog("LoginDialog", Para, arg =>
            {
                if (arg.Result == ButtonResult.OK)
                {
                    TeacherId = arg.Parameters.GetValue<int>("TeacherId");
                }
            });
            //_dialog.ShowDialog("LoginDialog");
        }
        public void RegisterDialog()
        {
            _dialog.ShowDialog("RegisterDialog");
        }
        //配置首页初始化参数
        public void Configure()
        {
            _regionManager.RegisterViewWithRegion(PrismManger.MainViewRegionName,typeof(Views.Index));
        }
        public MainWindowViewModel(IRegionManager regionManager,IDialogService dialog)
        {
            //区域管理
            _regionManager = regionManager;
            _dialog = dialog;
            ButtonIsEable = false;
            Configure();
            NavigaCommand = new DelegateCommand<string>(Navigate);
            OpenLoginDialogCommand = new DelegateCommand(LoginDialog);
            OpenRegisterDialogCommand = new DelegateCommand(RegisterDialog);
            GobackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
            GofowordCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
        }
    }
}
