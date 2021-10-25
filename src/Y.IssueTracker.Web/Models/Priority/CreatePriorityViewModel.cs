namespace Y.IssueTracker.Web.Models.Priority
{
    using Priorities.Commands;

    public sealed class CreatePriorityViewModel : ICreateCommand
    {
        public string Name { get; set; }

        public int Weight { get; set; }
    }
}
