// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Threading;

namespace Depra.Loading.UnitTests;

internal sealed class LoadingCurtainTests
{
	private ILoadingCurtain _loadingCurtain = null!;

	[SetUp]
	public void Setup() => _loadingCurtain = new Mocks.LoadingCurtain();

	[Test]
	public async Task Load_ShouldCompleteTask_WhenLoadingOperationsIsEmpty()
	{
		// Arrange:
		var operations = new Queue<ILoadingOperation>();
		var cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = cancellationTokenSource.Token;

		// Act:
		var task = _loadingCurtain.Load(operations, cancellationToken);

		// Assert:
		await Asserts.CompletesAsync(task.AsTask());
	}

	[Test]
	public async Task Load_ShouldCompleteTask_WhenAllOperationsAreCompleted()
	{
		// Arrange:
		var operations = new Queue<ILoadingOperation>();
		var cancellationTokenSource = new CancellationTokenSource();
		var operation1 = new Mocks.LoadingOperation();
		var operation2 = new Mocks.LoadingOperation();
		operations.Enqueue(operation1);
		operations.Enqueue(operation2);
		var cancellationToken = cancellationTokenSource.Token;

		// Act:
		var task = _loadingCurtain.Load(operations, cancellationToken);

		// Assert:
		await Asserts.CompletesAsync(task.AsTask());
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
		// Arrange:
		var operations = new Queue<ILoadingOperation>();
		var operation1 = new Mocks.LoadingOperation();
		var operation2 = new Mocks.LoadingOperation();
		operations.Enqueue(operation1);
		operations.Enqueue(operation2);
		var cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = cancellationTokenSource.Token;

		// Act:
		var task = _loadingCurtain.Load(operations, cancellationToken);
		cancellationTokenSource.Cancel();

		// Assert:
		var exception = Assert.ThrowsAsync<TaskCanceledException>(() => task.AsTask());
		Assert.Multiple(() =>
		{
			Assert.That(operation1.IsStarted);
			Assert.That(operation1.IsCompleted, Is.False);
			Assert.That(operation2.IsStarted, Is.False);
			Assert.That(operation2.IsCompleted, Is.False);
		});

		// Debug:
		Console.WriteLine(exception?.StackTrace);

		return Task.CompletedTask;
	}
}