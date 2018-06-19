using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Data
{
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 内容条目总数
        /// </summary>
        public int TotalContents { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 剩余条目总数
        /// </summary>
        public int HasCount
        {
            get
            {
                if (PageIndex == TotalPages)
                {
                    return 0;
                }
                return TotalContents - (PageIndex * PageSize);
            }
        }

        /// <summary>
        /// 是否还有上页
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// 是否还有下页
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalContents = count;
            PageSize = pageSize;
            this.AddRange(items);
        }

        /// <summary>
        /// 创建分页数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, int skip = 0)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(((pageIndex - 1) * pageSize) + skip).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count - skip, pageIndex, pageSize);
        }

    }
}
