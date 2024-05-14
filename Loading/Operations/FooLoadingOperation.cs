// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Operations
{
	public sealed class FooLoadingOperation : ILoadingOperation
	{
		private readonly float _step;
		private readonly float _delay;
		private readonly OperationDescription _description;

		public FooLoadingOperation(float step = 0.1f, float delay = 0, OperationDescription description = default)
		{
			_step = step;
			_delay = delay;
			_description = description.Equals(default) ? OperationDescription.Default("Foo") : description;
		}

		OperationDescription ILoadingOperation.Description => _description;

		async Task ILoadingOperation.Load(ProgressCallback onProgress, CancellationToken token)
		{
			onProgress(0);

			var elapsed = 0f;
			while (elapsed < _delay)
			{
				elapsed += _step;
				onProgress(elapsed / _delay);
				await Task.Delay((int) (_step * 1000), token);
			}

			onProgress(1);
			await Task.CompletedTask;
		}
	}
}