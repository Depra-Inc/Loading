// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Operations
{
	public sealed class FooLoadingOperation : ILoadingOperation
	{
		private readonly float _step;
		private readonly float _delay;
		private readonly OperationDescription _description;

		public FooLoadingOperation(OperationDescription description, float delay = 0, float step = 0.1f)
		{
			_step = step;
			_delay = delay;
			_description = description;
		}

		OperationDescription ILoadingOperation.Description => _description;

		async Task ILoadingOperation.Load(IProgress<float> progress, CancellationToken token)
		{
			progress.Report(0);

			var elapsed = 0f;
			while (elapsed < _delay)
			{
				elapsed += _step;
				progress.Report(elapsed / _delay);
				await Task.Delay((int) (_step * 1000), token);
			}

			progress.Report(1);
			await Task.CompletedTask;
		}
	}
}