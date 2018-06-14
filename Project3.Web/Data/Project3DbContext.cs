
using Microsoft.EntityFrameworkCore;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Web.Data
{
    public class Project3DbContext : DbContext
    {
        public Project3DbContext(DbContextOptions<Project3DbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 内容
        /// </summary>
        public DbSet<Contents> Contents { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public DbSet<Comments> Comments { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public DbSet<Options> Options { get; set; }
        /// <summary>
        /// 分类/标签
        /// </summary>
        public DbSet<Metas> Metas { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        public DbSet<Relationships> Relationships { get; set; }
        /// <summary>
        /// 后台用户
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public DbSet<Files> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
