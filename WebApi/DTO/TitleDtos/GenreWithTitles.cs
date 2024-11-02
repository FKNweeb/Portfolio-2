using WebApi.Models.TitleRelatedModels;

namespace WebApi.DTO.TitleDtos;

public class GenreWithTitles
{
    public string GenreName { get; set; }
    public List<Title> Titles { get; set; }

}
