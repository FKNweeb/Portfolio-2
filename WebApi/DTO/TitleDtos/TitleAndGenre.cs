namespace WebApi.DTO.TitleDtos;

public class TitleAndGenreDTO
{
    public string PrimaryTitle { get; set; }

    public string Plot { get; set; }

    public IList<string> Genre { get; set; } = new List<string>();
}
