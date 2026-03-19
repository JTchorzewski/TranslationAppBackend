using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApp_Application.DTO
{
    public class LogItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string Translator { get; set; } = string.Empty;
        public string InputText { get; set; } = string.Empty;
        public string? OutputText { get; set; }
        public bool IsSuccess { get; set; }
        public int DurationMs { get; set; }
    }
}
