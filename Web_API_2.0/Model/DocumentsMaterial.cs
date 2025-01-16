namespace Web_API_2._0.Model
{
    public class DocumentsMaterial
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date_created { get; set; }
        public DateTime Date_updated { get; set; }
        public string? Category { get; set; }
        public bool Has_comments { get; set; }
    }
}
