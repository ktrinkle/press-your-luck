namespace PressYourLuckApi.Models;

public class BoardStopDto {
    public string TeamId { get; set; } = String.Empty;
    public int PointAmt { get; set; } = 0;
    public int SpinChangeCnt { get; set; } = 0;
    public bool WhammyInd { get; set; } = false;
}


