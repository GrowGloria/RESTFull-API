namespace RESTFull_API.DTO
{
    public sealed class RollDto
    {
        public Guid Id { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }
        public DateTimeOffset AddedAt { get; set; }
        public DateTimeOffset? RemovedAt { get; set; }
    }
}
