namespace PressYourLuck
{
    public class Config
    {
        public bool OnlineInd { get; set; }
        public string? ApiUrl { get; set; }
        public string? ApiKey { get; set; }
        public string SignalRHub { get; set; } = "https://";
        public int[] Pins { get; set; } = {0};
        public string[] Colors { get; set; } = {""};
        public int[] Lights { get; set; } = {0};
    }
}