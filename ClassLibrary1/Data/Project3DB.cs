using Microsoft.EntityFrameworkCore;
using Project3.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data.Data
{
    public class Project3DB
    {
        public DbSet<Contents> Contents { get { return Context.Set<Contents>(); } }
        public DbSet<Comments> Comments { get { return Context.Set<Comments>(); } }
        public DbSet<Options> Options { get { return Context.Set<Options>(); } }
        public DbSet<Metas> Metas { get { return Context.Set<Metas>(); } }
        public DbSet<Relationships> Relationships { get { return Context.Set<Relationships>(); } }
        public DbSet<Users> Users { get { return Context.Set<Users>(); } }
        public DbSet<Files> Files { get { return Context.Set<Files>(); } }

        private DbContext Context { get; }

        public Project3DB(DbContext Context)
        {
            this.Context = Context;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            Context.Add<T>(entity);
            await SaveChangesAsync();
            return entity;
        }


        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            Context.Update<T>(entity);
            await SaveChangesAsync();
            return entity;
        }


        public async Task DeleteAsync<T>(T entity) where T : class
        {
            Context.Remove<T>(entity);
            await SaveChangesAsync();

        }
        public async Task<int> SaveChangesAsync()
        {
             
          
            return await Context.SaveChangesAsync();

        }
    }
}
