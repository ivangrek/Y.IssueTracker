namespace Y.IssueTracker;

internal interface IResultWithErrorBuilder
{
    IResultWithErrorBuilder WithError(string value);

    IResultWithErrorBuilder WithError(string key, string value);

    IResultWithErrorBuilder WithErrors(IEnumerable<KeyValuePair<string, string>> errors);

    IResult Build();
}

internal sealed class Result : IResult
{
    public static IResult Success()
    {
        return new Result
        {
            Status = ResultStatus.Success
        };
    }

    public static IResultWithErrorBuilder Invalid()
    {
        var result = new Result
        {
            Status = ResultStatus.Invalid
        };

        return new ResultWithErrorBuilder(result);
    }

    public static IResultWithErrorBuilder Failure()
    {
        var result = new Result
        {
            Status = ResultStatus.Failure
        };

        return new ResultWithErrorBuilder(result);
    }

    private Result()
    {
        Errors = Array.Empty<KeyValuePair<string, string>>();
    }

    public ResultStatus Status { get; init; }

    public KeyValuePair<string, string>[] Errors { get; private set; }

    private sealed class ResultWithErrorBuilder : IResultWithErrorBuilder
    {
        private readonly Result result;
        private readonly List<KeyValuePair<string, string>> errors;

        public ResultWithErrorBuilder(Result result)
        {
            this.result = result;
            this.errors = new List<KeyValuePair<string, string>>();
        }

        public IResultWithErrorBuilder WithError(string value)
        {
            this.errors
                .Add(new KeyValuePair<string, string>(string.Empty, value));

            return this;
        }

        public IResultWithErrorBuilder WithError(string key, string value)
        {
            this.errors
                .Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        public IResultWithErrorBuilder WithErrors(IEnumerable<KeyValuePair<string, string>> errors)
        {
            this.errors
                .AddRange(errors);

            return this;
        }

        public IResult Build()
        {
            this.result.Errors = this.errors.ToArray();

            return this.result;
        }
    }
}
