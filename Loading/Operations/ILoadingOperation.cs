// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Operations
{
	public interface ILoadingOperation
	{
		OperationDescription Description { get; }

		Task Load(Action<float> onProgress, CancellationToken token);
	}
}