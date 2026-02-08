using RESTFull_API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace RESTFull_API.Models
{
    /// <summary>
    /// Класс модель, рулонов.
    /// </summary>
    public class Roll : BaseModel
    {        
        [Range(0.01, double.MaxValue)]
        public decimal Length { get; set; }
        
        [Range(0.01, double.MaxValue)]
        public decimal Weight { get; set; }
        
        public DateTimeOffset AddedAt { get; set; }
        
        public DateTimeOffset? RemovedAt { get; set; }
    }
}
