using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace backend.ViewModels.Auth
{

    public class AccountViewModel : Models.Account
    {
        public AccountViewModel()
        {
        }
        public AccountViewModel(Models.Account param)
        {

            Id = param.Id;
            UserName = param.UserName;
            Email = param.Email;
            FirstChar = param.FirstChar;
            Color = param.Color;
            ActiveFlg = param.ActiveFlg;
            DisplayOrder = param.DisplayOrder;

        }

        [DisplayName("変更前パスワード")]
        public string OrgPassword { get; set; }

    }
}
