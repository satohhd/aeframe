using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace backend.ViewModels.Home
{

    public class NaviViewModel
    {


        [DisplayName("画面名または機能名")]
        public string Name { get; set; }

        [DisplayName("アイコン")]
        public string Icon { get; set; }
        [DisplayName("リンク")]
        public string Link { get; set; }

        // public bool? ReadFlg { get; set; }
        // [DisplayName("保存")]
        // public bool? StoreFlg { get; set; }
        // [DisplayName("削除")]
        // public bool? DeleteFlg { get; set; }

        [DisplayName("説明")]
        public string Description { get; set; }

        [DisplayName("リンクリスト")]
        public IEnumerable<NaviViewModel> Lists { get; set; }

    }
}
