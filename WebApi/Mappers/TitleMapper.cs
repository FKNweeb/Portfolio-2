using DataLayer.Models;
using WebApi.DTO.TitleDtos;
using Mapster;
namespace WebApi.Mappers;

public static class TitleMapper
{
    public static GetAllTitleDto ToGetAllTitleDto(this Title titleObject)
    {
        return new GetAllTitleDto
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            TitleKnowAs = titleObject.TitleKnownAs
        };
    }
}
