using WebApi.Models.UserRelatedModels;

namespace WebApi.DTO.UserDtos;

public class UserInfoDTO
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public IList<BookMarkName?> BookMarkNames { get; set; } 
    public IList<BookMarkTitle?> BookMarkTitles { get; set; }
}
