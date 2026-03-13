using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Models
{
    public class Repository<T> where T : class
    {
        protected BookstoreContext context { get; set; }
        internal DbSet<T> dbset { get; set; }

        public Repository(BookstoreContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        public IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            if (options.Where != null)
                query = query.Where(options.Where);

            if (!string.IsNullOrEmpty(options.Includes))
            {
                foreach (var include in options.Includes.Split(','))
                    query = query.Include(include.Trim());
            }

            if (options.OrderBy != null)
            {
                query = options.OrderByDirection == "desc"
                    ? query.OrderByDescending(options.OrderBy)
                    : query.OrderBy(options.OrderBy);
            }

            if (options.HasPaging)
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);

            return query.ToList();
        }

        public T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            if (options.Where != null)
                query = query.Where(options.Where);

            if (!string.IsNullOrEmpty(options.Includes))
            {
                foreach (var include in options.Includes.Split(','))
                    query = query.Include(include.Trim());
            }

            return query.FirstOrDefault();
        }

        public int Count => dbset.Count();
    }
}
