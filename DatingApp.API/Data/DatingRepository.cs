﻿using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entiry) where T : class
        {
            _context.Remove(entiry);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u=> u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
           var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
           return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var user = _context.Users.Include(p => p.Photos).OrderByDescending(u=> u.LastActive).AsQueryable();
            user = user.Where(u => u.Id != userParams.UserId);
            user = user.Where(u => u.Gender == userParams.Gender);
            if(userParams.MinAge !=18 || userParams.MaxAge !=99)
            {
                var minDOB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDOB = DateTime.Today.AddYears(-userParams.MinAge);
                user = user.Where(u => u.DateOfBirth >= minDOB && u.DateOfBirth <= maxDOB);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        user = user.OrderByDescending(u => u.Created);
                        break;
                    default:
                        user = user.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(user,userParams.PageNumber,userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
