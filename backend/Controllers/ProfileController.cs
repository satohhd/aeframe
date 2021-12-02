using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using backend.ViewModels.Home;
using backend.ViewModels.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerPageBase<ProfileController>
    {
        const string PageId = "profile";
        private readonly UserManager<Account> _userManager;
        public ProfileController(ApplicationDbContext context, ILogger<ProfileController> logger, UserManager<Account> userManager) : base(context, logger)
        {
            //共通コントローラ内で処理
            _userManager = userManager;
        }


        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(string id, ViewModels.Auth.AccountViewModel @account)
        {

            if (id != @account.Id)
            {
                return BadRequest(new Result()
                {
                    ResultMessage = new ResultMessage()
                    {
                        Level = "error",
                        Title = "データ不整合です",
                        Text = "最初からやり直してください"
                    }
                });
            }

            using (var tran = _context.Database.BeginTransaction())
            {

                try
                {
                    var user = await _context.Users.FindAsync(id);

                    user.UserName = @account.UserName;
                    user.Color = @account.Color;
                    user.FirstChar = @account.FirstChar;
                    // user.Password = @account.Password;

                    //var uesr = new Account(@account);
                    //_context.Entry(uesr).State = EntityState.Modified;
                    await _userManager.UpdateAsync(user);
                    if (!string.IsNullOrEmpty(@account.Password))
                    {
                        var result = await _userManager.ChangePasswordAsync(user, @account.OrgPassword, @account.Password);
                        if (!result.Succeeded)
                        {
                            await tran.RollbackAsync();
                            return BadRequest(new Result()
                            {
                                ResultMessage = new ResultMessage()
                                {
                                    Level = "error",
                                    Title = "パスワードを変更できませんでした。",
                                    Text = "エラー内容: 変更前パスワードが正しいか確認ください。"
                                }
                            });

                        }

                    }
                    // _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();
                    return Ok(new Result()
                    {
                        ResultMessage = new ResultMessage()
                        {
                            Level = "success",
                            Title = "保存しました。",
                            Text = "ユーザ名:　" + @account.UserName + "　処理日時: " + DateTime.Now.ToString("MM/dd HH:mm")
                        }
                    });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await tran.RollbackAsync();
                    _logger.LogError(ex.Message);
                    if (!AccountExists(id))
                    {
                        return NotFound(new Result()
                        {
                            ResultMessage = new ResultMessage()
                            {
                                Level = "error",
                                Title = "すでに削除されています",
                                Text = ""
                            }
                        });
                    }
                    else
                    {
                        return BadRequest(new Result()
                        {
                            ResultMessage = new ResultMessage()
                            {
                                Level = "error",
                                Title = "システムエラー",
                                Text = "エラー内容: " + ex.Message
                            }
                        });
                    }
                }
            }
            // return Ok(@account);
        }
        private bool AccountExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
