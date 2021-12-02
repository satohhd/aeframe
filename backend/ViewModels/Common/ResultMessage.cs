using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using backend.Models;

namespace backend.ViewModels.Common
{
    public class ResultMessage
    {
        public ResultMessage()
        {
            OccurrenceDatetime = DateTime.Now;
        }
        public DateTime OccurrenceDatetime {get; set;}
        public string Level {get; set; }
        public string Title {get; set; }
        public string Text {get; set; }

        private string toString()
        {
            return OccurrenceDatetime.ToLongDateString() + ":" + Level + ":" + Title + ":" + Text;
        }
    }


}
