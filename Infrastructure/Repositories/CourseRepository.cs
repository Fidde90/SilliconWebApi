﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CourseRepository(DataContext dataContext) : BaseRepository<CourseEntity>(dataContext)
    {
        private readonly DataContext _dataContext = dataContext;

        public async override Task<IEnumerable<CourseEntity>> GetAll(string category = "", string searchValue = "")
        {
            try
            {
                var query = _dataContext.Courses.Include(c => c.Category).AsQueryable();

                if (!string.IsNullOrWhiteSpace(category) && category != "all")
                    query = query.Where(c => c.Category!.CategoryName == category);

                if (!string.IsNullOrWhiteSpace(searchValue) && searchValue != "all")
                    query = query.Where(x => x.Title.Contains(searchValue) || x.Author!.Contains(searchValue) || x.Price!.Contains(searchValue));

                query = query.OrderByDescending(o => o.LastUpdated);
                var courses = await query.ToListAsync(); 
                if (courses.Count > 0)
                    return courses;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async override Task<IEnumerable<CourseEntity>> GetAll()
        {
            try
            {
                var list = await _dataContext.Set<CourseEntity>().Include(e => e.Category).ToListAsync();
                if (list.Count > 0)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<List<CourseEntity>> GetAllByIds(List<int> ids)
        {
            try
            {
                var list = await _dataContext.Set<CourseEntity>().Where(x => ids.Contains(x.Id)).Include(e => e.Category).ToListAsync();
                if (list.Count > 0)
                    return list;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async override Task<CourseEntity> GetOne(Expression<Func<CourseEntity, bool>> predicate)
        {
            try
            {
                var entity = await _dataContext.Set<CourseEntity>().Include(c => c.Category).FirstOrDefaultAsync(predicate);
                if (entity != null)
                    return entity;
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
