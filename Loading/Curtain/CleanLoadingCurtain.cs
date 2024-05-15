// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depra.Loading.Operations;

namespace Depra.Loading.Curtain
{
	public readonly struct CleanLoadingCurtain : ILoadingCurtain
	{
		async Task ILoadingCurtain.Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken)
		{
			foreach (var operation in operations)
			{
				await operation.Load(_ => { }, cancellationToken);
			}
		}

		Task ILoadingCurtain.Unload(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}