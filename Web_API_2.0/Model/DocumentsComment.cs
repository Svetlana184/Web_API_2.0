namespace Web_API_2._0.Model
{
    public class DocumentsComment
    {
        public int Id { get; set; }
        public int Document_id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date_created { get; set; }
        public DateTime? Date_updated { get; set; }
        
    }
}
