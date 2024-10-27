namespace WebApi.DTO.TitleDtos;

public class TitleAndDateDto
{
    public string PrimaryTitle { get; set; }
    public string StartDate { get; set; } = string.Empty;

    public string EndDate { get; set; }=string.Empty;
}
