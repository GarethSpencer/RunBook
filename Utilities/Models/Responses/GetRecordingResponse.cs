using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetRecordingResponse<TValue> : CommonResponse
{
    public RecordingResult<TValue>? Recording { get; set; }
}
