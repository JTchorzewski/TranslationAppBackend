using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Domain.Model;

namespace TranslationApp_Infrastructure.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext _dataContext;

        public LogRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(RequestLog log)
        {
            await _dataContext.TranslationLogs.AddAsync(log);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<RequestLog> Items, int TotalCount)> GetLogsAsync(
            int page, int pageSize, string? translator, bool? isSuccess, DateTime? fromUtc, DateTime? toUtc, string? searchInput)
        {
            var query = _dataContext.TranslationLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(translator))
                query = query.Where(x => x.Translator.ToLower() == translator.ToLower());

            if (isSuccess.HasValue)
                query = query.Where(x => x.IsSuccess == isSuccess.Value);

            if (fromUtc.HasValue)
                query = query.Where(x => x.CreatedAtUtc >= fromUtc.Value);

            if (toUtc.HasValue)
                query = query.Where(x => x.CreatedAtUtc <= toUtc.Value);

            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                var searchLower = searchInput.ToLower();
                query = query.Where(x => x.InputText.ToLower().Contains(searchLower));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
