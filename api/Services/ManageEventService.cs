namespace PressYourLuckApi.Services;

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
        var scoreTable =  await _contextPYL.Scoring.ToListAsync();

        foreach (var score in scoreTable)
        {
            score.PointAmt = 0;
            score.SpinCnt = 0;
        }
        
        _contextPYL.Scoring.UpdateRange(scoreTable);

        await _contextPYL.SaveChangesAsync();

        return $"Results were removed from the system.";

    }

    // handles a single. We may rethink this to do multiples at once.
    public async Task<List<CurrentStatusDto>> UpdateAnswerAsync(QuestionAnswerDto teamAnswer)
    {
        if (teamAnswer is null)
        {
            return await GetCurrentStatusAsync();
        }

        var dbRecord = await _contextPYL.Scoring.FirstOrDefaultAsync(s => s.TeamId == teamAnswer.TeamId);

        if (dbRecord is null)
        {
            return await GetCurrentStatusAsync();
        }

        dbRecord.SpinCnt = teamAnswer.SpinChangeCnt;
        _contextPYL.Scoring.Update(dbRecord);
        await _contextPYL.SaveChangesAsync();

        return await GetCurrentStatusAsync();
    }

    public async Task<List<CurrentStatusDto>> UpdateBoardStopAsync(CurrentStatusDto teamAnswer)
    {
        if (teamAnswer is null)
        {
            return await GetCurrentStatusAsync();
        }

        var dbRecord = await _contextPYL.Scoring.FirstOrDefaultAsync(s => s.TeamId == teamAnswer.TeamId);

        if (dbRecord is null)
        {
            return await GetCurrentStatusAsync();
        }

        dbRecord.SpinCnt = teamAnswer.SpinCnt;
        dbRecord.PassCnt = teamAnswer.PassCnt;

        // we pass in the new point amount here. If we pass in a -1, this is a whammy and score = 0.
        if (teamAnswer.PointAmt < 0)
        {
            dbRecord.PointAmt = 0;
        }
        else
        {
            dbRecord.PointAmt += teamAnswer.PointAmt;
        }

        dbRecord.WhammyCnt = teamAnswer.WhammyCnt;

        _contextPYL.Scoring.Update(dbRecord);
        await _contextPYL.SaveChangesAsync();

        return await GetCurrentStatusAsync();
    }

    public async Task<List<CurrentStatusDto>> FixAMistakeAsync(CurrentStatusDto teamAnswer)
    {
        // this is the nuclear option to fix everything
        // do we need to hit the DB first???

        var fixedRecord = new Scoring() 
        {
            TeamId = teamAnswer.TeamId,
            SpinCnt = teamAnswer.SpinCnt,
            PassCnt = teamAnswer.PassCnt,
            PointAmt = teamAnswer.PointAmt,
            WhammyCnt = teamAnswer.WhammyCnt
        };

        _contextPYL.Scoring.Update(fixedRecord);
        await _contextPYL.SaveChangesAsync();
        
        return await GetCurrentStatusAsync();
    }

    public async Task<List<CurrentStatusDto>> GetCurrentStatusAsync()
        =>  await _contextPYL.Scoring.Select(s => new CurrentStatusDto()
        {
            TeamId = s.TeamId,
            SpinCnt = s.SpinCnt,
            PointAmt = s.PointAmt,
            PassCnt = s.PassCnt,
            WhammyCnt = s.WhammyCnt
        }).ToListAsync();
}

