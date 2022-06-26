
using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity;

public class ClaimDTO : UsedDTO
{
    public IEnumerable<Claim> Claims { get; init; } = null!;
}
public class ReplaceClainDTO : UsedDTO
{
    public Claim Claim { get; init; } = null!;
    public Claim NewClaim { get; init; } = null!;
}
