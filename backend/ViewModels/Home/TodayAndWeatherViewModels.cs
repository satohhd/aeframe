using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace backend.ViewModels.Home
{

    public class TodayAndWeatherViewModels
    {

        public string Today { get; set; }
        public string Yyyy { get; set; }
        public string Mmdd { get; set; }
        public string Week { get; set; }
        public string Time { get; set; }
        public int TemperatureC { get; set; }
        public int Humidity { get; set; }
        public int Discomfort { get; set; }

        public string Weather { get; set; }
        public string Image { get; set; }

    }
}
