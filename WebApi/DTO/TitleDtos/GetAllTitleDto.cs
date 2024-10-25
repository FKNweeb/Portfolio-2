using DataLayer.Models;

namespace WebApi.DTO.TitleDtos;

public class GetAllTitleDto
{
   
    public string PrimaryTitle { get; set; }   

    public List<TitleKnowAs?> TitleKnowAs { get; set; }
}
