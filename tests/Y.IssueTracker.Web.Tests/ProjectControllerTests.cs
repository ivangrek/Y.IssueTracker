namespace Y.IssueTracker.Web.Tests;

using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Project;
using Moq;
using NUnit.Framework;
using Projects;
using Projects.Commands;
using Projects.Results;

internal sealed class ProjectControllerTests
{
    private ProjectController projectController;

    private Mock<IProjectCommandService> projectCommandServiceMock;
    private Mock<IProjectQueryService> projectQueryServiceMock;

    [SetUp]
    public void Setup()
    {
        this.projectCommandServiceMock = new Mock<IProjectCommandService>();
        this.projectQueryServiceMock = new Mock<IProjectQueryService>();

        this.projectController = new ProjectController(
            this.projectCommandServiceMock.Object,
            this.projectQueryServiceMock.Object);
    }

    [Test]
    public async Task Index_method_should_return_view_result()
    {
        // Arrange
        this.projectQueryServiceMock
            .Setup(x => x.QueryAllAsync())
            .Returns(Task.FromResult(Array.Empty<IProjectResult>()));

        // Act
        var result = await this.projectController
            .Index();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    [Test]
    public void Create_get_method_should_return_view_result()
    {
        // Arrange
        // Act
        var result = this.projectController
            .Create();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    [Test]
    public async Task Create_post_method_should_return_redirect_to_action_result_on_success()
    {
        // Arrange
        var successResultMock = new Mock<IResult>();

        successResultMock
            .SetupGet(x => x.Status)
            .Returns(ResultStatus.Success);

        this.projectCommandServiceMock
            .Setup(x => x.ExecuteAsync(It.IsAny<ICreateCommand>()))
            .Returns(Task.FromResult(successResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .Create(viewModel);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
    }

    [Test]
    public async Task Create_post_method_should_return_redirect_result_on_invalid()
    {
        // Arrange
        var invalidResultMock = new Mock<IResult>();

        invalidResultMock
            .SetupGet(x => x.Status)
            .Returns(ResultStatus.Invalid);

        this.projectCommandServiceMock
            .Setup(x => x.ExecuteAsync(It.IsAny<ICreateCommand>()))
            .Returns(Task.FromResult(invalidResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .Create(viewModel);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    [Test]
    public async Task Create_post_method_should_return_bad_request_result_on_failure()
    {
        // Arrange
        var failureResultMock = new Mock<IResult>();

        failureResultMock
            .SetupGet(x => x.Status)
            .Returns(ResultStatus.Failure);

        this.projectCommandServiceMock
            .Setup(x => x.ExecuteAsync(It.IsAny<ICreateCommand>()))
            .Returns(Task.FromResult(failureResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .Create(viewModel);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }
}
