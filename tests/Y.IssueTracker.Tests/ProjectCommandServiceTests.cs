namespace Y.IssueTracker.Tests;

using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Projects;
using Projects.Commands;
using Projects.Domain;

internal sealed class ProjectCommandServiceTests
{
    private IProjectCommandService projectCommandService;

    private Mock<IUnitOfWork> initOfWorkMock;
    private Mock<IProjectRepository> projectRepositoryMock;

    [SetUp]
    public void Setup()
    {
        this.initOfWorkMock = new Mock<IUnitOfWork>();
        this.projectRepositoryMock = new Mock<IProjectRepository>();

        this.projectCommandService = new ProjectCommandService(
            this.initOfWorkMock.Object,
            this.projectRepositoryMock.Object);
    }

    #region CreateCommand

    [Test]
    public async Task CreateCommand_invalid_with_empty_name()
    {
        // Arrange
        var createCommandMock = new Mock<ICreateCommand>();

        createCommandMock.SetupGet(x => x.Name)
            .Returns(string.Empty);

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(createCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Invalid));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateCommand_should_create_project()
    {
        // Arrange
        var createCommandMock = new Mock<ICreateCommand>();

        createCommandMock.SetupGet(x => x.Name)
            .Returns("Project");

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(createCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
    }

    #endregion CreateCommand

    #region UpdateCommand

    [Test]
    public async Task UpdateCommand_should_invalid_with_empty_name()
    {
        // Arrange
        var updateCommandMock = new Mock<IUpdateCommand>();

        updateCommandMock.SetupGet(x => x.Name)
            .Returns(string.Empty);

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(updateCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Invalid));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateCommand_should_failure_when_not_found()
    {
        // Arrange
        var updateCommandMock = new Mock<IUpdateCommand>();

        updateCommandMock.SetupGet(x => x.Name)
            .Returns("Project");

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(updateCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Failure));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateCommand_should_failure_when_not_active()
    {
        // Arrange
        var project = new Project(Guid.Empty)
        {
            Name = "Project",
            IsActive = false
        };

        this.projectRepositoryMock
            .Setup(x => x.QueryByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(project));

        var updateCommandMock = new Mock<IUpdateCommand>();

        updateCommandMock.SetupGet(x => x.Name)
            .Returns("Project new");

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(updateCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Failure));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateCommand_should_update_project()
    {
        // Arrange
        var projectName = "Project new";
        var project = new Project(Guid.Empty)
        {
            Name = "Project",
            IsActive = true
        };

        this.projectRepositoryMock
            .Setup(x => x.QueryByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(project));

        var updateCommandMock = new Mock<IUpdateCommand>();

        updateCommandMock.SetupGet(x => x.Name)
            .Returns(projectName);

        // Act
        var result = await this.projectCommandService
            .ExecuteAsync(updateCommandMock.Object);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
        Assert.That(project.Name, Is.EqualTo(projectName));
        Assert.That(project.IsActive, Is.True);
    }

    #endregion UpdateCommand
}
