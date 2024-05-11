// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Operations
{
	public sealed class FooLoadingOperation : ILoadingOperation
	{
		private float _delay;

		public FooLoadingOperation(float delay = 0) => _delay = delay;

		OperationDescription ILoadingOperation.Description => OperationDescription.Default("Foo");

		async Task ILoadingOperation.Load(ProgressCallback onProgress, CancellationToken token)
		{
			while (_delay-- > 0)
			{
				await Task.Delay(1000, token);
			}

			onProgress(1);
			await Task.CompletedTask;
		}
	}
}