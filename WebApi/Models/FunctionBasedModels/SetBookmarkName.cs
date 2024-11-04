using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.FunctionBasedModels;

[Keyless]
public class SetBookmarkName
{
    public bool IsBookmarkName { get; set; }
}
