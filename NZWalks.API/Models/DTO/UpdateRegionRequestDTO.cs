namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? RegionImageUrl { get; set; }
    }
}
