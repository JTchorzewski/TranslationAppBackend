using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.DTO;

namespace TranslationApp_Application.InterfaceService
{
    public interface ILogService
    {
        Task<LogPageResponseDto> GetLogsAsync(
            int page, int pageSize, string? translator, bool? isSuccess,
            DateTime? fromUtc, DateTime? toUtc, string? searchInput);
    }
}
