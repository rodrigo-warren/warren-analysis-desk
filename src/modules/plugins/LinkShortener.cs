public class LinkShortener
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> ShortenUrl(string url)
    {
        try
        {
            HttpResponseMessage response = await client
                .GetAsync($"https://tinyurl.com/api-create.php?url={url}");

            return await response.EnsureSuccessStatusCode()
                .Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao encurtar URL: {ex.Message}");
            return null;
        }
    }
}