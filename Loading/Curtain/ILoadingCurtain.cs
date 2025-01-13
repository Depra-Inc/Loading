// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using Depra.Threading;

namespace Depra.Loading
{
	public interface ILoadingCurtain
	{
		ITask Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken = default);

		ITask Unload(CancellationToken token = default);
	}
}