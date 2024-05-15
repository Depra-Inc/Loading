// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depra.Loading.Operations;

namespace Depra.Loading.Curtain
{
	public interface ILoadingCurtain
	{
		Task Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken = default);

		Task Unload(CancellationToken token = default);
	}
}