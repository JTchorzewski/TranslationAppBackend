using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceService;

namespace TranslationApp_Application.Service
{
    public class FunTranslationService : ITranslationProviderService
    {
        private readonly HttpClient _httpClient;

        public FunTranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
        }

        public async Task<ProviderTranslationResultDto> TranslateAsync(string text, string translator)
        {
            var result = new ProviderTranslationResultDto();

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("text", text)
                });

                var response = await _httpClient.PostAsync($"{translator}.json", content);
                result.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(responseString);

                    result.TranslatedText = jsonDoc.RootElement
                        .GetProperty("contents")
                        .GetProperty("translated")
                        .GetString();

                    result.IsSuccess = true;
                }
                else if (result.StatusCode == 429)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Rate limit exceeded (FunTranslations allows 5 requests per hour).";
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"Provider error: {result.StatusCode}";
                }
            }
            catch (TaskCanceledException)
            {
                result.IsSuccess = false;
                result.StatusCode = 408;
                result.ErrorMessage = "Request timed out.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.StatusCode = 500;
                result.ErrorMessage = $"Unexpected error: {ex.Message}";
            }

            return result;
        }
    }
}
