using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetRecordingsResponse<TValue> : CommonResponse
{
    public IEnumerable<RecordingResult<TValue>>? Recordings { get; set; }
}
