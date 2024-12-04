using WebApi.Models;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.DTO.TitleDtos;

public class TitleAndPlotDto
{ 
    public string tconst { get; set; }
    public string PrimaryTitle { get; set; }

    public string Plot { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }

    public IList<string> Genres { get; set; } = new List<string>();

    public IList<string> Languages { get; set; } = new List<string>();

    public string Poster { get; set; }

    public string Type { get; set; }

    public IList<string> Crew { get; set; } = new List<string>();

   

}
