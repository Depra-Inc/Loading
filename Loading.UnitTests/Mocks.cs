// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Threading;

namespace Depra.Loading.UnitTests;

internal static class Mocks
{
	internal sealed class LoadingCurtain : ILoadingCurtain
	{
		async ITask ILoadingCurtain.Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken)
		{
			foreach (var operation in operations)
			{
				cancellationToken.ThrowIfCancellationRequested();
				await operation.Load(new Progress<float>(), cancellationToken);
			}
		}

		ITask ILoadingCurtain.Unload(CancellationToken cancellationToken) => Task.CompletedTask.AsITask();
	}

	internal sealed class LoadingOperation : ILoadingOperation
	{
		public bool IsStarted { get; private set; }
		public bool IsCompleted { get; private set; }

		OperationDescription ILoadingOperation.Description => new("Mock loading operation");

		async ITask ILoadingOperation.Load(IProgress<float> progress, CancellationToken cancellationToken)
		{
			progress.Report(0f);
			IsStarted = true;

			await Task.Delay(10, cancellationToken);

			progress.Report(1f);
			IsCompleted = true;
		}
	}
}