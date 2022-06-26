
using Microsoft.AspNetCore.Identity;

using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.DTO.Identity;

public abstract class UsedDTO
{
    public User User { get; set; } = null!;
}

public class AddLoginDTO : UsedDTO
{
    public UserLoginInfo UserLoginInfo { get; init; } = null!;
}

public class PasswordHashDTO : UsedDTO
{
    public string Hash { get; set; } = null!;
}

public class SetLockoutDTO : UsedDTO
{
    public DateTimeOffset? LockoutEnd { get; init; } = null!;
}
