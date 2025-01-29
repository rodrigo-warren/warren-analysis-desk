using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk
{
    [Authorize]
    [Tags("News")]
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var news = await _newsService.GetByIdAsync(id);

                return Ok(news);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while fetching the news.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the news.", details = ex.Message });
            }
        }

        [HttpGet("search-by/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            try
            {
                var news = await _newsService.GetByTitleAsync(title);

                return Ok(news);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while fetching the news.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the news.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var newsList = await _newsService.GetAllAsync();
                return Ok(newsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all news.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewsDto newsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var addedNews = await _newsService.AddAsync(newsDto);
                return CreatedAtAction(nameof(GetById), new { id = addedNews.Id }, addedNews);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while adding the news.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the news.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NewsDto newsDto)
        {
            try
            {
                await _newsService.UpdateAsync(id, newsDto);
                return Ok(new { message = "News updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while updating the news.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the news.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _newsService.DeleteAsync(id);
                return Ok(new { message = "News deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while deleting the news.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the news.", details = ex.Message });
            }
        }
    }
}
