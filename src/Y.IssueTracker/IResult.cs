namespace Y.IssueTracker;

using System.Collections.Generic;

public enum ResultStatus
{
    Success,
    Invalid,
    Failure
}

public interface IResult
{
    ResultStatus Status { get; }

    KeyValuePair<string, string>[] Errors { get; }
}
