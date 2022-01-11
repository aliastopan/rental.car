namespace Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Manufacture { get; set; }
        public string? Brand { get; set; }
        public string? Year { get; set; }
        public int? Capacity { get; set; }
        public bool? Status { get; set; }
        public string? ImagePath { get; set; }
    }
}