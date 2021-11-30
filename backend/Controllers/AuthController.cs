using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerPageBase<AuthController>
    {
        private readonly UserManager<Account> _userManager;
    public AuthController(ApplicationDbContext context ,ILogger<AuthController> logger,UserManager<Account> userManager) : base(context, logger)
    {
        //共通コントロール内で処理
        _userManager = userManager;
    }

    /**
* ログイン
* 認証に成功したら、トークンを返却する
*
* @return \Illuminate\Http\JsonResponse
*/
    // [AllowAnonymous]
    //     [HttpPost]
    //     public IActionResult login([FromBody] LoginModel param)
    //     {


    //         if (param == null)
    //         {
    //             param = new LoginModel();
    //             return BadRequest(param);
    //         }


    //         try
    //         {   //ログインユーザの取得
    //             var act = (from a in _context.Users
    //                         where a.Email.Equals(param.loginId)
    //                         where a.PasswordHash()
    //                         select a).FirstOrDefault();

    //             //該当する条件に一致しない
    //             if (act == null)
    //             {
    //                 param._message = _localizer["Your identity is unregistered."];
    //                 param.Authorized = false;
    //                 param.Applying = false;
    //                 param._errorRedirect = new VueMSFramework.Core.Event() {Page="auth",Section="create", Action="create/load",ParamItems= "email,termAddr,remoteAddr" };
    //                 return BadRequest(param);

    //             }
    //             //データが取得できた場合　中身を確認する
    //             else if (!act.EmailConfirmed)
    //             {
    //                 param._message = _localizer["Your identity is being confirmed."];
    //                 param.Authorized = false;
    //                 param.Applying = true;
    //                 return BadRequest(param);

    //             }
    //             //管理者確認待ち
    //             else if (!act.ManagerConfirmed)
    //             {

    //                 param._message = _localizer["The administrator is checking it."];
    //                 param.Authorized = false;
    //                 param.Applying = true;
    //                 return BadRequest(param);

    //             }
    //             //使用禁止
    //             else if (act.LockoutEnabled)
    //             {

    //                 param._message = _localizer["Your identity is locked."];
    //                 param.Authorized = false;
    //                 param.Applying = true;
    //                 return BadRequest(param);

    //             }
    //             else if (!param.Password.Equals(act.Password))
    //             {
    //                 param._message = _localizer["Pssword or Account bad."];
    //                 param.Authorized = false;
    //                 param.Applying = false;
    //                 return BadRequest(param);
    //             }
    //             //本人確認済みであること
    //             else if (act.EmailConfirmed && act.ManagerConfirmed)
    //             {
    //                 //
    //                 act.AccessCount += 1;
    //                 act.Updated = DateTime.Now;
    //                 act.Updater = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

    //                 _context.Accounts.Update(act);

    //                 _context.SaveChanges();

    //                 ReflectionUtility.Model2Model(act, param);

    //                 param.Authorized = true;
    //                 param.Applying = false;
    //                 return Ok(param);
    //             }
    //             else
    //             {
    //                 param._message = _localizer["unknown"];
    //                 param.Authorized = false;
    //                 param.Applying = false;
    //                 return BadRequest(param);
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
    //             param.Authorized = false;
    //             param.Applying = false;
    //             return BadRequest(param);
    //         }
    //         //POST受け取り
    //         string username = param.Username;
    //         string password = param.Password;

    //         //ダミー認証
    //         if (username == "hoge" && password == "password")
    //         {   
    //             //token生成（下の関数で）
    //             var tokenString = BuildToken();
    //             var response = new
    //             {
    //                 token = tokenString
    //             };

    //             return Ok(response);
    //         }
    //         else
    //         {
    //             return BadRequest();
    //         }

    //         $credentials = request(['email', 'password']);

    //         if (! $token = auth()->attempt($credentials)) {
    //             return response()->json(['error' => 'Unauthorized'], 401);
    //         }

    //         return $this->respondWithToken($token);
        //     }
        // GET: api/Auth
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetInfo()
        {   
            _logger.LogInformation("getInfo");
            return Ok();
        }
        [AllowAnonymous]
        // [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginParam param)
        {
            string id = param.Username;
            string pw = param.Password;
            // IActionResult response = Unauthorized();
            // var user = await _userManager.FindByNameAsync(id);
            // if (user != null && 
            //     await _userManager.CheckPasswordAsync(user, pw))
            // {
            //     var tokenString = BuildToken();
            //     response = Ok(new { token = tokenString });
            // }

            // ユーザを追加
            var user = new Account { UserName =id, Email = id };
            await _userManager.CreateAsync(user, pw);
           // _context.SaveChanges();

            // ユーザもAdmin権限を追加
            // var admin = _userManager.FindByEmailAsync("admin");
            // await _userManager.AddToRoleAsync(user,"admin");
            return Ok(param);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginParam param)
        {
            // _logger.LogInformation("login .......");
            // if (param)
            // {
            //     var user = _testUsers.FirstOrDefault(u => u.UserName == inputModel.UserName);
            //     if (user != null && user.Password == inputModel.Password)
            //     {
            //         var token = CreateJwtSecurityToken(user);
            //         return Ok(new TokenViewModel
            //         {
            //             Token = new JwtSecurityTokenHandler().WriteToken(token),
            //             Expiration = token.ValidTo,
            //         });
            //     }
            // }
            // return BadRequest();

            string id = param.Username;
            string pw = param.Password;

            // _logger.LogInformation("xxxxxxxxxxxxxxxxx");
            // _logger.LogInformation(id);
            // _logger.LogInformation(pw);

            IActionResult response = Unauthorized();
            // var user = await _userManager.FindByEmailAsync(id);
//            var user = await _userManager.FindByNameAsync(id);
            Account user = null;
            try{
                user = await _userManager.FindByEmailAsync(id);
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                _logger.LogError("DB接続でエラー:" + ex.Message);
            }
            // if (user == null){
            //     // user = await _userManager.FindByEmailAsync(id);
            // }
            if (user != null && 
                await _userManager.CheckPasswordAsync(user, pw))
            {
                // var tokenString = BuildToken(user, pw);
                // response = Ok(new { token = tokenString });

                var token = CreateJwtSecurityToken(user);
                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                });

            }
            // _logger.LogInformation("4444444");
        
            return response;
        }
 
    
        // [AllowAnonymous]
        // [HttpPost]
        // public IActionResult Login(
        //     [Required][FromBody]LoginParam param
        // )
        // {
        //     var handler = new JwtSecurityTokenHandler();
        //     // JWT内に入れるクレームです。
        //     var claims = new[] {
        //         new Claim(ClaimTypes.Name, loginParam.Username)
        //     };
        //     var subject = new ClaimsIdentity(claims);
        //     var credentials = new SigningCredentials(
        //         JwtSecurityConfiguration.SecurityKey,
        //         SecurityAlgorithms.HmacSha256);
        //     // ここでトークンを生成しています。
        //     var token = handler.CreateJwtSecurityToken(
        //         audience: JwtSecurityConfiguration.Audience,
        //         issuer: JwtSecurityConfiguration.Issuer,
        //         subject: subject,
        //         signingCredentials: credentials);
        //     var tokenText = handler.WriteToken(token);
        //     var result = new
        //     {
        //         token = tokenText
        //     };

        //     return Ok(result);
        // }
        /**
        * ユーザ情報の取得
        * 認証されたユーザの情報を返却する
        */
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {


            IActionResult response = Ok();
            try {
                Console.WriteLine(User.Identity.IsAuthenticated);
                Console.WriteLine(User.Identity.Name);
                //Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);

                var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
                var user = _context.Users.First(u => u.Id == id);
                Console.WriteLine(user);
                // return Ok(user);
                return Ok(
                new {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Color,
                    user.FirstChar,
                    _Message = "接続検証"
                });

            }catch(Exception ex){
                _logger.LogError(ex.Message);
            }
            return response;
            
        }
        // /**
        // * ログアウト
        // * ログアウトし、トークンを無効にする
        // *
        // * @return \Illuminate\Http\JsonResponse
        // */
        // public function logout()
        // {
        //     auth()->logout();

        //     return response()->json(['message' => 'Successfully logged out']);
        // }

        // /**
        // * トークンのリフレッシュ
        // * 古いトークンを新しいトークンでリフレッシュする
        // *
        // * @return \Illuminate\Http\JsonResponse
        // */
        // public function refresh()
        // {
        //     return $this->respondWithToken(auth()->refresh());
        // }

        // /**
        // * トークン返却
        // * トークンに関する情報を配列化してからJSON形式で返却する
        // *
        // * @param  string $token
        // *
        // * @return \Illuminate\Http\JsonResponse
        // */
        // protected function respondWithToken($token)
        // {
        //     return response()->json([
        //         'access_token' => $token,
        //         'token_type' => 'bearer',
        //         'expires_in' => auth()->factory()->getTTL() * 60
        //     ]);
        // }

        // public function register(Request $request) {
        //     $validator = Validator::make($request->all(), [
        //         'name' => 'required|string|max:100',
        //         'email' => 'required|email|max:255|unique:users',
        //         'password' => 'required|string|min:8|max:255|confirmed',
        //         'password_confirmation' => 'required|string|min:8|max:255',
        //     ]);

        //     if($validator->fails()) {
        //         return response()->json([
        //             'status' => 'error',
        //             'messages' => $validator->messages()
        //         ], 200);
        //     }

        //     $user = new User;
        //     $user->fill($request->all());
        //     $user->password = bcrypt($request->password);
        //     $user->save();

        //     return response()->json([
        //         'status' => 'success',
        //         'data' => $user
        //     ], 200);
        // }
                //token生成関数
        private string BuildToken(Account user, string password)
        {

           // authentication successful so generate jwt token
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.ASCII.GetBytes("12345678901234567890");
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new Claim[]
            //     {
            //         new Claim(ClaimTypes.Name, user.Id)
            //     }),
            //     Expires = DateTime.UtcNow.AddDays(7),
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };
            // var token = tokenHandler.CreateToken(tokenDescriptor);

            //user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            // user.Password = null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678901234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "hoge",
                audience: "hoge",
                expires: DateTime.Now.AddMinutes(360),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        JwtSecurityToken CreateJwtSecurityToken(Account user)
        {
            // JWT に含めるクレーム
            var claims = new List<Claim>()
            {
                // JwtBeaerAuthentication 用
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                // User.Identity プロパティ用
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678901234567890")),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return token;
        }
    }
}