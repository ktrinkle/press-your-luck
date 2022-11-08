namespace PressYourLuckApi.Controllers;

[ApiController]
[Route("[api/pyl]")]
public class PressYourLuckController : ControllerBase
{
    private readonly ILogger<EventManageController> _logger;
    private readonly IManageEventService _manageEventService;

    public PressYourLuckController(ILogger<EventManageController> logger, IManageEventService manageEventService)
    {
        _logger = logger;
        _manageEventService = manageEventService;
    }

    [Authorize]
    [HttpPost("updateAnswer")]
    [SwaggerOperation(Summary = "Update the answers from a team.")]
    public async Task<ActionResult<QuestionAnswerDto>> UpdateAnswerAsync(QuestionAnswerDto teamAnswer)
        => Ok(await _manageEventService.UpdateAnswerAsync(teamAnswer));

    [Authorize]
    [HttpPost("updateBigBoard")]
    [SwaggerOperation(Summary = "Update the scores from the Big Board.")]
    public async Task<ActionResult<QuestionAnswerDto>> UpdateBoardStopAsync(CurrentStatusDto boardAnswer)
        => Ok(await _manageEventService.UpdateBoardStopAsync(boardAnswer));

}