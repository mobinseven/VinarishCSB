﻿using AutoMapper;
using VinarishCsb.Server.Data;
using VinarishCsb.Server.Data.Interfaces;
using VinarishCsb.Server.Middleware.Wrappers;
using VinarishCsb.Server.Models;
using VinarishCsb.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishCsb.Server.Services
{
    public interface IApiLogService
    {
        Task Log(ApiLogItem apiLogItem);

        Task<ApiResponse> Get();

        Task<ApiResponse> GetByApplictionUserId(Guid applicationUserId);

        Task<ApiLogItem> GetLastGet(string path, Guid userId);
    }

    public class ApiLogService : IApiLogService
    {
        private readonly ApplicationDbContext _db;
        private readonly DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;
        private readonly IMapper _autoMapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSession _userSession;

        public ApiLogService(IConfiguration configuration, ApplicationDbContext db, IMapper autoMapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IUserSession userSession)
        {
            _db = db;
            _autoMapper = autoMapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;

            // Calling Log from the API Middlware results in a disposed ApplicationDBContext. This is here to build a DB Context for logging API Calls
            // If you have a better solution please let me know.
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            if (Convert.ToBoolean(configuration["VinarishCsb:UsePostgresServer"] ?? "false"))
            {
                //_optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            }
            else if (Convert.ToBoolean(configuration["VinarishCsb:UseSqlServer"] ?? "false"))
            {
                _optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); //SQL Server Database
            }
            else
            {
                //_optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("SqlLiteConnectionFileName")}");  // Sql Lite / file database
            }
        }

        public async Task Log(ApiLogItem apiLogItem)
        {
            if (apiLogItem.ApplicationUserId != Guid.Empty)
            {
                //TODO populate _userSession??

                //var currentUser = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                //UserSession userSession = new UserSession();
                //if (currentUser != null)
                //{
                //    userSession = new UserSession(currentUser.Result);
                //}
            }
            else
            {
                apiLogItem.ApplicationUserId = null;
            }

            using (ApplicationDbContext _dbContext = new ApplicationDbContext(_optionsBuilder.Options, _userSession))
            {
                _dbContext.ApiLogs.Add(apiLogItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ApiResponse> Get()
        {
            var logs = _autoMapper.ProjectTo<ApiLogItemDto>(_db.ApiLogs);
            var users = _db.Users.Select(u => new { u.Id, u.UserName });
            System.Collections.Generic.List<ApiLogItemDto> logsUserNames = new System.Collections.Generic.List<ApiLogItemDto>();
            foreach (var log in logs)
            {
                try
                {
                    log.UserName = users.Where(u => u.Id == log.ApplicationUserId).FirstOrDefault().UserName;
                    logsUserNames.Add(log);
                }
                catch { }
            }
            return new ApiResponse(200, "Retrieved Api Log", logsUserNames);
        }

        public async Task<ApiResponse> GetByApplictionUserId(Guid applicationUserId)
        {
            try
            {
                return new ApiResponse(200, "Retrieved Api Log", _autoMapper.ProjectTo<ApiLogItemDto>(_db.ApiLogs.Where(a => a.ApplicationUserId == applicationUserId)));
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }

        public async Task<ApiLogItem> GetLastGet(string path, Guid userId)
        {
            var item = await _db.ApiLogs
                .OrderByDescending(p => p.RequestTime)
                .FirstAsync(log => log.Path == path && log.ApplicationUserId == userId);
            return item;
            //if (item != null)
            //    return new ApiResponse(200, "Retrieved Old Value", item);
            //else
            //    return new ApiResponse(400, "Old Value was no requested");
        }
    }
}