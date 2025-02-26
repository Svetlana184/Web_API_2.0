namespace Web_API_2._0.Model
{
    public class Errors
    {
        public long Timestamp { get; set; }
        public string Message { get; set; } = null!;
        public int ErrorCode { get; set; }
    }
}
