using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using AivenEcommerce.V1.Modules.ImgBB.Dto;
using AivenEcommerce.V1.Modules.ImgBB.Options;

namespace AivenEcommerce.V1.Modules.ImgBB.Services
{
    public class ImgBbService : IImgBbService
    {
        private readonly HttpClient _httpClient;
        private readonly IImgBbOptions _options;

        public ImgBbService(HttpClient httpClient, IImgBbOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<ImgBbResponse> UploadImage(byte[] image)
        {
            string fileBase64 = Convert.ToBase64String(image);
            // act on the Base64 data

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("image", fileBase64)
            });

            string uri = Path.Combine(_options.BaseAddress, $"1/upload?key={_options.ApiKey}");

            HttpResponseMessage responseHttp = await _httpClient.PostAsync(uri.ToString(), formContent);
            ImgBbResponse response = await responseHttp.Content.ReadFromJsonAsync<ImgBbResponse>();

            return response;

        }
    }
}
