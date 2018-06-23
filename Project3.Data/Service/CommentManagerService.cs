using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class CommentManagerService : ICommentManagerService
    {
        private readonly Project3DB _project3DB;

        public CommentManagerService(Project3DB _project3DB)
        {
            this._project3DB = _project3DB;
        }
        public async Task<PaginatedList<Comments>> GetPageListAsync(int? pageindex, int pagesize)
        {
            var data = from s in _project3DB.Comments orderby s.coid descending select s;


            return await PaginatedList<Comments>.CreateAsync(data.AsNoTracking(), pageindex ?? 1, pagesize);
        }
        public async Task<int> DeleteByCoidAsync(int[] coid)
        {
            foreach (int id in coid)
            {
                var comment = await _project3DB.Comments.Where(m => m.coid == id).SingleOrDefaultAsync();
                if (comment != null)
                {
                    //评论计数变更
                    var content = await _project3DB.Contents.Where(m => m.cid == comment.cid).SingleOrDefaultAsync();
                    if (content != null)
                    {
                        content.commentsnum--;
                        await _project3DB.UpdateAsync(content);
                    }
                    //删除评论
                    _project3DB.Comments.Remove(comment);

                }
            }
            return await _project3DB.SaveChangesAsync();
        }

        public async Task<IList<Comments>> GetListAsync(int cid, int parentcoid = 0)
        {
            var data = await _project3DB.Comments.
                Where(m => m.cid == cid && m.parentcoid == parentcoid).
                OrderByDescending(m => m.coid).
                //Select(m => new Comments() { coid = m.coid, cid = m.cid, parentcoid = m.parentcoid }).
                ToListAsync();
            return data;
        }

        public async Task<Comments> GetAsync(int coid)
        {
            var data = await _project3DB.Comments.
                 Where(m => m.coid == coid).
                 SingleOrDefaultAsync();
            return data;
        }

        public async Task AddAsync(Comments comment)
        {
            var content = await _project3DB.Contents.Where(m => m.cid == comment.cid && m.status == 0).SingleOrDefaultAsync();
            if (content != null)
            {
                //评论html代码转义
                comment.content = System.Web.HttpUtility.HtmlEncode(comment.content);

                comment.nickname = System.Web.HttpUtility.HtmlEncode(comment.nickname);
                //添加评论
                await _project3DB.AddAsync(comment);

                //计数
                content.commentsnum++;
                await _project3DB.UpdateAsync(content);

            }
        }

        public async Task<PaginatedList<CommentJsonModel>> GetAjaxPageListAsync(int cid, int? page, int pagesize, int parentcoid, int skip)
        {
             var content = await _project3DB.Contents.Where(m => m.cid == cid && m.status == 0).SingleOrDefaultAsync();
            if (content != null && content.allowcomment == 0)
            {

                var data = _project3DB.Comments.Where(m => m.cid == cid && m.parentcoid == parentcoid).
                    Select(m => new CommentJsonModel
                    {
                        cid = m.cid,
                        coid = m.coid,
                        createtime = m.createtime,
                        nickname = m.nickname,
                        reply = m.reply,
                        content = m.content,
                        headmd5= MD5Encrypt(m.mail)
                    }).
                    OrderByDescending(m => m.createtime);




                return await PaginatedList<CommentJsonModel>.CreateAsync(data.AsNoTracking(), page ?? 1, pagesize, skip);
            }

            return null;
        }
        private string MD5Encrypt(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        public async Task<int> CountAsync()
        {
            return await _project3DB.Comments.CountAsync();
        }
    }
}
