using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.ViewModels.Common
{
    public abstract class BaseViewModel
    {
       
        //System control item
        public string _message { get; set; }

        // public Pagination pagination {get; set;}

        public ResultMessage ResultMessage {get; set;}

        // public Permit Permit { get; set; }

        //共通項目
        // public string CreatedUserId { get; set; }
        // public DateTime? CreatedAt { get; set; }
        // public string UpdatedUserId { get; set; }
        // public DateTime? UpdatedAt { get; set; }
        // public string CreatedPageId { get; set; }
        // public string UpdatedPageId { get; set; }

    }

}
