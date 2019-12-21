﻿using AutoMapper;
using VinarishCsb.Server.Data;
using VinarishCsb.Server.Middleware.Wrappers;
using VinarishCsb.Server.Models;
using VinarishCsb.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishCsb.Server.Services
{
    public interface IMessageService
    {
        Task<ApiResponse> Create(MessageDto messageDto);
        List<MessageDto> GetList();
        Task<ApiResponse> Delete(int id);
    }
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public MessageService(ApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<ApiResponse> Create(MessageDto messageDto)
        {
            Message message = _autoMapper.Map<MessageDto, Message>(messageDto);
            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();

            return new ApiResponse(200, "Created Message", message);
        }

        public async Task<ApiResponse> Delete(int id)
        {
            _db.Messages.Remove(new Message() { Id = id });
            await _db.SaveChangesAsync();

            return new ApiResponse(200, "Deleted Message", id);
        }

        public List<MessageDto> GetList()
        {
            return _autoMapper.ProjectTo<MessageDto>(_db.Messages).OrderBy(i => i.When).ToList();
        }
    }
}
