using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Data;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserAsync(string userAuthId);

        Task<UserEntity?> GetUserIncludingGroupsAsync(string userAuthId);
        Task<UserEntity?> GetUserIncludingGroupsAndPlayersAsync(string userAuthId);

        Task<UserEntity?> GetUserIncludingGroupsAndPlayersAndExperienceAsync(string userAuthId);

        Task UpdateAsync(UserEntity userEntity);
        Task<UserEntity> CreateUserAsync(UserEntity user);
    }

    public class UserRepository(DatabaseContext dbContext) : IUserRepository
    {
        private readonly DatabaseContext dbContext = dbContext;

        public async Task<UserEntity?> GetUserAsync(string userAuthId)
        {
            UserEntity? userEntity = await GetUserQuery(userAuthId).FirstOrDefaultAsync();
            return userEntity;
        }

        public async Task<UserEntity?> GetUserIncludingGroupsAsync(string userAuthId)
        {
            UserEntity? userEntity = await GetUserQuery(userAuthId)
                .Include(u => u.Groups)
                .FirstOrDefaultAsync();
            return userEntity;
        }

        public async Task<UserEntity?> GetUserIncludingGroupsAndPlayersAsync(string userAuthId)
        {
            UserEntity? userEntity = await GetUserQuery(userAuthId)
                .Include(u => u.Groups)
                .ThenInclude(p => p.Players)
                .FirstOrDefaultAsync();
            return userEntity;
        }

        public async Task<UserEntity?> GetUserIncludingGroupsAndPlayersAndExperienceAsync(
            string userAuthId
        )
        {
            UserEntity? userEntity = await GetUserQuery(userAuthId)
                .Include(u => u.Groups)
                .ThenInclude(p => p.Players)
                .ThenInclude(p => p.PlayerExperiences)
                .FirstOrDefaultAsync();
            return userEntity;
        }

        public async Task UpdateAsync(UserEntity userEntity)
        {
            dbContext.Entry(userEntity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            var result = await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        private IQueryable<UserEntity> GetUserQuery(string userAuthId)
        {
            return dbContext
                .Users.Where(u => u.AuthProvider.AuthProviderUserId == userAuthId)
                .Include(u => u.AuthProvider);
        }
    }
}
