namespace Y.IssueTracker.Tests;

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
        var createCommand = new CreateCommand
        {
            Name = string.Empty
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(createCommand);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Invalid));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateCommand_should_create_project()
    {
        // Arrange
        var createCommand = new CreateCommand
        {
            Name = Guid.NewGuid().ToString()
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(createCommand);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
    }

    #endregion CreateCommand

    #region UpdateCommand

    [Test]
    public async Task UpdateCommand_should_invalid_with_empty_name()
    {
        // Arrange
        var updateCommand = new UpdateCommand
        {
            Name = string.Empty
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(updateCommand);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Invalid));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateCommand_should_failure_when_not_found()
    {
        // Arrange
        var updateCommand = new UpdateCommand
        {
            Name = Guid.NewGuid().ToString()
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(updateCommand);

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
            Name = Guid.NewGuid().ToString(),
            IsActive = false
        };

        this.projectRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(project));

        var updateCommand = new UpdateCommand
        {
            Name = Guid.NewGuid().ToString()
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(updateCommand);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Failure));
        Assert.That(result.Errors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateCommand_should_update_project()
    {
        // Arrange
        var project = new Project(Guid.Empty)
        {
            Name = Guid.NewGuid().ToString(),
            IsActive = true
        };

        this.projectRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(project));

        var updateCommand = new UpdateCommand
        {
            Name = Guid.NewGuid().ToString()
        };

        // Act
        var result = await this.projectCommandService
            .HandleAsync(updateCommand);

        // Assert
        Assert.That(result.Status, Is.EqualTo(ResultStatus.Success));
        Assert.That(project.Name, Is.EqualTo(updateCommand.Name));
        Assert.That(project.IsActive, Is.True);
    }

    #endregion UpdateCommand
}
