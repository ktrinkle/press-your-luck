namespace PressYourLuckApi.Data;

public static class ModelBuilderExtensions
{
    public static readonly List<Scoring> Scoring = new()
    {
        new Scoring()
        {
            TeamId = "Yellow",
            SpinCnt = 0,
            PointAmt = 0,
            PassCnt = 0,
            WhammyCnt = 0
        },
        new Scoring()
        {
            TeamId = "Red",
            SpinCnt = 0,
            PointAmt = 0,
            PassCnt = 0,
            WhammyCnt = 0
        },
        new Scoring()
        {
            TeamId = "Green",
            SpinCnt = 0,
            PointAmt = 0,
            PassCnt = 0,
            WhammyCnt = 0
        },
        new Scoring()
        {
            TeamId = "Blue",
            SpinCnt = 0,
            PointAmt = 0,
            PassCnt = 0,
            WhammyCnt = 0
        },
    };

    public static void CreateScoringData(this ModelBuilder modelBuilder) => modelBuilder.Entity<Scoring>().HasData(Scoring.ToArray());

}

