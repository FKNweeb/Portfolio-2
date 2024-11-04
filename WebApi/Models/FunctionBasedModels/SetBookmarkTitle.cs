using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.FunctionBasedModels;

[Keyless]
public class SetBookmarkTitle
{
    public bool IsBookmarkTitle { get; set; }
}
