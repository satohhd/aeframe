using System;
using System.Collections.Generic;
using System.ComponentModel;
using backend.Models;
using backend.ViewModels.Common;

namespace backend.ViewModels.Home
{

    public class Index : BaseViewModel
    {
        public Index()
        {
            SearchCondition = new SearchCondition();
            // Menu = new List<MenuViewModel>();
            // Notices = new List<NoticeViewModel>();
            // Fups = new List<FrequentlyUsePageViewModel>();
            
        }
        public SearchCondition SearchCondition { get; set; }

      
        // public List<MenuViewModel>  Menu { get; set; }
        public List<NaviViewModel>  NaviList { get; set; }
        // public List<NoticeViewModel>  Notices { get; set; }
        // public List<FrequentlyUsePageViewModel>  Fups { get; set; }
        public TodayAndWeatherViewModels Taw {get; set;}

    }

}

