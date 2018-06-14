using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Project3.Data.Data;
using Project3.Data.Models;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Service
{
    public class FilesManagerService : IFilesManagerService
    {
        private readonly Project3DB _project3DB;
        private readonly IHostingEnvironment hostingEnvironment;
        public FilesManagerService(Project3DB _project3DB, IHostingEnvironment hostingEnvironment)
        {
            this._project3DB = _project3DB;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<Files> AddAsync(Files file)
        {
            return await _project3DB.AddAsync(file);
        }


        public async Task<IList<Files>> GetFilesAsync()
        {
            return await _project3DB.Files.Where(m => m.cid == 0).OrderByDescending(m => m.cid).ToListAsync();
        }

        public async Task<IList<Files>> GetFilesByCidAsync(int cid)
        {
            return await _project3DB.Files.Where(m => m.cid == cid).OrderByDescending(m => m.cid).ToListAsync();
        }
        public async Task DeleteByFidAsync(int fid)
        {
            var file = await _project3DB.Files.Where(m => m.fid == fid).SingleOrDefaultAsync();
            if (file != null)
            {
                //删除本地文件
                DeleteFile(file.filepath);
                await _project3DB.DeleteAsync(file);

            }
        }

        public async Task<Files> GetFileByFidAsync(int fid)
        {

            return await _project3DB.Files.Where(m => m.fid == fid).SingleOrDefaultAsync();
        }

        public async Task DeleteAsync(Files file)
        {
            //删除本地文件
            DeleteFile(file.filepath);
            await _project3DB.DeleteAsync(file);
        }

        public async Task<int> UpdateByCidAsync(int cid, int[] files)
        {
            int i = 0;
            foreach (int fid in files)
            {
                var file = await _project3DB.Files.Where(m => m.fid == fid).SingleOrDefaultAsync();
                if (file != null)
                {
                    file.cid = cid;
                    await _project3DB.UpdateAsync(file);
                    i++;
                }
            }
            return i;
        }

        public async Task<PaginatedList<Files>> GetListAsync(int? pageindex, int pagesize)
        {
            var data = from s in _project3DB.Files select s;


            return await PaginatedList<Files>.CreateAsync(data.AsNoTracking(), pageindex ?? 1, pagesize);
        }

        public async Task<int> DeleteByFidAsync(int[] fid)
        {
            foreach (int id in fid)
            {
                var data = await _project3DB.Files.Where(m => m.fid == id).SingleOrDefaultAsync();
                if (data != null)
                {
                    _project3DB.Files.Remove(data);
                    //删除本地文件
                    DeleteFile(data.filepath);
                }
            }
            return await _project3DB.SaveChangesAsync();
        }

        private bool DeleteFile(string path)
        {
            //删除本地文件
            try
            {
                var filepath = hostingEnvironment.WebRootPath + path.Replace("/", "\\");

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                    return true;
                }

                
            }
            catch
            {
               
            }
            return false;
        }
    }
}
