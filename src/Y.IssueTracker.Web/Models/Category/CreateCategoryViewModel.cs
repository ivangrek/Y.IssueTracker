namespace Y.IssueTracker.Web.Models.Category
{
    using Categories.Commands;

    public sealed class CreateCategoryViewModel : ICreateCommand
    {
        public string Name { get; set; }
    }
}
