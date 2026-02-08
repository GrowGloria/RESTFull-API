namespace RESTFull_API.DTOs
{
    public sealed class RollStatsDto
    {
        public int AddedCount { get; set; }
        public int RemovedCount { get; set; }
        
        public decimal? AverageLength { get; set; }
        public decimal? AverageWeight { get; set; }

        public decimal? MinLength { get; set; }
        public decimal? MaxLength { get; set; }

        public decimal? MinWeight { get; set; }
        public decimal? MaxWeight { get; set; }

        public decimal TotalWeight { get; set; }

        public TimeSpan? MinLifetime { get; set; }
        public TimeSpan? MaxLifetime { get; set; }
    }
}
