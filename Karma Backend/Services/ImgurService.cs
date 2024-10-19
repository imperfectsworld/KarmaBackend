using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ImgurService
{
    private readonly HttpClient _httpClient;
    private const string ImgurApiUrl = "https://api.imgur.com/3/";
    private const string ClientId = "471792422f94881";
    private const string ClientSecret = "c7cb068411bc953ab8311e75c45b4855af7785d4";
    private const string AccessToken = "f409881ef1776edda8921812fc57ac97374380ca";

    public ImgurService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(ImgurApiUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
    }

    public async Task<string> GetImageAsync(string imageHash)
    {
        var response = await _httpClient.GetAsync($"image/{imageHash}");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }

    public async Task<string> GetAllImagesAsync()
    {
        var response = await _httpClient.GetAsync("account/me/images"); 
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync();  
        return responseBody;
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes, string imageTitle)
    {
        using (var content = new MultipartFormDataContent())
        {
            var imageContent = new ByteArrayContent(imageBytes);
            content.Add(imageContent, "image");
            content.Add(new StringContent(imageTitle), "title");

            var response = await _httpClient.PostAsync("upload", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }

    public async Task<string> RefreshAccessTokenAsync()
    {
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("refresh_token", "edecf6f4a24bb80a942ee96cf56a2a26e267d893"),
            new KeyValuePair<string, string>("client_id", ClientId),
            new KeyValuePair<string, string>("client_secret", ClientSecret),
            new KeyValuePair<string, string>("grant_type", "refresh_token")
        });

        var response = await _httpClient.PostAsync("oauth2/token", formData);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody;
    }
    public async Task<string> DeleteImageAsync(string imageHash)
    {
        var response = await _httpClient.DeleteAsync($"image/{imageHash}"); 
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync(); 
        return responseBody;  
    }

}
