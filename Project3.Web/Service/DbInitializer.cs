using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project3.Web.Service.Interface;
using Project3.Data.Service.Interface;
using Microsoft.AspNetCore.Http;
using Project3.Data.Models;
using System.Diagnostics;
using Project3.Extensions;

namespace Project3.Web.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly Project3DbContext _project3Context;


        public DbInitializer(Project3DbContext _project3Context)
        {
            this._project3Context = _project3Context;

        }

        public void Initialize()
        {
            if (_project3Context.Database.GetPendingMigrations().Any())
            {
                //需要迁移
                _project3Context.Database.Migrate();
            }
            //初始化设置
            AddDefaultData();
        }



        private void AddDefaultData()
        {
            int optionscount = _project3Context.Options.Count();

            if (optionscount > 0)
            {
                return;
            }
            //添加管理员用户
            var adminuser = new Users();
            adminuser.username = "admin";
            adminuser.password = MD5Extension.PassEncrypt("admin");
            _project3Context.Users.Add(adminuser);

            //添加默认设置
            var data = new OptionsModel();
            Type t = data.GetType();
            foreach (var pi in t.GetProperties())
            {
                object value = pi.GetValue(data);
                string name = pi.Name;
                var option = new Options();
                option.name = name;
                option.value = value != null ? value.ToString() : "";
                _project3Context.Options.Add(option);

            }



            _project3Context.SaveChanges();


        }
    }
}
