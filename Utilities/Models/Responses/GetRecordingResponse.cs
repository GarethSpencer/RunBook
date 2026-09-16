using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetRecordingResponse : CommonResponse
{
    public RecordingResult? Recording { get; set; }
}
