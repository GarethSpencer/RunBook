using Utilities.Models.Responses.Generic;

namespace Utilities.Models.Responses;

public class AddUserResponse : CommonResponse
{
    public Guid? UserId { get; set; }
}
