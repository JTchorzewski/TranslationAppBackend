using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TranslationApp_Domain.Model
{
    public class RequestLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Translator { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string InputText { get; set; } = string.Empty;

        public string? OutputText { get; set; }

        public int? ProviderStatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public int DurationMs { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
