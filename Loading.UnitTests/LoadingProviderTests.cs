// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Loading.Domain;

namespace Loading.UnitTests;

[TestFixture(TestOf = typeof(ILoadingProvider))]
internal sealed class LoadingProviderTests
{
    private ILoadingProvider _loadingProvider = null!;

    [SetUp]
    public void Setup() =>
        _loadingProvider = new MockLoadingProvider();

    [Test]
    public async Task Load_ShouldCompleteTask_WhenLoadingOperationsIsEmpty()
    {
        // Arrange.
        var provider = _loadingProvider;
        var operations = new Queue<ILoadingOperation>();
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        // Act.
        var task = provider.Load(operations, cancellationToken);

        // Assert.
        await Asserts.CompletesAsync(task);
    }

    [Test]
    public async Task Load_ShouldCompleteTask_WhenAllOperationsAreCompleted()
    {
        // Arrange.
        var provider = _loadingProvider;
        var operations = new Queue<ILoadingOperation>();
        var cancellationTokenSource = new CancellationTokenSource();
        var operation1 = new MockLoadingOperation(OperationResult.Success);
        var operation2 = new MockLoadingOperation(OperationResult.Success);
        operations.Enqueue(operation1);
        operations.Enqueue(operation2);
        var cancellationToken = cancellationTokenSource.Token;

        // Act.
        var task = provider.Load(operations, cancellationToken);

        // Assert.
        await Asserts.CompletesAsync(task);
        Assert.Multiple(() =>
        {
            Assert.That(operation1.IsStarted);
            Assert.That(operation1.IsCompleted);
            Assert.That(operation2.IsStarted);
            Assert.That(operation2.IsCompleted);
        });
    }

    [Test]
    public Task Load_ShouldCancelTask_WhenCancellationRequested()
    {
        // Arrange
        var provider = _loadingProvider;
        var operations = new Queue<ILoadingOperation>();
        var operation1 = new MockLoadingOperation(OperationResult.Success);
        var operation2 = new MockLoadingOperation(OperationResult.Failure);
        operations.Enqueue(operation1);
        operations.Enqueue(operation2);
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        // Act
        var task = provider.Load(operations, cancellationToken);
        cancellationTokenSource.Cancel();

        // Assert
        var exception = Assert.ThrowsAsync<TaskCanceledException>(() => task);
        Assert.Multiple(() =>
        {
            Assert.That(operation1.IsStarted);
            Assert.That(operation1.IsCompleted);
            Assert.That(operation2.IsStarted, Is.False);
            Assert.That(operation2.IsCompleted, Is.False);
        });

        // Debug.
        Console.WriteLine(exception?.StackTrace);
        
        return Task.CompletedTask;
    }
}