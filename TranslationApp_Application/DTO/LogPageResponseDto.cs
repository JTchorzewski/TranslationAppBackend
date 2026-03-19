using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApp_Application.DTO
{
    public class LogPageResponseDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<LogItemDto> Items { get; set; } = new List<LogItemDto>();
    }
}
