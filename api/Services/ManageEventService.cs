namespace PressYourLuckApi.Services
{
    public class ManageEventService : IManageEventService
    {
        private readonly ContextPYL _contextPYL;
        private readonly ILogger<ManageEventService> _logger;
        public ManageEventService(ILogger<ManageEventService> logger, ContextPYL context)
        {
            _logger = logger;
            _contextPYL = context;
        }

        public async Task<string> ResetEvent()
        {
            var scoreTable =  _contextPYL.Scoring;

            foreach (var score in scoreTable)
            {
                score.PointAmt = 0;
                score.SpinCnt = 0;
            }
            
            _contextPYL.Scoring.UpdateRange(scoreTable);

            await _contextPYL.SaveChangesAsync();

            return $"Results were removed from the system.";

        }
    }
}
