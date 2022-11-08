namespace PressYourLuckApi.Models;

public class CurrentStatusDto {
    public string TeamId { get; set; } = String.Empty;
    public int PointAmt { get; set; } = 0;
    public int SpinCnt { get; set; } = 0;
    public int PassCnt { get; set; } = 0;
    public int WhammyCnt { get; set; } = 0;
}


