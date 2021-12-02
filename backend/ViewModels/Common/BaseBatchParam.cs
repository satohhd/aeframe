using System.Collections.Generic;
using System.ComponentModel;
using backend.Models;

namespace backend.ViewModels.Common
{

    public abstract class BaseBatchParam
    {
    
        //URLのユニーク化のため
        public string Timestamp {get; set;}

    }

}