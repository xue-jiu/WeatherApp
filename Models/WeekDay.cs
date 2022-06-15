using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeekDay:BindableBase
    {
        public string Name { get; set; }
        public string Task { get; set; }
    }
}
