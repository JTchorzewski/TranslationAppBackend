using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Domain.Model;

namespace TranslationApp_Application.InterfaceRepository
{
    public interface ILogRepository
    {
        Task AddAsync(RequestLog log);
        Task<(IEnumerable<RequestLog> Items, int TotalCount)> GetLogsAsync(
            int page, int pageSize, string? translator, bool? isSuccess,
            DateTime? fromUtc, DateTime? toUtc, string? searchInput);
    }
}
