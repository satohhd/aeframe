using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Controllers
{

    public abstract class ControllerPageBase<T> : ControllerBase
    {

        protected readonly ILogger<T> _logger;
        protected ApplicationDbContext _context;
        // protected IStringLocalizer<T> _localizer;
        public ControllerPageBase(ApplicationDbContext context, ILogger<T> logger)
        {
            //共通コントローラ内で処理
            _logger = logger;
            _context = context;
            // _localizer = localizer;
            // Console.WriteLine("SharedMethodController constractor");
            // Console.WriteLine(_localizer["test"]);
        }
    }
}
