using HtmlAgilityPack;

namespace warren_analysis_desk
{
    public class GoogleNewsExtractorService : IGoogleNewsExtractorService
    {
        private static readonly HttpClient client = new HttpClient();
        
        private readonly IRobotKeysRepository _robotKeysRepository;
        private readonly INewsRepository _newsRepository;

        public GoogleNewsExtractorService(IRobotKeysRepository robotKeysRepository, INewsRepository newsRepository)
        {
            _robotKeysRepository = robotKeysRepository;
            _newsRepository = newsRepository;
        }

        public async Task<List<News>> GetGoogleNews()
        {
            try
            {
                var newsList = new List<News>();
                var robotKeys = await _robotKeysRepository.GetAllAsync();

                foreach(var rks in robotKeys)
                {
                    foreach(var rk in rks.RobotKeysList)
                    {
                        HttpResponseMessage response = await client.GetAsync($"https://news.google.com/search?q={rk}%20when%3A1h&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");
                        response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();

                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(responseBody);

                        var firstLink = htmlDoc.DocumentNode
                            .Descendants("a")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("WwrzSb"));

                        var firstTitle = htmlDoc.DocumentNode
                            .Descendants("button")
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("VfPpkd-Bz112c-LgbsSe yHy1rc eT1oJ mN1ivc hUJSud"));                          

                        if (firstLink != null && firstTitle != null)
                        {
                            var hrefValue = firstLink.GetAttributeValue("href", string.Empty);
                            var titleValue = firstTitle.GetAttributeValue("aria-label", string.Empty);

                            var existingNews = await _newsRepository
                                .GetByTitleAsync(titleValue
                                    .Replace("Mais -", "").Trim()
                                    .Replace("; entenda", ""));
                            
                            var linkShortener = new LinkShortener();
                            string shortenedUrl = await linkShortener.ShortenUrl($"https://news.google.com{hrefValue.TrimStart('.')}");
                            
                            if (existingNews.Any())
                            {
                                var newsToUpdate = existingNews.FirstOrDefault();
                                if (newsToUpdate != null)
                                {
                                    newsToUpdate.EndExecution = DateTime.Now;  
                                    await _newsRepository.UpdateAsync(newsToUpdate); 
                                }
                            }
                            else
                            {
                                var newObj = new News
                                {
                                    Title = titleValue
                                        .Replace("Mais -", "").Trim()
                                        .Replace("; entenda", ""),
                                    Url = shortenedUrl,
                                    RobotName = rks.KeyName,
                                    PublishDate = DateTime.Now,
                                    StartExecution = DateTime.Now,
                                    EndExecution = DateTime.Now, 
                                    RobotKeysId = rks.Id ?? 0,
                                };

                                var insertedNews = await _newsRepository.AddAsync(newObj);  
                                newObj.Id = insertedNews.Id;
                                newsList.Add(newObj);
                            }
                        }
                    }
                }

                return newsList;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching news: {ex.Message}", ex);
            }
        }
    }
}