using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Extensions;
using Project3.Web.Areas.Admin.Filters;

namespace Project3.Web.Areas.Admin.Controllers
{

    public class DefaultController : BaseController
    {
        private readonly IUsersManagerService ums;
        private readonly IOptionsCache optionsCache;
        private readonly IPageCache pageCache;
        public DefaultController(IUsersManagerService ums, IPageCache pageCache, IOptionsCache optionsCache)
        {
            this.ums = ums;
            this.pageCache = pageCache;
            this.optionsCache = optionsCache;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [Alert]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LoginPost(Users user)
        {

            int ExpiresUtc = Request.Form["remeber"].ToString() == "on" ? 7 : 1;
            //验证模型的数据验证是否通过
            if (ModelState.IsValid)
            {
                var pwd = MD5Extension.PassEncrypt(user.password);
                var User = await ums.GetUserByUserNameAsync(user.username);
                if (User != null && User.password == pwd)
                {
                    //添加cookies
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaim(new Claim(ClaimTypes.Name, user.username));
                    //设置有效期
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                    {
                        // 持久保存
                        IsPersistent = true,

                        ExpiresUtc = DateTime.UtcNow.AddDays(ExpiresUtc)
                    });
                    Alert.Content = "欢迎回来";
                    Alert.Level = Models.AlertModel.AlertType.Success;
                    return RedirectToAction(nameof(Index));

                }

            }
            Alert.Content = "登录失败，请检查登录名/密码";
            Alert.Level = Models.AlertModel.AlertType.Danger;
            return RedirectToAction(nameof(Login));
        }

        public IActionResult LoginOut()
        {
            //删除cookies
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> UserSetting()
        {
            var user = await ums.GetUserByUserNameAsync(User.Identity.Name);
            return View(user);
        }

        [Alert]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UserSettingPost(Users postuser, string repassword, string checkpassword)
        {
            Alert.Level = Models.AlertModel.AlertType.Danger;
            Alert.Content = "更新设置失败";


            var users = await ums.GetUserByUserNameAsync(User.Identity.Name);
            if (users == null)
            {
                Alert.Content = "身份验证失败";

            }
            else if (string.IsNullOrEmpty(checkpassword))
            {

                Alert.Content = "请输入当前密码";

            }
            else if (postuser.password != repassword)
            {
                Alert.Content = "两次密码输入不一致";
            }
            else if (users.password != MD5Extension.PassEncrypt(checkpassword))
            {
                Alert.Content = "验证当前密码失败";
            }
            else
            {
                users.username = postuser.username;
                if (!string.IsNullOrEmpty(repassword))
                {
                    users.password = MD5Extension.PassEncrypt(postuser.password);
                }
                await ums.UpdateAsync(users);
                Alert.Content = "用户设置已更新";
                Alert.Level = Models.AlertModel.AlertType.Success;
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.Name, postuser.username));
                //设置有效期
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            }
            return RedirectToAction(nameof(UserSetting));
        }

        [Alert]
        public async Task<IActionResult> RefCache()
        {
            Alert.Content = "站点缓存已更新";
            Alert.Level = Models.AlertModel.AlertType.Success;
            await pageCache.Refresh();
            await optionsCache.Refresh();

            return RedirectToAction(nameof(Index));

        }
    }
}