namespace Y.IssueTracker.Web.Tests;

using Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Project;
using Moq;
using NUnit.Framework;
using Projects.Commands;
using Projects.Results;
using Y.IssueTracker.Projects.Queries;
using Y.IssueTracker.Web.Services;

internal sealed class ProjectControllerTests
{
    private ProjectController projectController;

    private Mock<IProjectService> projectServiceMock;

    [SetUp]
    public void Setup()
    {
        this.projectServiceMock = new Mock<IProjectService>();

        this.projectController = new ProjectController(
            this.projectServiceMock.Object);
    }

    [Test]
    public async Task Index_method_should_return_view_result()
    {
        // Arrange
        this.projectServiceMock
            .Setup(x => x.HandleAsync(It.IsAny<GetAllQuery>()))
            .Returns(Task.FromResult(Array.Empty<ProjectResult>()));

        // Act
        var result = await this.projectController
            .IndexAsync(1, 10);

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

        this.projectServiceMock
            .Setup(x => x.HandleAsync(It.IsAny<CreateCommand>()))
            .Returns(Task.FromResult(successResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .CreateAsync(viewModel);

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

        this.projectServiceMock
            .Setup(x => x.HandleAsync(It.IsAny<CreateCommand>()))
            .Returns(Task.FromResult(invalidResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .CreateAsync(viewModel);

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

        this.projectServiceMock
            .Setup(x => x.HandleAsync(It.IsAny<CreateCommand>()))
            .Returns(Task.FromResult(failureResultMock.Object));

        var viewModel = new CreateProjectViewModel();

        // Act
        var result = await this.projectController
            .CreateAsync(viewModel);

        // Assert
        Assert.That(result, Is.TypeOf<StatusCodeResult>());
        Assert.That(((StatusCodeResult)result).StatusCode, Is.EqualTo(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));
    }
}
