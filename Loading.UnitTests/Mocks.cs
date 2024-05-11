// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Loading.Curtain;
using Depra.Loading.Operations;

namespace Depra.Loading.UnitTests;

internal static class Mocks
{
	internal sealed class LoadingCurtain : ILoadingCurtain
	{
		async Task ILoadingCurtain.Load(IEnumerable<ILoadingOperation> operations,
			CancellationToken cancellationToken)
		{
			foreach (var operation in operations)
			{
				cancellationToken.ThrowIfCancellationRequested();
				await operation.Load(_ => { }, cancellationToken);
			}
		}

		void ILoadingCurtain.Unload() { }
	}

	internal sealed class LoadingOperation : ILoadingOperation
	{
		public bool IsStarted { get; private set; }

		public bool IsCompleted { get; private set; }

		OperationDescription ILoadingOperation.Description => new("Mock loading operation");

		async Task ILoadingOperation.Load(ProgressCallback onProgress, CancellationToken cancellationToken)
		{
			onProgress(0f);
			IsStarted = true;

			await Task.Delay(10, cancellationToken);

			onProgress(1f);
			IsCompleted = true;
		}
	}
}