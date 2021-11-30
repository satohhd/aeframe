using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{

    public class Account : IdentityUser
    {
        public Account()
        {
        }
        public Account(Account param)
        {
            Id = param.Id;
            UserName = param.UserName;
            NormalizedUserName = param.Email.ToUpper();  
            Email = param.Email;
            NormalizedEmail = param.Email.ToUpper();
            Password = param.Password;
            FirstChar = param.FirstChar;
            Color = param.Color;
            ActiveFlg = param.ActiveFlg;
            DisplayOrder = param.DisplayOrder;

        }
        // // Monthプロパティ
        // public string FirstChar { 
        //     get { return UserName ? UserName.Substring(0,1) : ""; }
        // }
        // [DisplayName("ID")]
        // public string Id { get; set; }
        // [DisplayName("名前")]
        // public string Name { get; set; }
        // [DisplayName("ログインID")]
        // public string Email { get; set; }
        // [DisplayName("パスワード")]
        // public string Password { get; set; }
        [DisplayName("パスワード")]
        public string Password { get; set; }
        [DisplayName("１文字")]
        public string FirstChar { get; set; }

        [DisplayName("色")]
        [StringLength(10)]
        // [Required]   
        public string Color { get; set; }
        [DisplayName("有効フラグ")]
        public bool ActiveFlg { get; set; }
        [DisplayName("表示順")]
        public int DisplayOrder { get; set; }


    }
}
