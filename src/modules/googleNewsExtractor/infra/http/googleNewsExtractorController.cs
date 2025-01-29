using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk
{
    [Authorize]
    [ApiController]
    [Tags("Google News Extractor")]
    [Route("api/google-news-extractor")]
    public class GoogleNewsExtractorController : ControllerBase
    {
        private readonly IGoogleNewsExtractorService _googleNewsExtractorService;

        public GoogleNewsExtractorController(IGoogleNewsExtractorService googleNewsExtractorService)
        {
            _googleNewsExtractorService = googleNewsExtractorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoogleNews()
        {
            try
            {
                var newsList = await _googleNewsExtractorService.GetGoogleNews();
                return Ok(newsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all news.", details = ex.Message });
            }
        }
    }
}
