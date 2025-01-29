using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace warren_analysis_desk 
{
    [Authorize]
    [ApiController]
    [Tags("Robot Keys")]
    [Route("api/robot-keys")]
    public class RobotKeysController : ControllerBase
    {
        private readonly IRobotKeysService _robotKeysService;

        public RobotKeysController(IRobotKeysService robotKeysService)
        {
            _robotKeysService = robotKeysService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var robotKeys = await _robotKeysService.GetByIdAsync(id);

                return Ok(robotKeys);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while fetching the robot keys.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the robot keys.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var robotKeyss = await _robotKeysService.GetAllAsync();
                return Ok(robotKeyss);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching all robot keys.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(RobotKeysDto robotKeysDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var addedRobotKeys = await _robotKeysService.AddAsync(robotKeysDto);
                return CreatedAtAction(nameof(GetById), new { id = addedRobotKeys.Id }, addedRobotKeys);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while adding the robot keys.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the robot keys.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RobotKeysDto robotKeysDto)
        {
            try 
            {
                await _robotKeysService.UpdateAsync(id, robotKeysDto);
                return Ok(new { message = "Robot keys updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = "An error occurred while updating the robot keys.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the robot keys.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _robotKeysService.DeleteAsync(id);
                return Ok(new { message = "Robot Keys deleted successfully" });
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