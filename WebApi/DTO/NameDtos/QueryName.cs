using Microsoft.Identity.Client;

namespace WebApi.DTO.NameDtos;

public class QueryName
{
    public string? SortBy { get; set; } = null;

    public bool IsDescending { get; set; } = false;

}
