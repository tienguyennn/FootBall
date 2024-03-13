namespace N.Service.DTO
{
    public class Storage
    {
        public string? Name { get; set; }

        public CountAndSize? Data { get; set; }
    }

    public class CountAndSize
    {
        public int? Count { get; set; }
        public long? Size { get; set; }
    }
}
