using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Project3.Data.Enums;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using Project3.Web.Attributes;
using Project3.Web.Filters;
using Project3.Web.Models.ContentVM;

namespace Project3.Web.Controllers
{
    /// <summary>
    /// 内容显示（文章/页面）
    /// </summary>
    public class ContentController : BaseController
    {

        private readonly IContentManagerService cms;
        private readonly ICommentManagerService coms;
        private readonly IPageCache pageCache;
        private readonly IOptionsCache optionsCache;
        private readonly IActionCooldownService _actioncd;
        public ContentController(IContentManagerService cms, ICommentManagerService coms, IPageCache pageCache, IOptionsCache optionsCache
            , IActionCooldownService _actioncd)
        {
            this.cms = cms;
            this.coms = coms;
            this.pageCache = pageCache;
            this.optionsCache = optionsCache;
            this._actioncd = _actioncd;
        }
        #region 文章
        [Theme]
        public async Task<IActionResult> Article(int cid)
        {

            var VM = new ArticleViewModel();
            VM.content = await cms.GetByCidAsync(cid, false);
            if (VM.content == null)
            {

                Message.Show(new Widget.MessageModel()
                {
                    Content = "内容似乎已经丢失，请返回首页。"
                });
            }

            VM.metas = await cms.GetMetasAsync(cid);

            ViewData.Model = VM;

            await cms.UpdateReadnumAsync(cid, 1);


            return View();
        }
        #endregion

        #region 页面
        [Theme]
        public async Task<IActionResult> Page(int cid)
        {
            var VM = new ArticleViewModel();
            VM.content = await pageCache.GetByCid(cid);
            if (VM.content == null)
            {
                Message.Show(new Widget.MessageModel()
                {
                    Content = "页面似乎已经丢失，请返回首页。"
                });
            }

            ViewData.Model = VM;


            return View();
        }
        #endregion


        #region 评论提交
        [ActionCooldownFilter(ActionCooldownType.CommentsPost)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment_Post(int cid, int type, int parentcoid, string nickname, string email, string content)
        {
            
            var options = await optionsCache.Get();
            if (options.allowcomments == "1")
            {
                Message.Show(new Widget.MessageModel()
                {
                    Content = "站点评论功能已关闭，请返回。"
                });
            }
            var contentdata = await cms.GetByCidAsync(cid, false);
            if (contentdata == null)
            {
                Message.Show(new Widget.MessageModel()
                {
                    Content = "评论的内容似乎已经丢失，请返回。"
                });

            }
            else if (contentdata.allowcomment == 1)
            {
                Message.Show(new Widget.MessageModel()
                {
                    Content = "内容评论功能已关闭，请返回。"
                });
            }


            var comment = new Comments();
            comment.cid = cid;
            comment.nickname = nickname;
            comment.mail = email;
            comment.content = content;
            comment.createtime = DateTime.Now;
            comment.parentcoid = 0;


            if (parentcoid > 0)
            {
                var parentcomment = await coms.GetAsync(parentcoid);
                if (parentcomment == null)
                {
                    Message.Show(new Widget.MessageModel()
                    {
                        Content = "回复的评论已被删除，请返回重新提交。"
                    });
                 
                }
                else
                {
                    comment.parentcoid = parentcomment.parentcoid == 0 ? parentcoid : parentcomment.parentcoid;
                    comment.reply = parentcomment.nickname;
                }
            }
            if (TryValidateModel(comment))
            {
                //设置冷却时间
                _actioncd.SetCooldown(ActionCooldownType.CommentsPost, RemoteIP);
                await coms.AddAsync(comment);
                
            }
            else
            {
                Message.Show(new Widget.MessageModel()
                {
                    Content = "输入有误，请返回重新提交。"
                });
            
            }
            return RedirectToAction(type == 0 ? nameof(Article) : nameof(Page), new { cid });



        }
        #endregion

    }

}