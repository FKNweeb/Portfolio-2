namespace WebApi.DTO.TitleDtos;

public class QueryRating
{
    public int? Rate { get; set; } = null;

    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;

}
