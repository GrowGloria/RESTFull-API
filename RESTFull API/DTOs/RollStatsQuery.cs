namespace RESTFull_API.DTOs
{
    public sealed class RollStatsQuery
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
    }
}
