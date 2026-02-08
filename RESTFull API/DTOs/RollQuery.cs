namespace RESTFull_API.DTOs
{
    public class RollQuery
    {
        public Guid? Id { get; set; }

        public decimal? LengthFrom { get; set; }
        public decimal? LengthTo { get; set; }

        public decimal? WeightFrom { get; set; }
        public decimal? WeightTo { get; set; }

        public DateTimeOffset? AddedFrom { get; set; }
        public DateTimeOffset? AddedTo { get; set; }
        
        public DateTimeOffset? RemovedFrom { get; set; }
        public DateTimeOffset? RemovedTo { get; set; }

        public bool? OnlyInStock { get; set; }
    }
}
