using WebApi.Models;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.DTO.TitleDtos;

public class TitleAndPlotDto
{ 
    public string PrimaryTitle { get; set; }

    public string Plot { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }

   

}
