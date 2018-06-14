using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
namespace Project3.Data.Service
{
    public class MetasManagerService : IMetasManagerService
    {
        private readonly Project3DB _project3DB;

        public MetasManagerService(Project3DB _project3DB)
        {
            this._project3DB = _project3DB;
        }



        public async Task<IList<Metas>> GetMetasAsync(int type)
        {
            if (type == -1)
            {
                return await _project3DB.Metas.ToListAsync();

            }
            return await _project3DB.Metas.Where(m => m.type == type).ToListAsync();
        }

        public async Task<PaginatedList<Metas>> GetListAsync(int type, int? pageindex, int pagesize)
        {
            var data = from s in _project3DB.Metas.Where(m => m.type == type) select s;


            return await PaginatedList<Metas>.CreateAsync(data.AsNoTracking(), pageindex ?? 1, pagesize);
        }
        public async Task<int> DeleteByMidAsync(int[] mid)
        {
            foreach (int id in mid)
            {
                var data = await _project3DB.Metas.Where(m => m.mid == id).SingleOrDefaultAsync();
                if (data != null)
                {
                    _project3DB.Metas.Remove(data);
                }
            }
            return await _project3DB.SaveChangesAsync();
        }

        public async Task<Metas> GetByMidAsync(int mid)
        {
            return await _project3DB.Metas.Where(m => m.mid == mid).SingleOrDefaultAsync();

        }

        public async Task<Metas> UpdateAsync(Metas meta)
        {
            return await _project3DB.UpdateAsync(meta);
        }

        public async Task<Metas> AddAsync(Metas meta)
        {
            return await _project3DB.AddAsync(meta);
        }
    }
}
