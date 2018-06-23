using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class OptionsManagerService : IOptionsManagerService
    {
        private readonly Project3DB _project3DB;
        public OptionsManagerService(Project3DB _project3DB)
        {
            this._project3DB = _project3DB;
        }



        public async Task<int> UpdateAsync(object data)
        {
            Type t = data.GetType();
            foreach (var pi in t.GetProperties())
            {
                object value = pi.GetValue(data);
                string name = pi.Name;
                var option = await _project3DB.Options.Where(m => m.name == name).SingleOrDefaultAsync();
                if (option != null)
                {
                    option.value = value.ToString();
                    _project3DB.Options.Update(option);
                }
                else if (IsHasOptionName(name))
                {
                    option = new Options();
                    option.name = name;
                    option.value = value.ToString();
                    _project3DB.Options.Add(option);
                }
            }
            return await _project3DB.SaveChangesAsync();
        }
        public async Task<OptionsModel> GetAsync()
        {
            var data = new OptionsModel();
            Type t = data.GetType();
            foreach (var pi in t.GetProperties())
            {

                string name = pi.Name;
                var option = await _project3DB.Options.Where(m => m.name == name).SingleOrDefaultAsync();
                if (option != null)
                {

                    pi.SetValue(data, option.value);
                }


            }

            return data;
        }

        public async Task<int> UpdateAsync(Dictionary<string, object> data)
        {
            foreach (var d in data)
            {
                var option = await _project3DB.Options.Where(m => m.name == d.Key).SingleOrDefaultAsync();
                if (option != null)
                {
                    option.value = d.Value.ToString();
                    _project3DB.Options.Update(option);
                }
                else if(IsHasOptionName(d.Key))
                {
                    option = new Options();
                    option.name = d.Key;
                    option.value = d.Value.ToString();
                    _project3DB.Options.Add(option);
                }
            }
            return await _project3DB.SaveChangesAsync();

        }

        public async Task<int> Count()
        {
            return await _project3DB.Options.CountAsync();
        }

        public async Task AddAsync(object data)
        {
            Type t = data.GetType();
            foreach (var pi in t.GetProperties())
            {
                object value = pi.GetValue(data);
                string name = pi.Name;
                var option = new Options();
                option.name = name;
                option.value = value != null ? value.ToString() : "";

                _project3DB.Options.Add(option);
            }
            await _project3DB.SaveChangesAsync();
        }

        #region 扩展方法
        /// <summary>
        /// 是否存在配置项名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsHasOptionName(string name)
        {
            var data = new OptionsModel();
            Type t = data.GetType();
            foreach (var pi in t.GetProperties())
            {

                if (pi.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }

}
