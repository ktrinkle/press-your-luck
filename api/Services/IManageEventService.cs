namespace PressYourLuckApi.Services;

public interface IManageEventService
{
    Task<string> ResetEvent();
    Task<List<CurrentStatusDto>> UpdateAnswerAsync(QuestionAnswerDto teamAnswer);
    Task<List<CurrentStatusDto>> UpdateBoardStopAsync(CurrentStatusDto teamAnswer);
    Task<List<CurrentStatusDto>> GetCurrentStatusAsync();
    Task<List<CurrentStatusDto>> FixAMistakeAsync(CurrentStatusDto teamAnswer);
}

