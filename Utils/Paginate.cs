using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dimabeCRUD.Utils
{
    public class Paginate<T> : List<T>
    {
        public int IndexPage { get; private set; }
        public int TotalPages { get; private set; }

        public Paginate(List<T> items, int count, int indexPage, int pageSize)
        {
            IndexPage = indexPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (IndexPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (IndexPage < TotalPages);
            }
        }

        public static async Task<Paginate<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                    (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new Paginate<T>(items, count, pageIndex, pageSize);
        }
    }
}