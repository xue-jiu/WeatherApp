using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WeatherApp.Extensions
{
    //想法作废,想用datagridrow触发双击事件,发现并没有这个事件
    public class DatagridBehavior : Behavior<DataGrid>
    {
        public Action<string> MyCommand
        {
            get { return (Action<string>)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCommandProperty =
            DependencyProperty.Register("MyCommand", typeof(Action<string>), typeof(DatagridBehavior));
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }
        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (e.Key == Key.Enter)
            {
                MyCommand.Invoke("enter");
            }
        }
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }
    }

}
