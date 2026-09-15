using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetUsersDetailedResponse : CommonPaginationResponse
{
    public IEnumerable<UserAdminResult>? Users { get; set; }
}
