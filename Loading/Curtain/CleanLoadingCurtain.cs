// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depra.Threading;

namespace Depra.Loading
{
	public readonly struct CleanLoadingCurtain : ILoadingCurtain
	{
		async ITask ILoadingCurtain.Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken)
		{
			foreach (var operation in operations)
			{
				await operation.Load(new Progress<float>(), cancellationToken);
			}
		}

		ITask ILoadingCurtain.Unload(CancellationToken cancellationToken) => Task.CompletedTask.AsITask();
	}
}