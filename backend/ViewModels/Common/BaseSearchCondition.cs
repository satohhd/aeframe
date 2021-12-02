using System.Collections.Generic;
using System.ComponentModel;
using backend.Models;

namespace backend.ViewModels.Common
{

    public abstract class BaseSearchCondition
    {
        // [DisplayName("ページネーション")]
        // public Pagination Pagination {get; set; }
        public int Rows { get; set; } = 0;
        public int PerPage { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;

        //URLのユニーク化のため
        public string Timestamp {get; set;}

        //Pagepath
        public string PageId {get; set;}

    }

}