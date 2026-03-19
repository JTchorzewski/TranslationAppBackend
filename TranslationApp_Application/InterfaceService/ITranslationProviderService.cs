using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.DTO;

namespace TranslationApp_Application.InterfaceService
{
    public interface ITranslationProviderService
    {
        Task<ProviderTranslationResultDto> TranslateAsync(string text, string translator);
    }
}
