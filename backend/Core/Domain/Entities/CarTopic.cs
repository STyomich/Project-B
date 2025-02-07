namespace Core.Domain.Entities
{
    public class CarTopic
    {
        public Guid Id { get; set; }
        public string? CarName { get; set; } // Toyota, Ferrari, Lambo...
        public string? CarModel { get; set; } // Corolla, 458 GTB, Countach...
        public string? CarYear { get; set; } // 2021, 2020, 2019...
        public string? Description { get; set; } // Description of the car
        public string? ImageLogoUrl { get; set; } // Image logo URL of the CarName (Toyota, Ferrari...)
        public ICollection<Car>? Cars { get; set; } // Car collection
    }
}