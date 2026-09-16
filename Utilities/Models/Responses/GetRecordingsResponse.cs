using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetRecordingsResponse : CommonResponse
{
    public IEnumerable<RecordingResult>? Recordings { get; set; }
}
