using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PressYourLuckApi.Data
{
    [Table("scoring")]
    public class Scoring
    {
        [Key]
        public int Id { get; set; }
        public string TeamId { get; set; } = "Blue";
        public int PointAmt { get; set; } = 0;
        public int SpinCnt { get; set; } = 0;
    }
}
