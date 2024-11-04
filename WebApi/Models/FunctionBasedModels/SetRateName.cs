using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.FunctionBasedModels;

[Keyless]
public class SetRateName
{
    public bool IsSetRateName { get; set; }
}
