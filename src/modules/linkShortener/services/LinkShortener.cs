public class LinkShortener
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> ShortenUrl(string url)
    {
        try
        {
            string tinyUrlRequest = $"https://tinyurl.com/api-create.php?url={url}";
            HttpResponseMessage response = await client.GetAsync(tinyUrlRequest);
            response.EnsureSuccessStatusCode();
            string shortenedUrl = await response.Content.ReadAsStringAsync();
            return shortenedUrl;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao encurtar URL: {ex.Message}");
            return null;
        }
    }
}
